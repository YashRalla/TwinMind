using CoreEngine.Data;
using CoreEngine.Repositories;
using CoreEngine.Controllers;
using Microsoft.EntityFrameworkCore;

// 1. Create the application builder
var builder = WebApplication.CreateBuilder(args);

// 2. Register services to the DI container.

// 2.1 The below line is a Framework service
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

// 2.2 Registering service to the DI Container.
// 2.3 Format: builder.Services.Add{Lifetime}<Interface, Implementation>();
builder.Services.AddScoped<IAssetRepository, SqlAssetRepository>();
builder.Services.AddDbContext<CoreEngineContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseSeeding((context, _) =>
    {
	var parentAsset = context.Set<AssetComponent>().FirstOrDefault(a => a.Id == "1");
	if (parentAsset == null)
	{
		context.Set<AssetComponent>().Add(new AssetComponent
		{
			Id = "1",
			ParentID = null,
			Tag = "PARENT_ASSET",
			Name = "Parent Asset",
			Status = "Active",
			LastInspectTime = DateTime.UtcNow
		});
		context.SaveChanges();
	}
	var childAsset1 = context.Set<AssetComponent>().FirstOrDefault(b => b.Id == "2");
	if (childAsset1 == null)
	{
		context.Set<AssetComponent>().Add(new AssetComponent
		{
			Id = "2",
			ParentID = "1",
			Tag = "CHILD_ASSET",
			Name = "Child Asset",
			Status = "Active",
			LastInspectTime = DateTime.UtcNow
		});
		context.SaveChanges();
	}
	var docHotspot = context.Set<DocumentHotspot>().FirstOrDefault(c => c.Id == 1);
	if (docHotspot == null)
	{
		context.Set<DocumentHotspot>().Add(new DocumentHotspot
		{
			Id = 1,
			AssetComponentID = "2",
			DocumentID = "newDoc",
			Name = "Hotspot",
			CoordX = 100,
			CoordY = 200
		});
		context.SaveChanges();
	}
    });
    options.UseAsyncSeeding(async (context, _, cancellationToken) =>
    {
        var parentAsset = await context.Set<AssetComponent>().FirstOrDefaultAsync(a => a.Id == "1", cancellationToken);
        if (parentAsset == null)
        {
		context.Set<AssetComponent>().Add(new AssetComponent
            	{
                	Id = "1",
                	ParentID = null,
                	Tag = "PARENT_ASSET",
                	Name = "Parent Asset",
			Status = "Active",
                	LastInspectTime = DateTime.UtcNow
            	});
            	await context.SaveChangesAsync(cancellationToken);
        }
	var childAsset1 = await context.Set<AssetComponent>().FirstOrDefaultAsync(b => b.Id == "2", cancellationToken);
        if (childAsset1 == null)
        {
                context.Set<AssetComponent>().Add(new AssetComponent
                {
                        Id = "2",
                        ParentID = "1",
                        Tag = "CHILD_ASSET",
                        Name = "Child Asset",
                        Status = "Active",
                        LastInspectTime = DateTime.UtcNow
                });
                await context.SaveChangesAsync(cancellationToken);
        }
	var docHotspot = await context.Set<DocumentHotspot>().FirstOrDefaultAsync(c => c.Id == 1, cancellationToken);
        if (docHotspot == null)
        {
                context.Set<DocumentHotspot>().Add(new DocumentHotspot
                {
                        Id = 1,
                        AssetComponentID = "2",
                        DocumentID = "newDoc",
                        Name = "Hotspot",
                        CoordX = 100,
                        CoordY = 200
                });
		await context.SaveChangesAsync(cancellationToken);
        }
    });
});

// 2.4 Again, the below is likely a Framework service. More info at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// 3. Build the application i.e. finalize the container
var app = builder.Build();

// 4. Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// 5. Start the application

// Automatically apply migrations and run seeding on startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CoreEngineContext>();
    dbContext.Database.Migrate(); // This triggers UseSeeding!
}
app.Run();

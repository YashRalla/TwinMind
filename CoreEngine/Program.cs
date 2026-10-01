using CoreEngine.Data;
using CoreEngine.Repositories;
using CoreEngine.Controllers;
using Microsoft.EntityFrameworkCore;

// 1. Create the application builder
var builder = WebApplication.CreateBuilder(args);

// 2. Register services to the DI container.

// 2.1 The below line is a Framework service
builder.Services.AddControllers();

// 2.2 Registering service to the DI Container.
// 2.3 Format: builder.Services.Add{Lifetime}<Interface, Implementation>();
builder.Services.AddScoped<IAssetRepository, SqlAssetRepository>();
builder.Services.AddDbContext<CoreEngineContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
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
app.Run();

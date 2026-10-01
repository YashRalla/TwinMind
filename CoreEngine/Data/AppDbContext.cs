using Microsoft.EntityFrameworkCore;

namespace CoreEngine.Data;

public class CoreEngineContext : DbContext 
{
    // This is the standard constructor EF Core expects
    // This is a 'special' form of dependency injection. DbContextOptions<T> is a container, similar to a standard List, that was [contd]
    // created as part of the DbContext class. The T here is the same class we are creating this in. The instantiated object is called 'options'.
    // : base(options) sends this data back to the DbContext class to instantiate the database connection.
    // However, options is not populated by default. At runtime, the .NET DI Container, which runs the app builds the options package.
    // The constructor remains empty.
    public CoreEngineContext(DbContextOptions<CoreEngineContext> options) : base(options)
    {
    }
    
    public DbSet<AssetComponent> AssetComponents { get; set; }
    public DbSet<DocumentHotspot> DocumentHotspots { get; set; }
}

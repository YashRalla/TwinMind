using CoreEngine.Data;
using CoreEngine; // Adjusts to find AssetComponent and DocumentHotspot in the root namespace

namespace CoreEngine.Repositories;

public class SqlAssetRepository : IAssetRepository
{
    private readonly CoreEngineContext _context;

    public SqlAssetRepository(CoreEngineContext context)
    {
        _context = context;
    }

    public List<AssetComponent> AllAssets()
    {
        return _context.AssetComponents.ToList();
    }

    public AssetComponent GetAsset(string ID)
    {
        return _context.AssetComponents.Find(ID);
    }

    public List<DocumentHotspot> AllHotspots()
    {
        return _context.DocumentHotspots.ToList();
    }
}

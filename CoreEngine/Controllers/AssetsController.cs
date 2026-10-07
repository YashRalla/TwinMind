using Microsoft.AspNetCore.Mvc;
using CoreEngine.Repositories;

namespace CoreEngine.Controllers;

[ApiController]
[Route("[controller]")]
public class AssetsController : ControllerBase
{
	private readonly IAssetRepository _assetRepo;
	
	public AssetsController(IAssetRepository assetRepo)
	{
		_assetRepo = assetRepo;
	}

	[HttpGet(Name="GetAssets")]

	public IEnumerable<AssetComponent> Get() => _assetRepo.AllAssets();

	[HttpGet("{id}")]

	public ActionResult<AssetComponent> GetAsset(string id)
	{
		var asset = _assetRepo.GetAsset(id);
		if (asset == null)
		{
			return NotFound();
		}
		return asset;
	}

	[HttpPost]

	public IActionResult Create(AssetComponent asset)
	{
		_assetRepo.AddAsset(asset);
		return CreatedAtAction(nameof(GetAsset), 
				new { id = asset.Id }, asset);
	}
}

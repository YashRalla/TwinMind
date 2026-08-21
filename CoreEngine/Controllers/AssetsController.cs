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
}

using System;
using System.Collections.Generic;

namespace CoreEngine.Repositories;

public interface IAssetRepository
{
	List<AssetComponent> AllAssets();
	AssetComponent? GetAsset(string ID);
	List<DocumentHotspot> AllHotspots();
	void AddAsset(AssetComponent Asset);
	void UpdateAsset(AssetComponent Asset);
	void DeleteAsset(AssetComponent Asset);
}

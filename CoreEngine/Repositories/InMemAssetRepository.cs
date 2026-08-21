using System;
using System.Collections.Generic;

namespace CoreEngine.Repositories;

public class InMemAssetRepository : IAssetRepository
{
	private readonly List<AssetComponent> _assetList = new();
	private readonly List<DocumentHotspot> _hotspotList = new();

	public InMemAssetRepository()
	{
		var parentAsset = new AssetComponent {
			Id = "1",
			Name = "Parent Asset",
			Tag = "PARENT_ASSET"
		};

		var childAsset = new AssetComponent {
			Id = "2",
			ParentID = "1",
			Name = "Child Asset",
			Tag = "CHILD_ASSET"
		};

		var hotspot = new DocumentHotspot {
			AssetComponentID = "2",
			Name = "Hotspot",
			CoordX = 100,
			CoordY = 200
		};

		_assetList.Add(parentAsset);
		_assetList.Add(childAsset);
		_hotspotList.Add(hotspot);
	}

	public List<AssetComponent> AllAssets() => _assetList;

	public AssetComponent GetAsset(string ID)
	{
		foreach (AssetComponent asset in _assetList)
		{
			if (asset.Id == ID)
			{
				return asset;
			}
		}
		return null;//in case there is no matching ID
	}

	public List<DocumentHotspot> AllHotspots() => _hotspotList;
}

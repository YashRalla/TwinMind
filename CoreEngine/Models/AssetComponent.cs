using System;
using System.Collections.Generic;

public class AssetComponent
{
	public string Id {get; set;} = "0";
	public string? ParentID {get; set;}
	public string Tag {get; set;}
	public string Name {get; set;}
	public string Status {get; set;}
	public DateTime LastInspectTime {get; set;}
	
	public AssetComponent? Parent {get; set;}
	public List<AssetComponent> Children {get; set;} = new List<AssetComponent> ();
	
	public List<DocumentHotspot> Hotspots {get; set;} = new List<DocumentHotspot>();
}

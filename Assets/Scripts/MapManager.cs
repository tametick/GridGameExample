using System.IO;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;
using Xml2CSharp;

public class MapManager : MonoBehaviour {

	public TextAsset xmlMap;

	public GameObject grassPrefab;
	public GameObject mountainPrefab;
	public GameObject treesPrefab;
	public GameObject rocksPrefab;

	void Start() {
		var map = Deserialize<Map>(xmlMap.text);
		var mapLayer = new MapLayer(map.Layer.Name, map.Layer.Width, map.Layer.Height, map.Layer.Data.Text);
		var mapObjectGroup = new MapObjectGroup(map.Objectgroup.Name, map.Objectgroup.Object);
		
		for(var y=0; y<mapLayer.height; y++) {
			for (var x = 0; x < mapLayer.width; x++) {
				var newTile = Instantiate(GetPrefab(mapLayer.GetTile(x,y)), transform);
				newTile.transform.localPosition = new Vector3(x, 0, -y);
				newTile.name = $"{x},{y}";
			}
		}

	}

	private GameObject GetPrefab(TileType tileType) {
		switch (tileType) {
			case TileType.Grass:
				return grassPrefab;
			case TileType.Mountain:
				return mountainPrefab;
			case TileType.Rocks:
				return rocksPrefab;
			case TileType.Tree:
				return treesPrefab;
			default:
				return null;
		}
	}

	static T Deserialize<T>(string xmlString) {
		XmlSerializer serializer = new XmlSerializer(typeof(T));
		MemoryStream xmlStream = new MemoryStream(Encoding.UTF8.GetBytes(xmlString));
		return (T)serializer.Deserialize(xmlStream);
	}
}

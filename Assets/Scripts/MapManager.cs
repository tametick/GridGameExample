using Pathfinding;
using System.Collections.Generic;
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

	public GameObject playerPrefab;
	public GameObject enemyPrefab;

	Pathfinder pathfinder;
	IntVector2? selectedTile;

	void Start() {
		var map = Deserialize<Map>(xmlMap.text);
		var mapLayer = new MapLayer(map.Layer.Name, map.Layer.Width, map.Layer.Height, map.Layer.Data.Text);
		var mapObjectGroup = new MapObjectGroup(map.Objectgroup.Name, map.Objectgroup.Object);
		
		for(var y=0; y<mapLayer.height; y++) {
			for (var x = 0; x < mapLayer.width; x++) {
				var newTile = Instantiate(GetPrefab(mapLayer.GetTile(x,y)), transform);
				newTile.transform.localPosition = new Vector3(x, 0, -y);
				newTile.name = $"{x},{y}";
				var tile = newTile.AddComponent<Tile>();
				tile.x = x;
				tile.y = y;
				tile.OnClickTile = ClickTile;
			}
		}

		pathfinder = new Pathfinder();
		pathfinder.Init(mapLayer.data, new List<int>() { (int)TileType.Grass }, HasWall, BlockedByOthers);

		foreach(var obj in mapObjectGroup.objects) {
			GameObject newObject= Instantiate(GetObjectPrefab(obj.type), transform);
			newObject.transform.localPosition = new Vector3(obj.x, 0, -obj.y);
		}
	}

	private GameObject GetObjectPrefab(ObjectType type) {
		switch (type) {
			case ObjectType.Player:
				return playerPrefab;
			case ObjectType.Enemy:
				return enemyPrefab;
			default:
				return null;
		}
	}

	private void ClickTile(int x, int y) {
		
		if (selectedTile == null) {
			Debug.Log($"start from {x},{y}");
			selectedTile = new IntVector2(x, y);
		} else {
			var result = pathfinder.Search((IntVector2)selectedTile, new IntVector2(x, y));
			Debug.Log($"end at {x},{y}");
			foreach(GraphNode n in result) {
				Debug.Log($"{n.x},{n.y}");
			}
		}
	}

	private bool HasWall(int x, int y, Direction d) {
		return false;
	}

	private bool BlockedByOthers(int x, int y) {
		return false;
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

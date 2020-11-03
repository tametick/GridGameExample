using Pathfinding;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;
using Xml2CSharp;
using ExtensionMethods;
using DG.Tweening;

public class MapManager : MonoBehaviour {

	public TextAsset xmlMap;

	public GameObject grassPrefab;
	public GameObject mountainPrefab;
	public GameObject treesPrefab;
	public GameObject rocksPrefab;

	public GameObject playerPrefab;
	public GameObject enemyPrefab;

	public GameObject stepIndicatorPrefab;
	List<GameObject> pathIndicator;

	Pathfinder pathfinder;
	Actor selectedActor;
	private bool ignoreInput;

	void Start() {
		DOTween.Init();

		var map = Deserialize<Map>(xmlMap.text);
		var mapLayer = new MapLayer(map.Layer.Name, map.Layer.Width, map.Layer.Height, map.Layer.Data.Text);
		var mapObjectGroup = new MapObjectGroup(map.Objectgroup.Name, map.Objectgroup.Object);
		
		for(var y=0; y<mapLayer.height; y++) {
			for (var x = 0; x < mapLayer.width; x++) {
				var newTile = Instantiate(GetPrefab(mapLayer.GetTile(x,y)), transform);
				IntVector2 gridPosition = new IntVector2(x, y);
				newTile.transform.localPosition = gridPosition.GridToWorld();
				newTile.name = $"{x},{y}";
				var tile = newTile.AddComponent<Tile>();
				tile.gridPosition = gridPosition;
				tile.OnClickTile = ClickTile;
			}
		}

		pathfinder = new Pathfinder();
		pathfinder.Init(mapLayer.data, new List<int>() { (int)TileType.Grass }, HasWall, BlockedByOthers);
		pathIndicator = new List<GameObject>();

		// player & enemy actors
		foreach(var obj in mapObjectGroup.objects) {
			GameObject newObject= Instantiate(GetObjectPrefab(obj.type), transform);
			newObject.transform.localPosition =obj.gridPosition.GridToWorld();
			var actor = newObject.AddComponent<Actor>();
			actor.data = obj;
			actor.OnClickActor = ClickActor;
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

	private void ClickActor(Actor actor) {
		if (ignoreInput)
			return;

		Debug.Log($"click actor {actor.name} at {actor.gridPosition}");

		// set new player character selection
		if (actor.isPlayer) {
			selectedActor = actor;
		} else if (selectedActor != null) {
			// todo: attack
			Debug.Log($"{selectedActor.name} attack {actor.name}");
		}
	}

	private void ClickTile(IntVector2 gridPosition) {
		if (ignoreInput)
			return;

		if (selectedActor != null) {
			var result = pathfinder.Search(selectedActor.gridPosition, gridPosition);
			Debug.Log($"walk to {gridPosition.x},{gridPosition.y}");
			if (result == null)
				return;
			
			// make sure we have enough step indicators
			while (result.Count > pathIndicator.Count) {
				pathIndicator.Add(Instantiate(stepIndicatorPrefab,transform));
			}

			// disable old path
			foreach (var indicator in pathIndicator) {
				indicator.SetActive(false);
			}

			// set & show new path
			for (int step = 0; step < result.Count; step++) {
				var gridPos = new IntVector2(result[step].x, result[step].y);
				pathIndicator[step].SetActive(true);
				pathIndicator[step].transform.localPosition= gridPos.GridToWorld()+stepIndicatorPrefab.transform.localPosition;

				IntVector2 before, after;

				// rotate relative to next step
				if(step<result.Count-1) {
					before = new IntVector2(result[step].x, result[step].y);
					after = new IntVector2(result[step+1].x, result[step+1].y);
				} else if (step>0) {
					before = new IntVector2(result[step-1].x, result[step-1].y);
					after = new IntVector2(result[step].x, result[step].y);
				} else {
					// rotate relative to origin
					before = selectedActor.gridPosition;
					after = new IntVector2(result[step].x, result[step].y);
				}

				int zRot = 0;
				IntVector2 delta = (after - before);
				switch (delta.ToDirection()) {
					case Direction.Up:
						zRot = 180;
						break;
					case Direction.Down:
						zRot=0;
						break;
					case Direction.Right:
						zRot = 270;
						break;
					case Direction.Left:
						zRot = 90;
						break;
				}
				pathIndicator[step].transform.localEulerAngles = stepIndicatorPrefab.transform.localEulerAngles + new Vector3(0, 0, zRot);
			}

			if (result.Count > 0) {
				ignoreInput = true;
				selectedActor.WalkPath(pathIndicator, ()=> { ignoreInput = false; });
			}
			selectedActor = null;
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

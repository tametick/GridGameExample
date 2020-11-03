using System.Collections.Generic;
using UnityEngine;

internal enum ObjectType {
	Player=5,
	Enemy
}

class MapObject {
	internal int id;
	internal ObjectType type;
	internal IntVector2 gridPosition;

	internal MapObject(int id, ObjectType type, int x, int y) {
		this.id = id;
		this.type = type;
		this.gridPosition = new IntVector2(x, y);
	}

	public override string ToString() {
		return $"MapObject {id}: {type} <{gridPosition.x},{gridPosition.y}>";
	}
}

internal class MapObjectGroup {
	private string name;
	internal List<MapObject> objects { get; private set; }

	internal MapObjectGroup(string name, List<Xml2CSharp.Object> objects) {
		this.name = name;
		this.objects = new List<MapObject>();
		foreach (var o in objects) {
			var w = int.Parse(o.Width);
			var h = int.Parse(o.Height);

			MapObject mapObject = new MapObject(
				int.Parse(o.Id), (ObjectType)int.Parse(o.Gid), int.Parse(o.X) / w, int.Parse(o.Y) / h - 1);
			this.objects.Add(mapObject);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MapObject {
	internal string id;
	internal int x;
	internal int y;

	internal MapObject(string id, int x, int y) {
		this.id = id;
		this.x = x;
		this.y = y;
	}
}

internal class MapObjectGroup {
	private string name;
	private List<MapObject> objects;

	internal MapObjectGroup(string name, List<Xml2CSharp.Object> objects) {
		this.name = name;
		this.objects = new List<MapObject>();
		foreach (var o in objects) {
			var w = int.Parse(o.Width);
			var h = int.Parse(o.Height);

			this.objects.Add(new MapObject(o.Id, int.Parse(o.X)/w, int.Parse(o.Y)/h));
		}
	}
}

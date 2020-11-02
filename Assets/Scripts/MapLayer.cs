using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class MapLayer {
	private string name;
	private int width;
	private int height;
	private string data;//int[,]

	internal MapLayer(string name, string width, string height, string text) {
		this.name = name;
		this.width = int.Parse(width);
		this.height = int.Parse(height);
		this.data = text;
	}
}

using UnityEngine;

internal enum TileType {
	Grass=1,
	Mountain,
	Rocks,
	Tree
}

internal class MapLayer {
	private string name;
	private int width;
	private int height;
	private TileType[,] data;

	internal MapLayer(string name, string width, string height, string dataAsString) {
		this.name = name;
		this.width = int.Parse(width);
		this.height = int.Parse(height);
		data = new TileType[this.width, this.height];

		var lines =dataAsString.Trim().Split('\n');
		for (int y = 0; y < lines.Length; y++) {
			var line = lines[y].Trim().Split(',');
			for (int x = 0; x < line.Length; x++) {
				int tile;
				if(int.TryParse(line[x], out tile)) {
					data[x, y] = (TileType)tile;
				}
			}
		}
	}
}

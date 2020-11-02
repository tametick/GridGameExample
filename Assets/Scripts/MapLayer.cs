using UnityEngine;

internal enum TileType {
	Grass=1,
	Mountain,
	Rocks,
	Tree
}

internal class MapLayer {
	private string name;
	internal int width { get; private set; }
	internal int height { get; private set; }
	internal int[,] data { get; private set; }

	internal MapLayer(string name, string width, string height, string dataAsString) {
		this.name = name;
		this.width = int.Parse(width);
		this.height = int.Parse(height);
		data = new int[this.width, this.height];

		var lines =dataAsString.Trim().Split('\n');
		for (int y = 0; y < lines.Length; y++) {
			var line = lines[y].Trim().Split(',');
			for (int x = 0; x < line.Length; x++) {
				int tile;
				if(int.TryParse(line[x], out tile)) {
					data[x, y] = tile;
				}
			}
		}
	}

	internal TileType GetTile(int x, int y) {
		return (TileType)data[x, y];
	}
}

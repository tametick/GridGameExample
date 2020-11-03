using System;
using UnityEngine;

public class Tile : MonoBehaviour {
	internal IntVector2 gridPosition;
	internal Action<IntVector2> OnClickTile;

	private void OnMouseUp() {
		OnClickTile(gridPosition);
	}
}

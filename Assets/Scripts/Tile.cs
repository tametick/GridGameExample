using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
	internal int x;
	internal int y;
	internal Action<int, int> OnClickTile;

	private void OnMouseUp() {
		OnClickTile(x,y);
	}
}

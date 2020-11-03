using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
	internal int x;
	internal int y;
	internal Action<IntVector2> OnClickTile;

	private void OnMouseUp() {
		OnClickTile(new IntVector2(x,y));
	}
}

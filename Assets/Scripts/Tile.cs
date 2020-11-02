using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
	internal int x;
	internal int y;

	private void OnMouseUp() {
		Debug.Log(x+","+y );
	}
}

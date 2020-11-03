using UnityEngine;

namespace ExtensionMethods {
	public static class PositionExtensions {
		public static IntVector2 WorldToGrid(this Vector3 worldPosition) {
			return new IntVector2((int) worldPosition.x, (int) -worldPosition.z);
		}
		public static Vector3 GridToWorld(this IntVector2 gridPosition) {
			return new Vector3(gridPosition.x, 0, -gridPosition.y);
		}
	}
}
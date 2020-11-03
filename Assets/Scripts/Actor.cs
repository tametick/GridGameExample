using System;
using UnityEngine;

public class Actor : MonoBehaviour {
    internal MapObject data;
    internal Action<Actor> OnClickActor;

    internal bool isPlayer {
        get {
            return data.type == ObjectType.Player;
        }
    }

    internal IntVector2 gridPosition {
        get {
            return data.gridPosition;
        }
        set {
            data.gridPosition = value;
        }
    }

    private void OnMouseUp() {
        OnClickActor(this);
    }
}

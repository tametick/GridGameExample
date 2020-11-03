using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

	internal void WalkPath(List<GameObject> pathIndicator) {
        Sequence walkSequence = DOTween.Sequence();
        foreach(var stepIndictaor in pathIndicator) {
            var destination = new Vector3(
                stepIndictaor.transform.localPosition.x,
                transform.localPosition.y,
                stepIndictaor.transform.localPosition.z
            );
            walkSequence.Append(transform.DOLocalMove(destination, 0.25f));
        }
    }
}

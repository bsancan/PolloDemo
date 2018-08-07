
using UnityEngine;

public class SimpleMove : MonoBehaviour {
    public float SpeedForward;
    public float Speed;
    protected virtual void LateUpdate() {

        transform.position = transform.position + (transform.forward * SpeedForward * Time.deltaTime);
        // This will move the current transform based on a finger drag gesture
        TouchSystem.MoveObjectInX(transform, TouchSystem.DragDelta, Speed);
    }
}

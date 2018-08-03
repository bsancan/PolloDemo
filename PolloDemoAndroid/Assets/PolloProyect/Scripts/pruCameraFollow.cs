using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pruCameraFollow : MonoBehaviour {

    public Transform Target;
    public float SmoothSpeed;
    public Vector3 Offset;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //private void LateUpdate()
    //{
    //    Vector3 desiredPosition = Target.position + Offset;
    //    Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, SmoothSpeed * Time.deltaTime);
    //    transform.position = smoothedPosition;
    //    transform.LookAt(Target);

    //}

    private void FixedUpdate()
    {
        Vector3 desiredPosition = Target.position + Offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, SmoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
        transform.LookAt(Target);
    }
}

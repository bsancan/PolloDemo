using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pruDrag : MonoBehaviour {
    Vector3 touchPosition;
    Touch _touch;
     Rigidbody rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.touchCount > 0) {
            _touch = Input.GetTouch(0);

            if (_touch.phase == TouchPhase.Began) {
                print(_touch.position);
                touchPosition = Camera.main.ScreenToWorldPoint(_touch.position);
                rb.position = new Vector3(touchPosition.x, 1f, rb.position.z);
            }
        }

	}
}

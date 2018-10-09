using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCamera : MonoBehaviour {

    [SerializeField]
    private Transform character;
    public float speedCamera = 4f;

    private Vector3 cameraOffset;

    void Start () {
        //cameraOffset = new Vector3(transform.position.x, transform.position.y, Mathf.Abs(transform.position.z));
        cameraOffset = transform.localPosition;
	}
	

	void Update () {
        //Vector3 desiredPosition = (character.localPosition * 0.3f) + cameraOffset;
        Vector3 desiredPosition = new Vector3(character.localPosition.x,
            character.localPosition.y, character.localPosition.z) + cameraOffset;

        transform.localPosition = Vector3.Lerp(transform.localPosition, desiredPosition, Time.deltaTime * speedCamera);
    }
}

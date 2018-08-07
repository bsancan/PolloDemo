using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

    public Transform AsteroidModel;
    public int ValueDamage;
    public float SpeedForward;
    public float SpeedRotation;
    public bool EnableAutoDirection;

    private Vector3 asteroidDirection;
    // Use this for initialization
    void Start () {
        if (EnableAutoDirection) {
            GetRandomDirection();
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (!EnableAutoDirection) {
            asteroidDirection = Vector3.right;
        }

        AsteroidModel.rotation *= Quaternion.AngleAxis(SpeedRotation * Time.deltaTime, asteroidDirection);

        if (SpeedForward != 0f)
            transform.position += transform.forward * SpeedForward * Time.deltaTime;
    }

    void GetRandomDirection()
    {
        int r = Random.Range(0, 6);

        if (r == 1) //derecha
            asteroidDirection = Vector3.right;
        else if (r == 2) // izquier
            asteroidDirection = -Vector3.up;
        else if (r == 3)
            asteroidDirection = Vector3.forward;
        else if (r == 4)
            asteroidDirection = -Vector3.forward;
        else if (r == 5) // arriba
            asteroidDirection = Vector3.up;
        else if (r == 6) // abajo
            asteroidDirection = -Vector3.up;
    }
}

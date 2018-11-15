using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrSpaceStationControll : MonoBehaviour {
    public float speed;
    public float speedAngular;
    private Rigidbody rb;

    void Awake(){
        rb = GetComponent<Rigidbody>();
    }

    void Start () {
        
        //rb.angularVelocity = Random.insideUnitCircle * tumble;
    }

    void FixedUpdate(){
        transform.Rotate(Vector3.up, speedAngular * Time.deltaTime);
        //rb.rotation = Quaternion.AngleAxis(0, Vector3.up * Time.deltaTime);
        //rb.rotation = Quaternion.Euler(350, tumble * Time.deltaTime, 15);
        rb.velocity = new Vector3(0, 0, speed);

    }
}

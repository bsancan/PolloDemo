using UnityEngine;
using System.Collections;

public class MisilControl : MonoBehaviour {

    public float speed;
    Rigidbody rb;

    Transform playerPos;
    GameObject player;
    float rotSpeed = 90f;
    public Vector3 playerFirstPos;

	void Start () {
        player = GameObject.Find("Player");
        playerPos = player.GetComponent<Transform>();
       rb  = GetComponent<Rigidbody>();
	}
	
	void Update () {
	
//        if (player == null)
//        {
//            Debug.Log("NO");
//            return;
//        }
//        Vector3 dir = playerPos.position - transform.position;
//        dir.Normalize();
//
//        float zAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Deg2Rad - 90;
//
//        Quaternion desireRot = Quaternion.Euler(0, 0, zAngle);
//
//        transform.rotation = Quaternion.RotateTowards(transform.rotation, desireRot, rotSpeed * Time.deltaTime);

	}

    void FixedUpdate(){
        Vector3 offset;
        // obtengo la posicion del player conrespecto a la nave que dispara
        //offset = playerFirstPos - rb.transform.position;
        rb.velocity = transform.TransformDirection(Vector3.back * speed);

//        rb.AddRelativeForce(offset * speed);

        //rb.velocity = offset.normalized * speed;
            
        //rb.AddForceAtPosition(offset.normalized, playerPos.position);

        //rb.AddForce(offset ,ForceMode.Acceleration);
        //rb.velocity = speed * (rb.velocity.normalized);

//        Vector3 offset = rb.position - playerPos.position;
//
//        if (offset.z >= 5f)
//        {
//            rb.position = Vector3.Lerp(rb.position, playerPos.position, Time.deltaTime * 2f);
//        }
//        else
//        {
//            Vector3 offset2 = new Vector3(rb.position.x, rb.position.y, -20f);
//            rb.position = Vector3.Lerp(rb.position, offset2, Time.deltaTime * 2f);
//        }

//        rb.velocity = speed * (rb.velocity.normalized);

        //rb.velocity = new Vector3(0, 0, speed);
//        rb.velocity -= new Vector3(0, 0, rb.position.z * speed);
//        rb.velocity = new Vector3(0, 0, rb.position.z * ( Time.deltaTime * speed));
//        transform.position = Vector3.Lerp(transform.position
//            , new Vector3(transform.position.x, transform.position.y
//            , -30)
//            , (Time.deltaTime * speed));
    }


}

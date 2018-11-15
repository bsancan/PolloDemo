using UnityEngine;
using System.Collections;

public class MoveLaser : MonoBehaviour
{
	//public Transform targetPoint;
	Transform localPoint;

	Rigidbody rb;
	public float speed;
	//Vector3 _targetPoint;

	void Start ()
	{
		//targetPoint = GameObject.Find ("Player").GetComponent<Transform> ();
	
		//_targetPoint = targetPoint.position;
		rb = GetComponent<Rigidbody> ();

	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void FixedUpdate(){
		rb.velocity = transform.rotation * (Vector3.forward * speed);



//		rb.position = Vector3.MoveTowards (rb.position, _targetPoint, Time.deltaTime * speed);
//		rb.velocity = new Vector3(0, 0, speed);

	}
}


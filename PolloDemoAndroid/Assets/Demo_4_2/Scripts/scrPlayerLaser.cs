using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrPlayerLaser : MonoBehaviour {

    public float speed;
    public float timeLife;
    private Rigidbody rb;

	void Start () {
        rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
    void FixedUpdate () {
        
        rb.velocity = transform.rotation * (Vector3.forward * speed);
	}

    public void StartLaser(){
        this.StopCoroutine("Move");
        this.StartCoroutine(Move());
    }

    IEnumerator Move(){

        yield return new WaitForSeconds(timeLife);

        if (gameObject.activeInHierarchy)
            gameObject.SetActive(false);
        //Destroy(gameObject);

    }
}

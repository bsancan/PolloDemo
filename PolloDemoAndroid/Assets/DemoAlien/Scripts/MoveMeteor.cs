using UnityEngine;
using System.Collections;


public class MoveMeteor : MonoBehaviour
{
	private Rigidbody rb;
	public float speed;
	public float tumble;

	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		rb.angularVelocity = Random.insideUnitSphere * tumble;
	}
	

	void Update ()
	{
		
	}

	void FixedUpdate(){
		rb.velocity = new Vector3(0, 0, speed);

	}

    public void MeteorGrowUp(float _scale) {
        StartCoroutine(StartMeteorGrowUp(_scale));   
    }

    IEnumerator StartMeteorGrowUp(float _scale) {
        float countScale = 10;
        while (countScale < _scale)
        {
            countScale = countScale + (Time.deltaTime * _scale * 4);
            transform.localScale = new Vector3(countScale, countScale, countScale);

            yield return new WaitForSeconds(0.1f);
        }
    }
}


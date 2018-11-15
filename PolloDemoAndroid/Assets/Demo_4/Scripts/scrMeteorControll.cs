using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrMeteorControll : MonoBehaviour {
    public scrGameControll gameControll;
    public GameObject mtrChild;
    public int meteorID;
    public bool isMeteorSelected;
    //public GameObject meteorExplotion;
    public float speedMeteor;
    public float tumbleMeteor;
    public int damagePoint;
    private Rigidbody rb;


    void Awake(){
        rb = GetComponent<Rigidbody>();

    }

    void Start () {
        
        rb.angularVelocity = Random.insideUnitCircle * tumbleMeteor;
	}
	
	
	void Update () {
		
	}

    void FixedUpdate(){
        rb.velocity = new Vector3(0, 0, speedMeteor);

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
//            GameObject exp = gameControll.GetObjPoolExplotion();
//            exp.transform.position = gameObject.transform.position;
//            exp.SetActive(true);
//            exp.GetComponent<scrDestroyByTime>().StartLifeTime(0.5f);

			//if (!gameObject.CompareTag("MeteorPath"))
              //  gameObject.SetActive(false);
            gameObject.SetActive(false);
            gameControll.TakeDamage(damagePoint);

            //other.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {

        //if (gameObject.CompareTag("Path"))
        //    return;

        if (other.CompareTag("Laser"))
        {
            //          objHealth.TakeDamage (damage);
            //gameObject.GetComponent<MoveMeteor> ().speed = 0;
            //objSpawn.StartExplotionAsteroid(transform);
            //Instantiate (meteorExplotion, transform.position, transform.rotation);
            //gameControll = GameObject.Find("GameControll").GetComponent<scrGameControll>();

            //des parpruebas
//            GameObject exp = gameControll.GetObjPoolExplotion();
//            exp.transform.position = gameObject.transform.position;
//            exp.SetActive(true);
//            exp.GetComponent<scrDestroyByTime>().StartLifeTime(0.5f);

            if (!gameObject.CompareTag("MeteorPath"))
                gameObject.SetActive(false);

            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("Player"))
        {
            //des para pruebas
//            GameObject exp = gameControll.GetObjPoolExplotion();
//            exp.transform.position = gameObject.transform.position;
//            exp.SetActive(true);
//            exp.GetComponent<scrDestroyByTime>().StartLifeTime(0.5f);

            if (!gameObject.CompareTag("MeteorPath"))
                gameObject.SetActive(false);

            gameControll.TakeDamage(damagePoint);
  
            //other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("Boundary"))
        {
//            if (!mtrChild.activeInHierarchy)
//                mtrChild.SetActive(true);

            if (!gameObject.activeInHierarchy)
                gameObject.SetActive(true);
        }
    }

    public void DestroyMeteor(){
        //Instantiate (meteorExplotion, transform.position, transform.rotation);
        gameObject.SetActive (false);

    }
    //sin usar
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

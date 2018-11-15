using UnityEngine;
using System.Collections;

public class DestroyByContactMeteor : MonoBehaviour
{
	//public GameObject explosion;
	public GameObject playerExplosion;
	public int damage;

	HealthControl objHealth;
    SpawnWave objSpawn;

	void Start ()
	{
        
		objHealth = GameObject.Find ("GameControl").GetComponent<HealthControl> ();
        objSpawn = GameObject.Find ("GameControl").GetComponent<SpawnWave> ();
	}


	void OnTriggerEnter(Collider other)
	{
        //pruebas nivel 1 fase 2 naves
        if (gameObject.CompareTag("barras") && other.CompareTag("Enemy")){
            
            other.gameObject.SetActive(false);
            //other.gameObject.GetComponent<Transform>().localScale = new Vector3(3f, 3f, 3f);
            //objSpawn.StartShipA();

            return;
        }

        //Explotion
        if (other.CompareTag ("Boundary") || other.CompareTag("Enemy")) {
			return;
		}

        if  (other.CompareTag("Rocket")) {
            return;
        }

        if  (other.CompareTag("LaserEnemy")) {
            return;
        }

        if (other.CompareTag("MineEnemy"))
        {
            return;
        }

        //if (!other.CompareTag("Meteor"))
        //if (other.CompareTag("Laser"))
        //{
        //    //Vector3 vector = GameObject.Find("MainCamera").GetComponent<Camera>().WorldToScreenPoint(transform.position);
        //    objSpawn.StartExplotionAsteroid(transform);


        //}
        /*

        if (explosion != null && !other.CompareTag ("Meteor")) {
			Vector3 vector = GameObject.Find ("MainCamera").GetComponent<Camera> ().WorldToScreenPoint (transform.position);
            GameObject exp = null;
//            exp.SetActive(false);

            exp = explosion;
//            exp.GetComponent<ParticleSystem>().Clear();
//            exp.GetComponent<ParticleSystem>().Play();
            exp.SetActive(true);
            exp.GetComponent<DestroyByTime>().LifeTime(1.5f);
			//Instantiate (explosion, transform.position, transform.rotation);
			//Instantiate (gameController.pnlPoints, vector, new Quaternion(0,0,0,0));
		}


        */
		if (other.CompareTag ("Player") && gameObject.CompareTag ("LaserEnemy")) {
            objSpawn.StartExplotionAsteroid(transform);
            gameObject.SetActive (false);
//			objHealth.TakeDamage (damage);
        }else if (other.CompareTag("Player") && gameObject.CompareTag("MineEnemy"))
        {
            objSpawn.StartExplotionAsteroid(other.gameObject.transform);
            gameObject.SetActive(false);
            //			objHealth.TakeDamage (damage);
        }
        else if (other.CompareTag("Player") && gameObject.CompareTag("Meteor"))
        {
            objSpawn.StartExplotionAsteroid(transform);
            gameObject.SetActive(false);

        }

        else if (other.CompareTag ("Player") && gameObject.CompareTag ("Rocket")) {
            gameObject.SetActive (false);
            gameObject.GetComponent<MisilControl> ().speed = 0;
            gameObject.GetComponent<MisilControl>().playerFirstPos = new Vector3();
        }else if (other.CompareTag ("Laser") && gameObject.CompareTag("Rocket")) {
            //          objHealth.TakeDamage (damage);
            objSpawn.StartExplotionAsteroid(transform);
            gameObject.SetActive (false);
            gameObject.GetComponent<MisilControl> ().speed = 0;
            gameObject.GetComponent<MisilControl>().playerFirstPos = new Vector3();
        }
        else if (other.CompareTag ("Player")) {
//			objHealth.TakeDamage (damage);
			gameObject.GetComponent<MoveMeteor> ().speed = 0;
			gameObject.SetActive (false);
        }else if (other.CompareTag ("Laser")) {
            //          objHealth.TakeDamage (damage);
            //gameObject.GetComponent<MoveMeteor> ().speed = 0;
            objSpawn.StartExplotionAsteroid(transform);
            gameObject.SetActive (false);
            other.gameObject.SetActive(false);
        }
			

//
//		if (other.tag == "Laser") {
//
//			Destroy (other.gameObject);
//		}

//		if (other.tag == "Player") {
//			goPlayer.TakeDamage (scoreValue);
//			goPlayerC.Damage01 ();
//			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
//			gameController.GameOver ();
//		}
	


		//Destroy (other.gameObject);
//		Destroy (gameObject);
	}
}


using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour {

	void OnTriggerExit(Collider other)
	{
		if (!other.CompareTag ("Player")) {
			if (other.CompareTag ("Meteor")) {
				other.gameObject.SetActive (false);
				other.GetComponent<MoveMeteor> ().speed = 0;
            } else if (other.CompareTag ("Rocket")) {
                other.gameObject.SetActive (false);
                other.GetComponent<MisilControl> ().speed = 0;
            }
            else if (other.CompareTag ("LaserEnemy")) {
                other.GetComponent<MoveLaser>().speed = 0;
                other.gameObject.SetActive(false);
                //Destroy (other.gameObject);
            }else if (other.CompareTag ("Item")) {
                Destroy (other.gameObject);
            } else if (other.CompareTag ("Laser")) {
                other.GetComponent<MoveLaser>().speed = 0;
                other.gameObject.SetActive(false);
            }  else if (other.CompareTag ("Explotion")) {
                
                other.gameObject.SetActive(false);
            }
            else if (other.CompareTag("MineEnemy"))
            {
                other.GetComponent<MoveLaser>().speed = 0;
                other.gameObject.SetActive(false);
                //Destroy (other.gameObject);
            }
        }
//			Destroy (other.gameObject);
		
			
	}
}

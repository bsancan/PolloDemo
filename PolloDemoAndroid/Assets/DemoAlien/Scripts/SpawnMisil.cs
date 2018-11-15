using UnityEngine;
using System.Collections;

public class SpawnMisil : MonoBehaviour {
    
    public GameObject misil;

   

    void OnTriggerEnter(Collider other){
        
        if (other.CompareTag("Enemy")) {
//            GameObject goMisil = (GameObject)
                Instantiate (misil, transform.position, transform.rotation);
//            goMisil.GetComponent<MisilControl>().speed = 4f;

//            misil.GetComponent<Transform>().forward;
        }
    }

  
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class scrDestroyByBoundary : MonoBehaviour {

    void OnTriggerExit(Collider other){
        if (!other.CompareTag("Player"))
        {
            if (other.CompareTag ("Meteor")) {
                Debug.Log("trigger" + other.gameObject.name);
                //other.gameObject.GetComponent<scrMeteorControll>().speedMeteor = 0f;

                other.gameObject.GetComponent<scrMeteorControll> ().isMeteorSelected =false;

                other.gameObject.SetActive (false);
            }else if (other.CompareTag("MeteorPath"))
            {
                Debug.Log("trigger" + other.gameObject.name);
                //other.gameObject.GetComponent<scrMeteorControll>().speedMeteor = 0f;

                other.gameObject.GetComponent<scrMeteorControll>().isMeteorSelected = false;

                other.gameObject.SetActive(false);
            }else if (other.CompareTag("Laser"))
            {
                Debug.Log("trigger" + other.gameObject.name);
                other.gameObject.SetActive(false);
            }
            else if (other.CompareTag("Energy"))
            {
                Debug.Log("trigger" + other.gameObject.name);
                other.gameObject.SetActive(false);
            }


        }
    }
}

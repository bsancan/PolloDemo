using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrDestroyByTime : MonoBehaviour {
    //public float lifeTime;

    void Start () {
        //Destroy (gameObject, lifeTime);
    }

    public void StartLifeTime(float t){
        StartCoroutine(WaitLifeTime(t));
    }

    IEnumerator WaitLifeTime(float t)
    {
        yield return new WaitForSeconds(t);

        gameObject.SetActive(false);
    }
}

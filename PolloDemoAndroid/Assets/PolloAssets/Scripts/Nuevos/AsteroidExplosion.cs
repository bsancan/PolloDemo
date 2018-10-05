using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidExplosion : MonoBehaviour
{
    private float lifeTime = 1f;

    private void OnEnable()
    {
        CancelInvoke("AsteroidDie");
        Invoke("AsteroidDie", lifeTime);
    }

    void AsteroidDie()
    {
        //CancelInvoke();
        gameObject.SetActive(false);
        //gameObject.SetActive(false);
        //Destroy(gameObject);
    }
}

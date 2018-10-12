using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidExplosion : MonoBehaviour
{
    private float lifeTime = 1f;

    [HideInInspector]
    public Vector3 initialScale;

    private void Awake()
    {
        initialScale = transform.localScale;
    }

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

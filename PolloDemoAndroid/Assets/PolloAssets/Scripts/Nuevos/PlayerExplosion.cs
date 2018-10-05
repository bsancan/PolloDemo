using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExplosion : MonoBehaviour
{
    private float lifeTime = 1f;


    private void OnEnable()
    {
        CancelInvoke("PlayerExplosionDie");
        Invoke("PlayerExplosionDie", lifeTime);
    }

    void PlayerExplosionDie()
    {
        //CancelInvoke();
        gameObject.SetActive(false);
        //gameObject.SetActive(false);
        //Destroy(gameObject);
    }
}

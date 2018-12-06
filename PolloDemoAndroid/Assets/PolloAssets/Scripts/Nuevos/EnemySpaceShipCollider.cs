using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpaceShipCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAmmo"))
        {
            ExplosionManager.explosionManagerInstance.SpawnSpaceShipExplosion(transform.position);
            transform.parent.gameObject.SetActive(false);
            //gameObject.SetActive(false);
        }
    }
}

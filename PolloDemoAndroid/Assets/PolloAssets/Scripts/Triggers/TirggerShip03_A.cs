using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TirggerShip03_A : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            EnemySpaceShipManager.enemySpaceShipManagerInstance.StartEnemySpawn03_A();
            Destroy(gameObject);
        }

    }
}

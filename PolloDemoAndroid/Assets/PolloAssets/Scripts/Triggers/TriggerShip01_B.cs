using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerShip01_B : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            EnemySpaceShipManager.enemySpaceShipManagerInstance.StartEnemySpawn01_B();
            Destroy(gameObject);
        }
 
    }
}

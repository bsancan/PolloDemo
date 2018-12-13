using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerShip02_A : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            EnemySpaceShipManager.enemySpaceShipManagerInstance.StartEnemySpawn02_A();
            Destroy(gameObject);
        }

    }
}

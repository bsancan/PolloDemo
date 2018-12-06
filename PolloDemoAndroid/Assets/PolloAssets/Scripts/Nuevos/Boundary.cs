using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemySpaceShip"))
        {
            other.transform.parent.gameObject.SetActive(false);

        }else
        {
            other.gameObject.SetActive(false);
        }



    }
}

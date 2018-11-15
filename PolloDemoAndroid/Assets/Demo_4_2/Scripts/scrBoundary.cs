using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace juego
{
    [RequireComponent(typeof(Collider))]
    public class scrBoundary : MonoBehaviour
    {



        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Meteor") || other.CompareTag("Item"))
            {
                other.gameObject.SetActive(false);
            }
        }


    }
}

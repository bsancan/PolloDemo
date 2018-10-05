using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

    public Transform AsteroidModel;
    public int valueDamage = 20;
    public int valueStamina = 20;
    public float speedForward = 5;
    public float speedRotation = 10;
    public bool enableAutoDirection;

    private Vector3 asteroidDirection;
    private int currentStamina;

    void Start () {
        if (enableAutoDirection) {
            GetRandomDirection();
        }
       
	}

    private void OnEnable()
    {
        currentStamina = valueStamina;
    }


    void Update () {
        if (!enableAutoDirection) {
            asteroidDirection = Vector3.right;
        }

        AsteroidModel.rotation *= Quaternion.AngleAxis(speedRotation * Time.deltaTime, asteroidDirection);

        if (speedForward != 0f)
            transform.position += transform.forward * speedForward * Time.deltaTime;
    }

    void GetRandomDirection()
    {
        int r = Random.Range(0, 6);

        if (r == 1) //derecha
            asteroidDirection = Vector3.right;
        else if (r == 2) // izquier
            asteroidDirection = -Vector3.up;
        else if (r == 3)
            asteroidDirection = Vector3.forward;
        else if (r == 4)
            asteroidDirection = -Vector3.forward;
        else if (r == 5) // arriba
            asteroidDirection = Vector3.up;
        else if (r == 6) // abajo
            asteroidDirection = -Vector3.up;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAmmo"))
        {
            TakeDamage(other.GetComponent<PlayerAmmo>().valueDamage);
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            ExplosionManager.explosionManagerInstance.SpawnExplosion(transform.position);
        }
    }

    private void TakeDamage(int damageFromPlayer)
    {
        int sum = currentStamina - damageFromPlayer;
        if(sum > 0)
        {
            currentStamina = sum;
            UIManager.uiManagerInstance.ShowNegativePoints(damageFromPlayer, transform.position);

            //print(currentStamina);
        }
        else
        {
            if (gameObject.activeInHierarchy)
            {
                gameObject.SetActive(false);
                UIManager.uiManagerInstance.ShowNegativePoints(damageFromPlayer, transform.position);
                ExplosionManager.explosionManagerInstance.SpawnExplosion(transform.position);

            }
            currentStamina = 0;
        }

           }
}

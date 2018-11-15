using UnityEngine;
using System.Collections;

public class ItemControl : MonoBehaviour {
    public int itemID;
    public int healthAmount;
    public float fireAmount;
    public float speed;

    HealthControl objHealth;
    Rigidbody rb;

    void Start ()
    {
        objHealth = GameObject.Find ("GameControl").GetComponent<HealthControl> ();
        rb = GetComponent<Rigidbody> ();

    }

    void FixedUpdate(){
        rb.velocity = new Vector3(0, 0, speed);

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag ("Boundary") || other.CompareTag("Enemy") || other.CompareTag("Item")) {
            return;
        }

        if (other.CompareTag ("Player")) {
         
            if (itemID == 1)
            {
                int healmax = (int)objHealth.healthSlider.maxValue;
                int canH = objHealth.currentHealth + healthAmount;
                if (canH > healmax)
                    objHealth.currentHealth = healmax;
                else
                    objHealth.currentHealth += healthAmount;

                objHealth.healthSlider.value = objHealth.currentHealth;
            }
            else if (itemID == 2)
            {
                
            }

            Destroy(gameObject);
        }

    }
}

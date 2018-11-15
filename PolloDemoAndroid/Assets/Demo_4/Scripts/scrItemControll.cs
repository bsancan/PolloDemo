using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrItemControll : MonoBehaviour {
    public scrGameControll gameControll;
    public GameObject itemChild;
    public int itemID;
    public int amount;
    public float speed;
    public  float tumble;
    private Rigidbody rb;

    void Awake(){
        rb = GetComponent<Rigidbody>();
    }

	void Start () {
        rb.angularVelocity = Random.insideUnitCircle * tumble;
	}
	
	void Update () {
		
	}

    void FixedUpdate(){
        rb.velocity = new Vector3(0, 0, speed);
    }

    void OnTriggerEnter(Collider other){
       
        if (other.CompareTag("Player"))
        {
            Debug.Log("PLAYER");
            if (gameObject.CompareTag("Energy"))
            {
                //Debug.Log("E");
                int energyMax = (int)gameControll.status.sldEnergy.maxValue;
                int canH = gameControll.status.currentEnergy + amount;
                if (canH > energyMax)
                {
                    gameControll.status.currentEnergy = energyMax;
                }
                else
                {
                    gameControll.status.currentEnergy += amount;
                }

                gameControll.status.sldEnergy.value = gameControll.status.currentEnergy;
            }
   

            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Boundary"))
        {
//            if (!itemChild.activeInHierarchy)
//                itemChild.SetActive(true);

            if (!gameObject.activeInHierarchy)
                gameObject.SetActive(true);
        }
    }

    //-----------<   >-----------
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour {

    public static CharacterManager characterManagerInstance;
    public Camera mainCamera;                       //Camara hija que se seguira al player
    public Character character;                     //clase que controla los movimientos del player junto a colliders
    

    //public CharacterCollider characterCollider;

    public float speedZ = 6.0f;                  // Velocidad de movimiento en XY del player

    public int maxLife = 3;

    public bool startMovement = false;

    void Awake()
    {
        
    }


    // Use this for initialization
    void Start () {


        if (characterManagerInstance == null)
        {
            characterManagerInstance = this;
        }
        else
        {

            Destroy(gameObject);
        }

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);


    }

    private void Update()
    {
        if (startMovement)
        {
            transform.position += transform.forward * Time.deltaTime * speedZ;
        }
    }


}

   



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour {

    public static CharacterManager characterManagerInstance;
    public Camera mainCamera;                       //Camara hija que se seguira al player
    public Character character;                     //clase que controla los movimientos del player junto a colliders
    public bool playerIsDead;

    //public CharacterCollider characterCollider;

    public float speedZ = 6.0f;                  // Velocidad de movimiento en XY del player

    public int maxLife = 3;
    
    public bool startMovement = false;

    private Level02 level02;                        //variale que se busca en el 2do nivel
    private Quaternion initialRotationForTunnel;             //Rotacion para seguir la animacion del tunel
    private bool startTunnelAnimation;                          //el player comienza a seguir el camino del animacion
    private float speedRotationForTunnel;                       //velocidad para suavizar la rotacion de la animacion del tunel 

    void Awake()
    {
        startTunnelAnimation = false;
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
       // DontDestroyOnLoad(gameObject);


    }

   

    private void Update()
    {
        if (GameManager.gameManagerInstance.currentLevel == 1 && startMovement)
        {
            transform.position += transform.forward * Time.deltaTime * speedZ;
        }
        else if (GameManager.gameManagerInstance.currentLevel == 2 && !startMovement && startTunnelAnimation)
        {
            MovingCharacterOnTunnel();
        }
    }

    public void SetInitialTunnelValues() {
        level02 = GameObject.FindObjectOfType<Level02>();
        initialRotationForTunnel = Quaternion.Euler(0, 90.05801f, 0);
       
        character.transform.position = Vector3.zero;
        StartCoroutine(CorStartTunnelAnimation());
      
    }

    private void MovingCharacterOnTunnel()
    {

        Quaternion newRot = Quaternion.Euler(level02.tunnel.rotation.eulerAngles - initialRotationForTunnel.eulerAngles);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, newRot, speedRotationForTunnel * Time.deltaTime);

        ////Diferente de AXIS 
        //if (xAxis != 0f && yAxis != 0f)
        //{
        //    //xy = (transform.right * xAxis * distanceX) + (transform.up * yAxis * distanceY);
        //    //r = transform.right * xAxis * distanceX;
        //    Vector3 xy0 = (transform.right * xAxis * distanceX) + (transform.up * yAxis * distanceY);
        //    //xy = Vector3.Lerp(xy, xy0, playerSpeedB * Time.deltaTime);
        //    xy = Vector3.Lerp(xy, xy0, playerSpeedB * Time.deltaTime);
        //}

        //Vector3 fixedPosition = traPath.position + xy;
        transform.position = level02.tunnel.position;

    }


    IEnumerator CorStartTunnelAnimation() {
        yield return null;

        while (level02 == null) {
            yield return null;
        }
        transform.position = level02.tunnel.position;
        startTunnelAnimation = true;
    }

}

   



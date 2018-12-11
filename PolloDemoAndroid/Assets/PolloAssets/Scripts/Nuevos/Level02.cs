using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level02 : MonoBehaviour
{
    public Transform tunnel;
    private Animator tunnelAnimator;
    private Animation tunnelAnimation;

    private bool startTunnelAnimation;                          //el player comienza a seguir el camino del animacion
    [SerializeField]
    private float speedRotationForTunnel = 20f;                       //velocidad para suavizar la rotacion de la animacion del tunel 
    private Quaternion initialRotationForTunnel;             //Rotacion para seguir la animacion del tunel


    private void Awake()
    {
        startTunnelAnimation = false;
        tunnelAnimation = tunnel.GetComponent<Animation>();
    }
    // Start is called before the first frame update
    void Start()
    {
        SetInitialTunnelValues();
    }

    // Update is called once per frame
    void Update()
    {
         if (GameManager.gameManagerInstance.currentLevel == 2 && startTunnelAnimation)
        {
            MovingCharacterOnTunnel();
        }
    }
    private void MovingCharacterOnTunnel()
    {

        Quaternion newRot = Quaternion.Euler(tunnel.rotation.eulerAngles - initialRotationForTunnel.eulerAngles);
        CharacterManager.characterManagerInstance.transform.rotation = Quaternion.RotateTowards(CharacterManager.characterManagerInstance.transform.rotation, newRot, speedRotationForTunnel * Time.deltaTime);

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
        CharacterManager.characterManagerInstance.transform.position = tunnel.position;
        //Quaternion newRot2 = Quaternion.Euler(CharacterManager.characterManagerInstance.character.transform.localRotation.eulerAngles + CharacterManager.characterManagerInstance.transform.localRotation.eulerAngles);
        //CharacterManager.characterManagerInstance.character.transform.localRotation = newRot2;

    }

    private void SetInitialTunnelValues()
    {

        initialRotationForTunnel = Quaternion.Euler(0, 90.05801f, 0);
        startTunnelAnimation = true;
        tunnelAnimation.Play("Anm_ne_esc");
        tunnel.GetComponent<Renderer>().enabled = false;
    }




}

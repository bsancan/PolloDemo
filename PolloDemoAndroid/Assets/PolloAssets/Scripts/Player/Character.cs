using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Mainly used as a data container to define a character. This script is attached to the prefab
/// (found in the Bundles/Characters folder) and is to define all data related to the character.
/// </summary>
public class Character : MonoBehaviour {

    public string characterName;                    //nombre del player

    public Animator characterAnimator;              //animacion del player

    [SerializeField]
    private Transform characterModel;               //modelo fbx

    [SerializeField]
    private Transform spawnSpotLeft;
    [SerializeField]
    private Transform spawnSpotRight;

    [SerializeField]
    private Vector2 characterRangeMin;              //Rango minimo en XY del player sobre el mundo/local
    [SerializeField]
    private Vector2 characterRangeMax;              //Rango máximo en XY del player sobre el mundo/local

    [SerializeField]
    private float moveSpeed = 20f;                  // Velocidad de movimiento en XY del player
    [SerializeField]
    private float rotationSpeed = 10f;                //Velocidad de rotación del player
    [SerializeField]
    private float crossHairSpeed = 3f;               //Velocidad del CrossHair para desplazarse sobre la pantalla
    [SerializeField]
    private float distanceRaycast = 100f;             //Distancia para el uso del Raycast

    //======Animaciones del player
    static int s_DeadHash = Animator.StringToHash("Dead");
    static int s_ShotHash = Animator.StringToHash("Shot");
    static int s_MovingHash = Animator.StringToHash("MovingXY");
    static int s_HitHash = Animator.StringToHash("Hit");

    //=====Obtienen los valores obtenidos de los controles
    private Vector3 leftJoystickInput = Vector3.zero;
    private Vector3 rightJoystickInput = Vector3.zero;

    public float fireRate;
    private float nextFire;


    void Start () {
		
	}
	

	void Update () {

        leftJoystickInput = UIManager.uiLevelsInstance.leftJoystick.GetInputDirection();
        rightJoystickInput = UIManager.uiLevelsInstance.rightJoystick.GetInputDirection();

        MovingCharacterXY();
        MovingCrosshair();
    }

    private void MovingCharacterXY()
    {

        // if there is no input on the left joystick
        if (leftJoystickInput == Vector3.zero)
        {
            characterAnimator.SetFloat(s_MovingHash, 0f);
        }

        // if there is only input from the left joystick
        if (leftJoystickInput != Vector3.zero)
        {
            Vector3 accelXY = new Vector3(leftJoystickInput.x, leftJoystickInput.y, 0f) * moveSpeed;
            Vector3 velocity = transform.localPosition + accelXY * Time.deltaTime;
            Vector3 velFixed = new Vector3(
                Mathf.Clamp(velocity.x, -characterRangeMin.x, characterRangeMax.x),
                 Mathf.Clamp(velocity.y, -characterRangeMin.y, characterRangeMax.y),
                 0f
                );
            transform.localPosition = velFixed;
            characterAnimator.SetFloat(s_MovingHash, leftJoystickInput.x);


            //aniPlayer.SetFloat(horizontalHash, xAxis, 0.1f, animationSpeed * Time.deltaTime);
        }

        Quaternion newRot = Quaternion.Euler(-10f * leftJoystickInput.y, 20f * leftJoystickInput.x, 0f);
        characterModel.localRotation = Quaternion.Lerp(characterModel.localRotation,
            newRot, Time.deltaTime * rotationSpeed);
    }


    private void MovingCrosshair()
    {
        //float xMovementRightJoystick = rightJoystickInput.x; // The horizontal movement from joystick 02
        //float yMovementRightJoystick = rightJoystickInput.y; // The vertical movement from joystick 02

        Vector3 jsPosition;
        if (rightJoystickInput != Vector3.zero)
        {
            jsPosition = new Vector3(
                Screen.width / 2 + (rightJoystickInput.x * Screen.width * 0.75f),
                Screen.height / 2 + (rightJoystickInput.y * Screen.height * 0.75f),
                0f);

            characterAnimator.SetBool(s_ShotHash, true);

            //disparo de ammo
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
              
                AmmoManager.ammoManagerInstance.SpawnAmmo(spawnSpotLeft.position, Quaternion.identity);
                AmmoManager.ammoManagerInstance.SpawnAmmo(spawnSpotRight.position, Quaternion.identity);
            }
        }
        else
        {
            jsPosition = new Vector3(
                Screen.width / 2,
                Screen.height / 2,
                0f);
            characterAnimator.SetBool(s_ShotHash, false);
        }

        //transformo el vector jposition en screen position
        Vector2 viewPortPosA = CharacterManager.characterManagerInstance.mainCamera.ScreenToViewportPoint(jsPosition);
        Vector2 screenPosA = new Vector2(
                ((viewPortPosA.x * UIManager.uiLevelsInstance.rtCrossHairParent.sizeDelta.x) - (UIManager.uiLevelsInstance.rtCrossHairParent.sizeDelta.x * 0.5f)),
                ((viewPortPosA.y * UIManager.uiLevelsInstance.rtCrossHairParent.sizeDelta.y) - (UIManager.uiLevelsInstance.rtCrossHairParent.sizeDelta.y * 0.5f)));

        //controlo que el crosshair no pase los limites de la pantalla
        screenPosA = new Vector3(Mathf.Clamp(screenPosA.x, -(Screen.width / 2), Screen.width / 2),
            Mathf.Clamp(screenPosA.y, -(Screen.height / 2), Screen.height / 2), 0f);

        UIManager.uiLevelsInstance.rtCrossHair.anchoredPosition = Vector2.Lerp(
            UIManager.uiLevelsInstance.rtCrossHair.anchoredPosition, screenPosA,
            Time.deltaTime * crossHairSpeed
            );


        //Obtengo un nuevo vector para rotar al Player y los lasers
        Vector3 target = CharacterManager.characterManagerInstance.mainCamera.ScreenToWorldPoint(
            new Vector3(UIManager.uiLevelsInstance.rtCrossHair.position.x, UIManager.uiLevelsInstance.rtCrossHair.position.y, distanceRaycast));
        
        //Rotacion del modelo hacia en direccion al crossHair
        characterModel.LookAt(target);


        //funciona con hit
        Ray _ray = CharacterManager.characterManagerInstance.mainCamera.ScreenPointToRay(jsPosition);
        RaycastHit _rayHit;
        if (Physics.Raycast(_ray, out _rayHit, distanceRaycast))
        {

            if (_rayHit.collider.gameObject.CompareTag("Asteroid") || _rayHit.collider.gameObject.CompareTag("Enemy"))
            {
                //target = _rayHit.collider.transform.position;
                UIManager.uiLevelsInstance.SetCrossHairColor01();
                print(_rayHit.collider.name);
            }
            //else if (_rayHit.collider.gameObject.CompareTag("Player"))
            //{
            //    UILevels.uiLevelsInstance.SetCrossHairColor02();
            //    UILevels.uiLevelsInstance.SetCrossHairColor00();
            //}

        }
        else
        {
            UIManager.uiLevelsInstance.SetCrossHairColor00();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        
    }


}

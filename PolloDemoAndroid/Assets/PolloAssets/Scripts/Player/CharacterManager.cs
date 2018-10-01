using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour {
    public static CharacterManager characterManagerInstance;
    public Camera mainCamera;                       //Camara hija que se seguira al player
    [SerializeField]
    private Transform crossHairFromWorld;           //CrossHair de referencia más lejano para usar en el modo sin joystick derecho
    [SerializeField]
    private Transform crossHairFromWorldB;          //CrossHair de referencia más cercano para usarlo en el modo sin joystick derecho
    [SerializeField]
    private bool activateRightJoyStick;             //Boolean de prueba para desactivar el joystick derecho

    public Character character;                     //clase que controla los movimientos del player junto a colliders
    //public CharacterCollider characterCollider;




    //public float moveSpeed = 6.0f;                  // Velocidad de movimiento en XY del player
    //public float rotationSpeed = 1f;                //Velocidad de rotación del player
    //public float crossHairSpeed = 2f;               //Velocidad del CrossHair para desplazarse sobre la pantalla
    //public float distanceRaycast = 10f;             //Distancia para el uso del Raycast
    //[SerializeField]
    //private Vector2 characterRangeMin;              //Rango minimo en XY del player sobre el mundo/local
    //[SerializeField]
    //private Vector2 characterRangeMax;              //Rango máximo en XY del player sobre el mundo/local

    //private Vector3 leftJoystickInput; // holds the input of the Left Joystick
    //private Vector3 rightJoystickInput; // hold the input of the Right Joystick
    //private Animator characterAnimator; // the animator controller of the player character

    //private float _screenW;                         //Tamaño de pantalla
    //private float _screenH;                         //tamañao de pantalla para el calculo de movimiento en crosshair

    //[Header("Animaciones")]
    //private int s_DeadHash = Animator.StringToHash("Dead");
    //private int s_ShotHash = Animator.StringToHash("Shot");
    //private int s_MovingHash = Animator.StringToHash("MovingXY");

    //static int s_RunStartHash = Animator.StringToHash("RunStart");

    //static int s_JumpingHash = Animator.StringToHash("Jumping");
    //static int s_JumpingSpeedHash = Animator.StringToHash("JumpSpeed");
    //static int s_SlidingHash = Animator.StringToHash("Sliding");




    //public float SpeedForward;
    //public float Speed;

    public int maxLife = 3;
    public float laneChangeSpeed = 1.0f;

    public int currentLife { get { return m_CurrentLife; } set { m_CurrentLife = value; } }
    protected bool m_IsInvincible;

    protected int m_CurrentLife;

    // Cheating functions, use for testing
    public void CheatInvincible(bool invincible)
    {
        m_IsInvincible = invincible;
    }

    public bool IsCheatInvincible()
    {
        return m_IsInvincible;
    }


    protected void Awake()
    {
 
        m_CurrentLife = 0;
  
    }


    // Use this for initialization
    void Start () {
        //characterAnimator = character.GetComponent<Animator>();
        //_screenH = Screen.height;
        //_screenW = Screen.width;

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

        if (!activateRightJoyStick)
        {
            UIManager.uiLevelsInstance.GetComponent<DualJoystickTouchContoller>().ActivateRightJoyStick(false);
        }
        else
        {
            UIManager.uiLevelsInstance.GetComponent<DualJoystickTouchContoller>().ActivateRightJoyStick(true);


        }

    }

    // Update is called once per frame
    protected void Update() {

        // get input from both joysticks
        //leftJoystickInput = UIManager.uiLevelsInstance.leftJoystick.GetInputDirection();
        //rightJoystickInput = UIManager.uiLevelsInstance.rightJoystick.GetInputDirection();

        //MovingCharacterXY();

        if (activateRightJoyStick)
        {
            //MovingCrosshair();
        }
        else {
            //MovingCrosshairFromWorld();
        }
   
       

       
    }

   


    private void MovingCrosshairFromWorld() {
        //transformo el vector jposition en screen position
        Vector2 viewPortPosA = mainCamera.WorldToViewportPoint(crossHairFromWorld.position);
        Vector2 screenPosA = new Vector2(
                ((viewPortPosA.x * UIManager.uiLevelsInstance.rtCrossHairParent.sizeDelta.x) - (UIManager.uiLevelsInstance.rtCrossHairParent.sizeDelta.x * 0.5f)),
                ((viewPortPosA.y * UIManager.uiLevelsInstance.rtCrossHairParent.sizeDelta.y) - (UIManager.uiLevelsInstance.rtCrossHairParent.sizeDelta.y * 0.5f)));

        UIManager.uiLevelsInstance.rtCrossHair.anchoredPosition = screenPosA;

        Vector2 viewPortPosB = mainCamera.WorldToViewportPoint(crossHairFromWorldB.position);
        Vector2 screenPosB = new Vector2(
                ((viewPortPosB.x * UIManager.uiLevelsInstance.rtCrossHairParent.sizeDelta.x) - (UIManager.uiLevelsInstance.rtCrossHairParent.sizeDelta.x * 0.5f)),
                ((viewPortPosB.y * UIManager.uiLevelsInstance.rtCrossHairParent.sizeDelta.y) - (UIManager.uiLevelsInstance.rtCrossHairParent.sizeDelta.y * 0.5f)));
        UIManager.uiLevelsInstance.rtCrossHairB.anchoredPosition = screenPosB;
    }

    protected virtual void LateUpdate()
    {

        //transform.position = transform.position + (transform.forward * SpeedForward * Time.deltaTime);
        //// This will move the current transform based on a finger drag gesture
        //TouchSystem.MoveObjectInX(transform, TouchSystem.DragDelta, Speed);
    }


    //private void MovingCrosshair()
    //{
    //    float xMovementRightJoystick = rightJoystickInput.x; // The horizontal movement from joystick 02
    //    float yMovementRightJoystick = rightJoystickInput.y; // The vertical movement from joystick 02

    //    Vector3 jsPosition;
    //    if (rightJoystickInput != Vector3.zero)
    //    {
    //        jsPosition = new Vector3(
    //            _screenW / 2 + (xMovementRightJoystick * _screenW * 0.75f),
    //            _screenH / 2 + (yMovementRightJoystick * _screenH * 0.75f),
    //            0f);

    //        characterAnimator.SetBool(s_ShotHash, true);
    //    }
    //    else
    //    {
    //        jsPosition = new Vector3(
    //            _screenW / 2,
    //            _screenH / 2,
    //            0f);
    //        characterAnimator.SetBool(s_ShotHash, false);
    //    }

    //    //transformo el vector jposition en screen position
    //    Vector2 viewPortPosA = mainCamera.ScreenToViewportPoint(jsPosition);
    //    Vector2 screenPosA = new Vector2(
    //            ((viewPortPosA.x * UIManager.uiLevelsInstance.rtCrossHairParent.sizeDelta.x) - (UIManager.uiLevelsInstance.rtCrossHairParent.sizeDelta.x * 0.5f)),
    //            ((viewPortPosA.y * UIManager.uiLevelsInstance.rtCrossHairParent.sizeDelta.y) - (UIManager.uiLevelsInstance.rtCrossHairParent.sizeDelta.y * 0.5f)));

    //    //controlo que el crosshair no pase los limites de la pantalla
    //    screenPosA = new Vector3(Mathf.Clamp(screenPosA.x, -(_screenW / 2), _screenW / 2),
    //        Mathf.Clamp(screenPosA.y, -(_screenH / 2), _screenH / 2), 0f);

    //    UIManager.uiLevelsInstance.rtCrossHair.anchoredPosition = Vector2.Lerp(
    //        UIManager.uiLevelsInstance.rtCrossHair.anchoredPosition, screenPosA,
    //        Time.deltaTime * crossHairSpeed
    //        );

    //    //funciona con hit
    //    Ray _ray = mainCamera.ScreenPointToRay(jsPosition);
    //    RaycastHit _rayHit;
    //    if (Physics.Raycast(_ray, out _rayHit, distanceRaycast))
    //    {

    //        if (_rayHit.collider.gameObject.CompareTag("Asteroid") || _rayHit.collider.gameObject.CompareTag("Enemy"))
    //        {
    //            //target = _rayHit.collider.transform.position;
    //            UIManager.uiLevelsInstance.SetCrossHairColor01();
    //            print(_rayHit.collider.name);
    //        }
    //        //else if (_rayHit.collider.gameObject.CompareTag("Player"))
    //        //{
    //        //    UILevels.uiLevelsInstance.SetCrossHairColor02();
    //        //    UILevels.uiLevelsInstance.SetCrossHairColor00();
    //        //}

    //    }
    //    else
    //    {
    //        UIManager.uiLevelsInstance.SetCrossHairColor00();
    //    }

    //}

    //private void MovingCharacterXY()
    //{
    //    float xMovementLeftJoystick = leftJoystickInput.x; // The horizontal movement from joystick 01
    //    float yMovementLeftJoystick = leftJoystickInput.y; // The vertical movement from joystick 01	

    //    // if there is no input on the left joystick
    //    if (leftJoystickInput == Vector3.zero)
    //    {
    //        characterAnimator.SetFloat(s_MovingHash, 0f);
    //    }

    //    // if there is only input from the left joystick
    //    if (leftJoystickInput != Vector3.zero)
    //    {
    //        Vector3 accelXY = new Vector3(xMovementLeftJoystick, yMovementLeftJoystick, 0f) * moveSpeed;
    //        Vector3 velocity = character.transform.localPosition + accelXY * Time.deltaTime;
    //        Vector3 velFixed = new Vector3(
    //            Mathf.Clamp(velocity.x, -characterRangeMin.x, characterRangeMax.x),
    //             Mathf.Clamp(velocity.y, -characterRangeMin.y, characterRangeMax.y),
    //             0f
    //            );
    //        character.transform.localPosition = velFixed;
    //        characterAnimator.SetFloat(s_MovingHash, xMovementLeftJoystick);


    //        //aniPlayer.SetFloat(horizontalHash, xAxis, 0.1f, animationSpeed * Time.deltaTime);
    //    }

    //    Quaternion newRot = Quaternion.Euler(-10f * yMovementLeftJoystick, 20f * xMovementLeftJoystick, 0f);
    //    character.characterModel.localRotation = Quaternion.Lerp(character.characterModel.localRotation,
    //        newRot, Time.deltaTime * rotationSpeed);
    //}

    //public void Init()
    //{
    //    //transform.position = k_StartingPosition;
    //    //m_TargetPosition = Vector3.zero;

    //    //m_CurrentLane = k_StartingLane;
    //    //characterCollider.transform.localPosition = Vector3.zero;

    //    currentLife = maxLife;

    //    //m_Audio = GetComponent<AudioSource>();

    //    //m_ObstacleLayer = 1 << LayerMask.NameToLayer("Obstacle");
    //}

    //// Called at the beginning of a run or rerun
    //public void Begin()
    //{
    //    character.animator.SetBool(s_DeadHash, false);

    //    //characterCollider.Init();

    //    //m_ActiveConsumables.Clear();
    //}
}

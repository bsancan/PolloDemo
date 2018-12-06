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
    private Transform spawnSpotCenter;
    [Tooltip("Rango minimo del movimiento del player en X Y")]
    [SerializeField]
    private Vector2 characterRangeMin;              //Rango minimo en XY del player sobre el mundo/local
    [Tooltip("Rango maximo del movimiento del player en X Y")]
    [SerializeField]
    private Vector2 characterRangeMax;              //Rango máximo en XY del player sobre el mundo/local
    [Tooltip("Velodidad de movimiento del player")]
    [SerializeField]
    private float moveSpeed = 20f;                  // Velocidad de movimiento en XY del player
    [Tooltip("Velodidad de rotacion del player")]
    [SerializeField]
    private float rotationSpeed = 10f;                //Velocidad de rotación del player
    [Tooltip("Velodidad de movimiento del crossHair")]
    [SerializeField]
    private float crossHairSpeed = 3f;               //Velocidad del CrossHair para desplazarse sobre la pantalla
    [Tooltip("Distancia del Raycast")]

    public float fireRate;

    [SerializeField]
    private float distanceRaycast = 100f;             //Distancia para el uso del Raycast
    [Tooltip("Energia maxima del player")]
    [SerializeField]
    private int playerEnergy = 100;
    [SerializeField]
    private float timeToConsumeEnergy = 1f;
    [Tooltip("Escudo maximo del player")]
    [SerializeField]
    private int playerShield = 100;
    [SerializeField]
    private float timeToConsumeShield = 1f;
    [SerializeField]
    private int wasteEnergy = 1;


    private IEnumerator corEnergy;

    private int currentPlayerEnergy;
    private int currentPlayerShield;

    //======Animaciones del player
    static int s_DeadHash = Animator.StringToHash("Dead");
    static int s_ShotHash = Animator.StringToHash("Shot");
    static int s_MovingHash = Animator.StringToHash("MovingXY");
    static int s_HitHash = Animator.StringToHash("Hit");

    //=====Obtienen los valores obtenidos de los controles
    private Vector3 leftJoystickInput = Vector3.zero;
    private Vector3 rightJoystickInput = Vector3.zero;

   
    private float nextFire;
    private float saveCurrentPlayeSpeedZ;
    //pruebas
    //[Header("variables para pruebas")]
    //public bool iniciarConsumoDeEnergia;
    //private Vector3 pruebaTarget;


    void Start () {
      

  

        //if (iniciarConsumoDeEnergia)
        //{
        //    StartEnergyConsumption();
        //}
       
    }
	

	void Update () {

        //if (GameManager.gameManagerInstance.isGameOver)
        //    return;

        leftJoystickInput = UIManager.uiManagerInstance.leftJoystick.GetInputDirection();
        rightJoystickInput = UIManager.uiManagerInstance.rightJoystick.GetInputDirection();

        if (GameManager.gameManagerInstance.currentLevel == 1)
        {
            MovingCharacterXY();
            MovingCrosshair();
        }
        else if (GameManager.gameManagerInstance.currentLevel == 2)
        {
            MovingCharacterTunnelXY();
            MovingCrosshairTunnel();
        }

    }

    public void SetInitialValues() {
        currentPlayerEnergy = playerEnergy;
        currentPlayerShield = playerShield;

        //asigno la corutine a una variable para poder reiniciarla cada vez que se obtenga energia
        corEnergy = CorEnergyConsumption();

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
        //Obtengo un nuevo vector para rotar al Player y los lasers
        Vector3 target = CharacterManager.characterManagerInstance.mainCamera.ScreenToWorldPoint(
            new Vector3(UIManager.uiManagerInstance.rtCrossHair.position.x, UIManager.uiManagerInstance.rtCrossHair.position.y, distanceRaycast));

        Vector3 jsPosition;
        if (rightJoystickInput != Vector3.zero)
        {
            jsPosition = new Vector3(
                Screen.width / 2 + (rightJoystickInput.x * Screen.width * 0.75f),
                Screen.height / 2 + (rightJoystickInput.y * Screen.height * 0.75f),
                0f);

           
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
                ((viewPortPosA.x * UIManager.uiManagerInstance.rtCrossHairParent.sizeDelta.x) - (UIManager.uiManagerInstance.rtCrossHairParent.sizeDelta.x * 0.5f)),
                ((viewPortPosA.y * UIManager.uiManagerInstance.rtCrossHairParent.sizeDelta.y) - (UIManager.uiManagerInstance.rtCrossHairParent.sizeDelta.y * 0.5f)));

        //controlo que el crosshair no pase los limites de la pantalla
        screenPosA = new Vector3(Mathf.Clamp(screenPosA.x, -(Screen.width / 2), Screen.width / 2),
            Mathf.Clamp(screenPosA.y, -(Screen.height / 2), Screen.height / 2), 0f);

        UIManager.uiManagerInstance.rtCrossHair.anchoredPosition = Vector2.Lerp(
            UIManager.uiManagerInstance.rtCrossHair.anchoredPosition, screenPosA,
            Time.deltaTime * crossHairSpeed
            );

        if (rightJoystickInput != Vector3.zero) {

            characterAnimator.SetBool(s_ShotHash, true);

            //disparo de ammo
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                //spawnSpotCenter.LookAt(target);
                AmmoManager.ammoManagerInstance.SpawnAmmo(spawnSpotCenter);
                //spawnSpotLeft.LookAt(target);
                //spawnSpotRight.LookAt(target);
                //AmmoManager.ammoManagerInstance.SpawnAmmo(spawnSpotLeft.position, spawnSpotLeft.rotation);
                //AmmoManager.ammoManagerInstance.SpawnAmmo(spawnSpotRight.position, spawnSpotRight.rotation);

                //pruebas
                //pruebaTarget = target;
            }

            //funciona con hit
            Ray _ray = CharacterManager.characterManagerInstance.mainCamera.ScreenPointToRay(jsPosition);
                RaycastHit _rayHit;
                if (Physics.Raycast(_ray, out _rayHit, distanceRaycast))
                {

                    if (_rayHit.collider.gameObject.CompareTag("Asteroid") || _rayHit.collider.gameObject.CompareTag("Enemy"))
                    {
                        //target = _rayHit.collider.transform.position;
                        //UIManager.uiManagerInstance.SetCrossHairColor01();
                        //Rotacion center hacia en direccion al objeto en la mira
                        spawnSpotCenter.LookAt(_rayHit.collider.transform);

                        Vector2 viewPortPosB = CharacterManager.characterManagerInstance.mainCamera.WorldToViewportPoint(_rayHit.collider.transform.position);
                        Vector2 screenPosB = new Vector2(
                                ((viewPortPosB.x * UIManager.uiManagerInstance.rtCrossHairParent.sizeDelta.x) - (UIManager.uiManagerInstance.rtCrossHairParent.sizeDelta.x * 0.5f)),
                                ((viewPortPosB.y * UIManager.uiManagerInstance.rtCrossHairParent.sizeDelta.y) - (UIManager.uiManagerInstance.rtCrossHairParent.sizeDelta.y * 0.5f)));
                       //UIManager.uiManagerInstance.rtCrossHairB.anchoredPosition = screenPosB;
                        //UIManager.uiManagerInstance.SetCrossHairBColor01();
                        //UIManager.uiManagerInstance.ShowCrossHairB(true);
                        //print(_rayHit.collider.name);
                    }
                    else
                    {
                        spawnSpotCenter.LookAt(target);
                        UIManager.uiManagerInstance.ShowCrossHairB(false);
                    }

                }
                else
                {
                    //UIManager.uiManagerInstance.SetCrossHairColor00();
                    //UIManager.uiManagerInstance.SetCrossHairBColor00();
                    UIManager.uiManagerInstance.ShowCrossHairB(false);
                    spawnSpotCenter.LookAt(target);
                }
           
        }

        //Rotacion del modelo hacia en direccion al crossHair
        characterModel.LookAt(target);

    }

    private void MovingCharacterTunnelXY()
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

        //Quaternion newRot = Quaternion.Euler(-10f * leftJoystickInput.y, 20f * leftJoystickInput.x, 0f);
        //characterModel.localRotation = Quaternion.Lerp(characterModel.localRotation,
        //    newRot, Time.deltaTime * rotationSpeed);
    }

    private void MovingCrosshairTunnel()
    {
        //float xMovementRightJoystick = rightJoystickInput.x; // The horizontal movement from joystick 02
        //float yMovementRightJoystick = rightJoystickInput.y; // The vertical movement from joystick 02
        //Obtengo un nuevo vector para rotar al Player y los lasers
        Vector3 target = CharacterManager.characterManagerInstance.mainCamera.ScreenToWorldPoint(
            new Vector3(UIManager.uiManagerInstance.rtCrossHair.position.x, UIManager.uiManagerInstance.rtCrossHair.position.y, distanceRaycast));

        Vector3 jsPosition;
        if (rightJoystickInput != Vector3.zero)
        {
            jsPosition = new Vector3(
                Screen.width / 2 + (rightJoystickInput.x * Screen.width * 0.75f),
                Screen.height / 2 + (rightJoystickInput.y * Screen.height * 0.75f),
                0f);


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
                ((viewPortPosA.x * UIManager.uiManagerInstance.rtCrossHairParent.sizeDelta.x) - (UIManager.uiManagerInstance.rtCrossHairParent.sizeDelta.x * 0.5f)),
                ((viewPortPosA.y * UIManager.uiManagerInstance.rtCrossHairParent.sizeDelta.y) - (UIManager.uiManagerInstance.rtCrossHairParent.sizeDelta.y * 0.5f)));

        //controlo que el crosshair no pase los limites de la pantalla
        screenPosA = new Vector3(Mathf.Clamp(screenPosA.x, -(Screen.width / 2), Screen.width / 2),
            Mathf.Clamp(screenPosA.y, -(Screen.height / 2), Screen.height / 2), 0f);

        UIManager.uiManagerInstance.rtCrossHair.anchoredPosition = Vector2.Lerp(
            UIManager.uiManagerInstance.rtCrossHair.anchoredPosition, screenPosA,
            Time.deltaTime * crossHairSpeed
            );

        if (rightJoystickInput != Vector3.zero)
        {

            characterAnimator.SetBool(s_ShotHash, true);

            //disparo de ammo
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                //spawnSpotCenter.LookAt(target);
                AmmoManager.ammoManagerInstance.SpawnAmmo(spawnSpotCenter);
                //spawnSpotLeft.LookAt(target);
                //spawnSpotRight.LookAt(target);
                //AmmoManager.ammoManagerInstance.SpawnAmmo(spawnSpotLeft.position, spawnSpotLeft.rotation);
                //AmmoManager.ammoManagerInstance.SpawnAmmo(spawnSpotRight.position, spawnSpotRight.rotation);

                //pruebas
                //pruebaTarget = target;
            }

            //funciona con hit
            Ray _ray = CharacterManager.characterManagerInstance.mainCamera.ScreenPointToRay(jsPosition);
            RaycastHit _rayHit;
            if (Physics.Raycast(_ray, out _rayHit, distanceRaycast))
            {

                if (_rayHit.collider.gameObject.CompareTag("Asteroid") || _rayHit.collider.gameObject.CompareTag("Enemy"))
                {
                    //target = _rayHit.collider.transform.position;
                    //UIManager.uiManagerInstance.SetCrossHairColor01();
                    //Rotacion center hacia en direccion al objeto en la mira
                    spawnSpotCenter.LookAt(_rayHit.collider.transform);

                    Vector2 viewPortPosB = CharacterManager.characterManagerInstance.mainCamera.WorldToViewportPoint(_rayHit.collider.transform.position);
                    Vector2 screenPosB = new Vector2(
                            ((viewPortPosB.x * UIManager.uiManagerInstance.rtCrossHairParent.sizeDelta.x) - (UIManager.uiManagerInstance.rtCrossHairParent.sizeDelta.x * 0.5f)),
                            ((viewPortPosB.y * UIManager.uiManagerInstance.rtCrossHairParent.sizeDelta.y) - (UIManager.uiManagerInstance.rtCrossHairParent.sizeDelta.y * 0.5f)));
                    //UIManager.uiManagerInstance.rtCrossHairB.anchoredPosition = screenPosB;
                    //UIManager.uiManagerInstance.SetCrossHairBColor01();
                    //UIManager.uiManagerInstance.ShowCrossHairB(true);
                    //print(_rayHit.collider.name);
                }
                else
                {
                    spawnSpotCenter.LookAt(target);
                    UIManager.uiManagerInstance.ShowCrossHairB(false);
                }

            }
            else
            {
                //UIManager.uiManagerInstance.SetCrossHairColor00();
                //UIManager.uiManagerInstance.SetCrossHairBColor00();
                UIManager.uiManagerInstance.ShowCrossHairB(false);
                spawnSpotCenter.LookAt(target);
            }

        }

        //Rotacion del modelo hacia en direccion al crossHair
        characterModel.LookAt(target);

    }

    public void StartEnergyConsumption() {
        
        StartCoroutine(corEnergy);
    }

    private void PlayerDamage(int valueDamage)
    {
        //damaged = true;
        
        if ((currentPlayerShield - valueDamage) >= 0)
        {
            currentPlayerShield -= valueDamage;
            UIManager.uiManagerInstance.txtPlayerShield.text = 
                ((int)(currentPlayerShield * 100 / playerShield)).ToString();
        }
        else
        {
            //find del jugo
            gameObject.SetActive(false);
            CharacterManager.characterManagerInstance.playerIsDead = true;

            UIManager.uiManagerInstance.scoreManager.ShowScoreList(true);
            UIManager.uiManagerInstance.ShowCrossHairCanvas(false);
            UIManager.uiManagerInstance.ShowJoyStickCanvas(false);
            UIManager.uiManagerInstance.ShowBars(false);
            UIManager.uiManagerInstance.ShowMenuCanvas(false);
            //detengo el movimiento en z del player
            CharacterManager.characterManagerInstance.speedZ = 0f;

            //UIManager.uiManagerInstance.FadeAnimationOUT();
            //desactivo todo los objetos
        }


    }

    public void PlayerGainEnergy(int valueItem)
    {
        StopCoroutine(corEnergy);

        if ((currentPlayerEnergy + valueItem) > playerEnergy)
        {
            currentPlayerEnergy = playerEnergy;
        }
        else {
            currentPlayerEnergy += valueItem;
        }

        UIManager.uiManagerInstance.txtPlayerEnergy.text =
               ((int)(currentPlayerEnergy * 100 / playerEnergy)).ToString();
        //sumo puntaje por la energuia recogida
        UIManager.uiManagerInstance.scoreManager.currentPlayerScore += valueItem;

        StartCoroutine(corEnergy);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //anmacion de daño
            characterAnimator.SetTrigger(s_HitHash);
            //explosion del player
            ExplosionManager.explosionManagerInstance.SpawnPlayerExplosion(transform.position);
            //consumo de escudo
            PlayerDamage(other.GetComponent<Asteroid>().valueDamage);
        } else if(other.gameObject.name == "Level01_END")
        {
            StopCoroutine(corEnergy);
            //FIN DEL NIVEL
            if (!CharacterManager.characterManagerInstance.playerIsDead)
            {
                UIManager.uiManagerInstance.scoreManager.ShowScoreList(true);
                //UIManager.uiManagerInstance.ShowCrossHairCanvas(false);
                //UIManager.uiManagerInstance.ShowJoyStickCanvas(false);
                //UIManager.uiManagerInstance.ShowBars(false);
                //UIManager.uiManagerInstance.ShowMenuCanvas(false);

                //detengo el movimiento en z del player
                saveCurrentPlayeSpeedZ = CharacterManager.characterManagerInstance.speedZ;
                CharacterManager.characterManagerInstance.speedZ = 0f;
                GameManager.gameManagerInstance.isGameOver = true;
                //characterAnimator.SetBool(s_ShotHash, false);
            }
        } else if (other.gameObject.CompareTag("EnemyAmmo"))
        {
            //anmacion de daño
            characterAnimator.SetTrigger(s_HitHash);
            //explosion del player
            ExplosionManager.explosionManagerInstance.SpawnPlayerExplosion(transform.position);
            PlayerDamage(other.GetComponent<EnemyAmmo>().valueDamage);
        }


    }

    #region Cortinas

    IEnumerator CorEnergyConsumption() {

        print("cor - Inicio de consumo de energia");

        while (currentPlayerEnergy > 0)
        {
            yield return new WaitForSeconds(timeToConsumeEnergy);
            currentPlayerEnergy -= wasteEnergy;
   
            UIManager.uiManagerInstance.txtPlayerEnergy.text = 
                ((int)(currentPlayerEnergy * 100 / playerEnergy)).ToString();


        }

        currentPlayerEnergy = 0;
        UIManager.uiManagerInstance.txtPlayerEnergy.text = "0";

        //fin del juego
        gameObject.SetActive(false);
        CharacterManager.characterManagerInstance.playerIsDead = true;

        UIManager.uiManagerInstance.scoreManager.ShowScoreList(true);
        UIManager.uiManagerInstance.ShowCrossHairCanvas(false);
        UIManager.uiManagerInstance.ShowJoyStickCanvas(false);
        UIManager.uiManagerInstance.ShowBars(false);
        UIManager.uiManagerInstance.ShowMenuCanvas(false);
        //detengo el movimiento en z del player
        CharacterManager.characterManagerInstance.speedZ = 0f;
        //UIManager.uiManagerInstance.FadeAnimationOUT();
    }


   
    #endregion

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawLine(transform.position, pruebaTarget);
    //}

}

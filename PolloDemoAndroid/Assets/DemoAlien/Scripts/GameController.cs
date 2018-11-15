using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	public Camera _camera;
    public GameObject mainPlayer;

    public GameObject cnvLevel;
    public GameObject cnvGame;
    public GameObject cnvParameters;
    public GameObject GalaxySphere;
    public GameObject Dust;

	public LayerMask layerMask;
    public GameObject explosionEnemy;

    public bool SwipeDestroyingEnemy;

    //crosshair
    public Canvas cnvCrossHair;
    public GameObject GoCrossHair01;
    public GameObject refCrossHair01;
    public GameObject GoCrossHair02;
    public GameObject refCrossHair02;

    //para controlar la carga

    public GameObject sldCharge;
    public GameObject txtParameters;
    public GameObject playerChoose;
    public GameObject btnNext;
//    public GameObject[] btnLevels;



//    public GameObject[] panelParameters;

    //panel 01
//    public InputField meteorCount;
//    public InputField spawnWait;
//    public InputField speedMeteor;
//    public InputField countDown;


    Rigidbody rb;
    RobotControl robot;

    List<int> sid;

    int LastID;

    int NewID;

    bool isParameter;
    bool rotatePlayerChoose;

    int touchFinger;
    Vector2 touchPosition;
    Vector3 nueva;
    Quaternion nuevaRot;
    float suma = 0f;
    int opc = 0;

    void Awake(){
        robot = mainPlayer.GetComponent<RobotControl>();
//        cnvParameters.SetActive(true);
//        mainPlayer.SetActive(false);
//        cnvGame.SetActive(false);
        cnvLevel.SetActive(false);
        playerChoose.SetActive(false);
        btnNext.SetActive(false);
//        GalaxySphere.SetActive(false);
//        Dust.SetActive(false);

//        for (int i = 0; i < btnLevels.Length; i++)
//        {
//            btnLevels[i].SetActive(false);
//        }
//        for (int i = 0; i < panelParameters.Length; i++)
//        {
//            panelParameters[i].SetActive(false);
//        }

//        txtParameters.SetActive(true);
//        txtParameters.GetComponent<Text>().text = "Cargando...";
        sldCharge.GetComponent<Slider>().value = 0f;
        sldCharge.GetComponent<Slider>().maxValue = 100f;
        isParameter = true;

        rb = playerChoose.GetComponent<Rigidbody>();

        //parametros nivel 01
//        meteorCount.text = "200";
//        spawnWait.text = "0.04";
//        speedMeteor.text = "-20";
//        countDown.text = "60";
    }

	void Start () {
        sid = new List<int>();
//        playerChoose.SetActive(true);
        //playerChoose.GetComponent<Animator>().SetBool("StartFly", true);
        //rotatePlayerChoose = true;

        StartCoroutine(StartParameters());  
//        Next(1);
//        LevelParameter(11);
	}
	
    void FixedUpdate(){
        if (rotatePlayerChoose)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    touchFinger = touch.fingerId;
                    touchPosition = touch.deltaPosition;
                }
                if (touch.phase == TouchPhase.Moved)
                {
//                    Debug.Log(touch.deltaPosition.x.ToString());

                    Vector3 rotateFinger = new Vector3();

                    if (touch.deltaPosition.x < 0)
                        rotateFinger = new Vector3(rotateFinger.x, 45f, 0f);
                    else if (touch.deltaPosition.x > 0)
                        rotateFinger = new Vector3(rotateFinger.x, -45f, 0f);

//                    if (touch.deltaPosition.y < 0)
//                        rotateFinger = new Vector3(90f,rotateFinger.y, 0f);
//                    else if (touch.deltaPosition.y > 0)
//                        rotateFinger = new Vector3(-90f,rotateFinger.y, 0f);
                    

                    Quaternion deltaRotation = Quaternion.Euler(rotateFinger * Time.deltaTime * 4f);
                    rb.MoveRotation(rb.rotation * deltaRotation);
                }
            }
        }

        //rotate galaxy
        Rigidbody rbPlayer = mainPlayer.GetComponent<Rigidbody>();
        Rigidbody tr = GalaxySphere.GetComponent<Rigidbody>();
        Vector3 vt = new Vector3(Mathf.Clamp(rbPlayer.position.y/2f,-1f,1f) , Mathf.Clamp( (rbPlayer.position.x/2f),-2-5f,2.5f), 0f);

        Quaternion deltaGalaxy = Quaternion.Euler(vt);
        tr.MoveRotation(deltaGalaxy);


        //MOVE CROSSHAIR
        //first you need the RectTransform component of your canvas
        RectTransform CanvasRect = cnvCrossHair.GetComponent<RectTransform>();
        //CROSSHAIR 01
        RectTransform UI_Element01 = GoCrossHair01.GetComponent<RectTransform>();
        Vector2 ViewportPosition01 = _camera.WorldToViewportPoint(refCrossHair01.GetComponent<Transform>().position);
        Vector2 ScreenPosition01 = new Vector2(
            ((ViewportPosition01.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
            ((ViewportPosition01.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f))
        );
        UI_Element01.anchoredPosition = ScreenPosition01;

        //CrossHair 02
        RectTransform UI_Element02 = GoCrossHair02.GetComponent<RectTransform>();
        Vector2 ViewportPosition02 = _camera.WorldToViewportPoint(refCrossHair02.GetComponent<Transform>().position);
        Vector2 ScreenPosition02 = new Vector2(
            ((ViewportPosition02.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
            ((ViewportPosition02.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f))
        );
        UI_Element02.anchoredPosition = ScreenPosition02;
    }

	void Update () {

//        parametros
//        if (isParameter && sldCharge.GetComponent<Slider>().value == (float)GetComponent<SpawnWave>().pooledAmountM){
//            StartCoroutine(StartParameters());   
//        }

       

        //SOLO FUNCIONA EN EL NIVEL 2
		if (!SwipeDestroyingEnemy) {
			foreach (Touch touch in Input.touches) {
				if (touch.phase == TouchPhase.Ended
//					&& touch.phase == TouchPhase.Moved
				) {
	
					Ray ray = _camera.ScreenPointToRay(touch.position);
					RaycastHit hit;
					if (Physics.Raycast(ray, out hit))
					{
//						Debug.Log (hit.collider.name);
						if (hit.collider.gameObject.CompareTag("Enemy"))
						{
							if (sid.Count == 0)
							{
								LastID = hit.collider.gameObject.GetComponent<ShipControl>().shipID;
								NewID = -1;
								sid.Add(LastID);

								Debug.Log("nave: " + LastID.ToString());
							}
							else if (sid.Count > 0)
							{
								NewID = hit.collider.gameObject.GetComponent<ShipControl>().shipID;
								if (LastID != NewID)
								{
									sid.Add(NewID);
									LastID = NewID;
									Debug.Log("nave: " + NewID.ToString());
								}
							}
							hit.collider.gameObject.GetComponent<ShipControl> ().SetColor (1);

						}


					}

				}
			}
		}

       

//        if (Input.GetMouseButton(0))
//        {
//            if (!SwipeDestroyingEnemy)
//            {
//                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//                RaycastHit hit;
//                if (Physics.Raycast(ray, out hit))
//                {
//                    if (hit.collider.gameObject.CompareTag("Enemy"))
//                    {
//                        if (sid.Count == 0)
//                        {
//                            LastID = hit.collider.gameObject.GetComponent<ShipControl>().shipID;
//                            NewID = -1;
//                            sid.Add(LastID);
//                            Debug.Log("nave: " + LastID.ToString());
//                        }
//                        else if (sid.Count > 0)
//                        {
//                            NewID = hit.collider.gameObject.GetComponent<ShipControl>().shipID;
//                            if (LastID != NewID)
//                            {
//                                sid.Add(NewID);
//                                LastID = NewID;
//                                Debug.Log("nave: " + NewID.ToString());
//                            }
//                        }
//						hit.collider.gameObject.GetComponent<ShipControl> ().SetColor (1);
//                    }
//
//
//                }
//            }
//
//        }
//        else
//        {
//			
//            
//        }
	}

    public void DestroyEnemiesBySwipe(){
        if (!SwipeDestroyingEnemy)
            StartCoroutine(DestroyEnemies());
    }

    IEnumerator DestroyEnemies(){
        SwipeDestroyingEnemy = true;
        SpawnWave objsw = GetComponent<SpawnWave>();
        GameObject objShip =  null;
        if (sid != null)
        {
            if (sid.Count > 0)
            {
                foreach (int id in sid)
                {
//                    Debug.Log("lista: " + id.ToString());
                    for (int i = 0; i < objsw.pooledShipsA.Count; i++)
                    {
                        objShip = objsw.pooledShipsA[i];
                        int pid = objShip.GetComponent<ShipControl>().shipID;
                        if (pid == id){
                            objShip.SetActive(false);
                            objShip.GetComponent<EnemyWeaponController> ().CancelInvokes();
                            if (explosionEnemy != null)
                            {
                                Vector3 vector = GameObject.Find("MainCamera").GetComponent<Camera>().WorldToScreenPoint(objShip.transform.position);
                                Instantiate(explosionEnemy, objShip.transform.position, objShip.transform.rotation);
                            }
                            break;
                        }
                    }
                    yield return new WaitForSeconds(0.2f);
                }
            }


        }


        sid = new List<int>();
        yield return new WaitForEndOfFrame();
        SwipeDestroyingEnemy = false;
    }

	public void GameOver(){
		StartCoroutine (ieGameOver ());
	}

    public void Next(int _opc){
        
        opc += _opc;
        Debug.Log(opc);
        if (opc == 1)
        {
//            txtParameters.GetComponent<Text>().text = "Escoja un nivel";
//            btnNext.SetActive(false);

//            btnLevels[0].SetActive(true);
//            btnLevels[1].SetActive(true);

        }
        else if (opc == 2)
      
        {
            
//            GetComponent<SpawnWave>().spawnWait = float.Parse(spawnWait.text);
//            GetComponent<SpawnWave>().hazartCount = int.Parse(meteorCount.text);
//            GetComponent<SpawnWave>().speedMeteor = float.Parse(speedMeteor.text);
//            GetComponent<SpawnWave>().timeLeft = float.Parse(countDown.text);
//            GetComponent<SpawnWave>().pooledAmountM = int.Parse((GetComponent<SpawnWave>().hazartCount / 2).ToString());


//            GetComponent<SpawnWave>().StartHazardsXY();
//
//            StartCoroutine(WaitToStart());


        }
    }

    public void LevelParameter(int lvl){
        if (lvl == 101)
        {
//            txtParameters[0].GetComponent<Text>().text = "NIVEL 01 - Oleada de meteoros";
//            playerChoose.SetActive(false);
//            playerChoose.GetComponent<Animator>().SetBool("StartFly", false);
//            rotatePlayerChoose = false;

            StartCoroutine(WaitToStart(lvl));

//            panelParameters[0].SetActive(true);
//
//
//            meteorCount.text = "200";
//            spawnWait.text = "0.04";
//            speedMeteor.text = "-20";
//            countDown.text = "60";

        }
        else if (lvl == 102)
        {
//            playerChoose.SetActive(false);
//            playerChoose.GetComponent<Animator>().SetBool("StartFly", false);
//            rotatePlayerChoose = false;

            StartCoroutine(WaitToStart(lvl));
        }
        else if (lvl == 103)
        {
            StartCoroutine(WaitToStart(lvl));
        }
        else if (lvl == 104)
        {
            StartCoroutine(WaitToStart(lvl));
        }
        else if (lvl == 105)
        {
            StartCoroutine(WaitToStart(lvl));
        }
        else if (lvl == 106)
        {
            StartCoroutine(WaitToStart(lvl));
        }
        else if (lvl == 107)
        {
            StartCoroutine(WaitToStart(lvl));
        }
        else if (lvl == 108)
        {
            StartCoroutine(WaitToStart(lvl));
        }
        else if (lvl == 0)
        {
        
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
//        txtParameters.SetActive(false);
//        btnLevels[0].SetActive(false);
//        btnLevels[1].SetActive(false);
//        btnNext.SetActive(true);
    }

	IEnumerator ieGameOver(){
		robot.PlayerDestroyed ();

		yield return new WaitForSeconds (3.5f);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

    IEnumerator StartParameters(){
//        isParameter = false;
        cnvLevel.SetActive(false);
        cnvParameters.SetActive(true);

        yield return new WaitForSeconds(1f);

//        btnLevels[0].SetActive(true);
//        btnLevels[1].SetActive(true);
//        btnLevels[2].SetActive(true);
//        btnLevels[3].SetActive(true);



//        
        //txtParameters.GetComponent<Text>().text = "Sistemas cargados";
    
//        btnNext.SetActive(true);
//        sldCharge.SetActive(false);
//        yield return new WaitForSeconds(1f);
//        rotatePlayerChoose = true;
    }

    IEnumerator WaitToStart(int lvl){
//        float currentSpeed = GetComponent<SpawnWave>().speedMeteor;
//        GetComponent<SpawnWave>().speedMeteor = -5f;

        if (lvl == 101)
        {
//            txtParameters.GetComponent<Text>().text = "Intenta esquivar los meteros y recoger los items";
            cnvGame.SetActive(true);
            mainPlayer.SetActive(true);
            GalaxySphere.SetActive(true);
            yield return new WaitForSeconds(3f);
            Dust.SetActive(true);
            GetComponent<SpawnWave>().Start_Level101();
            GetComponent<SpawnWave>().StartLaser01();

            yield return new WaitForSeconds(0.2f);
            GetComponent<SpawnWave>().level = lvl;

            //        panelParameters[0].SetActive(false);
            cnvParameters.SetActive(false);

            cnvLevel.SetActive(true);
        }
        else if (lvl == 102)
        {
//            txtParameters.GetComponent<Text>().text = "Intenta esquivar los meteros y recoger los items";
            cnvGame.SetActive(true);
            mainPlayer.SetActive(true);
            GalaxySphere.SetActive(true);
            yield return new WaitForSeconds(3f);
            Dust.SetActive(true);

            //GetComponent<SpawnWave>().StartEnemyShipA();

            //comienza el lanzamiento de proyectiles
            //GetComponent<SpawnWave>().Start_Level102_MisilA1A2();
            //comienza la animacion de varias naves pasando al fondo
            //GetComponent<SpawnWave>().Start_Level102_ShipA();


            //inicia el nivel 102: naves pasando por los lados
            GetComponent<SpawnWave>().Start_Level102();
            GetComponent<SpawnWave>().StartLaser01();

            GetComponent<SpawnWave>().level = lvl;
            yield return new WaitForSeconds(0.2f);
            cnvParameters.SetActive(false);
            cnvLevel.SetActive(true);
        }
        else if (lvl == 103)
        {
            //            txtParameters.GetComponent<Text>().text = "Intenta esquivar los meteros y recoger los items";
            cnvGame.SetActive(true);
            mainPlayer.SetActive(true);
            GalaxySphere.SetActive(true);
            yield return new WaitForSeconds(3f);
            Dust.SetActive(true);

            GetComponent<SpawnWave>().Start_Level103();
            GetComponent<SpawnWave>().StartLaser01();

            GetComponent<SpawnWave>().level = lvl;
            yield return new WaitForSeconds(0.2f);
            cnvParameters.SetActive(false);
            cnvLevel.SetActive(true);
        }
        else if (lvl == 104)
        {
            //            txtParameters.GetComponent<Text>().text = "Intenta esquivar los meteros y recoger los items";
            cnvGame.SetActive(true);
            mainPlayer.SetActive(true);
            GalaxySphere.SetActive(true);
            yield return new WaitForSeconds(3f);
            Dust.SetActive(true);

            GetComponent<SpawnWave>().Start_Level104();
            GetComponent<SpawnWave>().StartLaser01();

            GetComponent<SpawnWave>().level = lvl;
            yield return new WaitForSeconds(0.2f);
            cnvParameters.SetActive(false);
            cnvLevel.SetActive(true);
        }
        else if (lvl == 105)
        {
            cnvGame.SetActive(true);
            mainPlayer.SetActive(true);
            GalaxySphere.SetActive(true);
            yield return new WaitForSeconds(3f);
            Dust.SetActive(true);

            GetComponent<SpawnWave>().Start_Level105();
            GetComponent<SpawnWave>().StartLaser01();

            GetComponent<SpawnWave>().level = lvl;
            yield return new WaitForSeconds(0.2f);
            cnvParameters.SetActive(false);
            cnvLevel.SetActive(true);
        }
        else if (lvl == 106)
        {
            cnvGame.SetActive(true);
            mainPlayer.SetActive(true);
            GalaxySphere.SetActive(true);
            yield return new WaitForSeconds(3f);
            Dust.SetActive(true);

            GetComponent<SpawnWave>().Start_Level106();

            GetComponent<SpawnWave>().level = lvl;
            yield return new WaitForSeconds(0.2f);
            cnvParameters.SetActive(false);
            cnvLevel.SetActive(true);
        }

        else if (lvl == 107)
        {
            //            txtParameters.GetComponent<Text>().text = "Intenta esquivar los meteros y recoger los items";
            cnvGame.SetActive(true);
            mainPlayer.SetActive(true);
            GalaxySphere.SetActive(true);
            yield return new WaitForSeconds(3f);
            Dust.SetActive(true);

            //comienza la animacion de varias naves pasando al fondo
            //comienza el lanzamiento de proyectiles
            GetComponent<SpawnWave>().Start_MisilA1A2();
            GetComponent<SpawnWave>().StartLaser01();

            GetComponent<SpawnWave>().level = lvl;
            yield return new WaitForSeconds(0.2f);
            cnvParameters.SetActive(false);
            cnvLevel.SetActive(true);
        }

        else if (lvl == 108)
        {
            //            txtParameters.GetComponent<Text>().text = "Intenta esquivar los meteros y recoger los items";
            cnvGame.SetActive(true);
            mainPlayer.SetActive(true);
            GalaxySphere.SetActive(true);
            yield return new WaitForSeconds(3f);
            Dust.SetActive(true);

            GetComponent<SpawnWave>().StartEnemyShipA();

            GetComponent<SpawnWave>().level = lvl;
            yield return new WaitForSeconds(0.2f);
            cnvParameters.SetActive(false);
            cnvLevel.SetActive(true);
        }

    }
}

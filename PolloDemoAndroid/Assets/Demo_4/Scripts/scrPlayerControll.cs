using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerRange{
    public Vector2 minRange;
    public Vector2 maxRange;
}

[System.Serializable]
public class PlayerArms{
    public LayerMask layerMaskHit;
    public float distanceHit;
    public bool isLaserEnable;
    public GameObject laser;
    public Transform shotSpawnL, shotSpawnR; 
    public int amountLaser;
    public float speedLaser, waitLaser;
    public bool isShooting;

}

public class scrPlayerControll : MonoBehaviour {
    
    public Camera mainCamera;
    public PlayerRange playerRange;
    //public bool ActivePlayer;
    public float speedPlayer;
    public float speedPlayerZ;
    public float gradesAccelerometer;

    public float smoth;
    public float tiltAngle;

    public GameObject explotionPlayer;

    //crosshair
    public GameObject crossHair;
    public GameObject crossHairA;
    public GameObject crossHairB;
    public GameObject crossHairC;
//    public GameObject crossHairRefA;
//    public GameObject crossHairRefB;

    //laser

    public PlayerArms arms;
    public List<GameObject> poolLaser;

    //gamecontroll
    public scrGameControll gameControll;

    private RectTransform recCrossHair;
    private RectTransform recCrossHairA;
    private RectTransform recCrossHairB;

    private Rigidbody rb;
    private Animator aniPlayer;
    //accelerometer
    private Vector3 accelerationSnapshot;
    private Quaternion rotateQuaternion;
    private Matrix4x4 calibrationMatrix;

    //movimiento player
    private Vector3 acceleration;
    private Vector3 fixedAcceleration;

    //touch meteor
    Vector3 inputPos;
    public float TouchRangeY;
    //private int lastMeteorID;
    //private List<int> listMeteorID;
    //private Vector2 viewPortTouchPos;


    void Awake(){
        rb = GetComponent<Rigidbody>();
        aniPlayer = GetComponent<Animator>();

        recCrossHair = crossHair.GetComponent<RectTransform>();
        recCrossHairA = crossHairA.GetComponent<RectTransform>();
        recCrossHairB = crossHairB.GetComponent<RectTransform>();

        //prueba de touch
        //listMeteorID = new List<int>();
    }

	void Start () {
        //aniPlayer.SetBool("StartFly", true);
        CalibrateAccelerometer();

        //laser
        poolLaser = new List<GameObject>();
        CreatePools();

        //if (!arms.isLaserEnable)
        //{
        //    //solo para pruebas de disparo de lasers
        //    crossHair.GetComponent<CanvasGroup>().alpha = 0f;
        //    GameObject.Find("FireButton").SetActive(false);
        //}

        inputPos = new Vector3(Screen.width/2f, Screen.height/2f,0f);

        crossHairB.SetActive(false);
        aniPlayer.SetBool("StartFly", true);

        //TouchRangeY = 30f / 100f;
    }
	
	
	void Update () {
        //Vector2 viewPortPosB;
        //Vector2 screenPosB;

        


//        if (Application.platform == RuntimePlatform.Android)
//        {
//            foreach (Touch touch in Input.touches)
//            {
//                Ray andRayZero = mainCamera.ScreenPointToRay(crossHairC.transform.position);
//                RaycastHit andHitZero;
//                if (Physics.Raycast(andRayZero,out andHitZero, arms.distanceHit, arms.layerMaskHit))
//                {
//                    if (andHitZero.collider.gameObject.CompareTag("Meteor"))
//                    {
//                        scrMeteorControll mZero = andHitZero.collider.gameObject.GetComponent<scrMeteorControll>();
//                        if (!mZero.isMeteorSelected)
//                        {
//                            mZero.isMeteorSelected = true;
//                            if (!arms.isLaserEnable)
//                                gameControll.SetTargetEnemy(andHitZero);

//                            Debug.Log("Meteor_" + mZero.meteorID.ToString());

//                            StartShootLaser(andHitZero.collider.gameObject.transform.position);
//                        }


//                    }
//                }

                

////                if (touch.phase == TouchPhase.Began && touch.phase == TouchPhase.Stationary )
////                    //|| touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary
////                {
////
////                    //el target sigue a la posicion del dedo o mouse
////                    Ray andRay = mainCamera.ScreenPointToRay(touch.position);
////                    RaycastHit andHit;
////                    if (Physics.Raycast(andRay, out andHit, arms.distanceHit, arms.layerMaskHit))
////                    {
////                        if (andHit.collider.gameObject.CompareTag("Meteor"))
////                        {
////                            scrMeteorControll m = andHit.collider.gameObject.GetComponent<scrMeteorControll>();
////                            if (!m.isMeteorSelected)
////                            {
////                                //gameControll.poolTargetEnemy[lastMeteorID].GetComponent<scrMeteorControll>().isMeteorSelected = true;
////                                //listMeteorID.Add(newID);
////                                //lastMeteorID = newID;
////                                m.isMeteorSelected = true;
////                                if (!arms.isLaserEnable)
////                                    gameControll.SetTargetEnemy(andHit);
////
////                                Debug.Log("Meteor_" + m.meteorID.ToString());
////                                viewPortPosB = mainCamera.ScreenToViewportPoint(new Vector3(inputPos.x, inputPos.y, 0f));
////                                screenPosB = new Vector2(
////                                    ((viewPortPosB.x * recCrossHair.sizeDelta.x) - (recCrossHair.sizeDelta.x * 0.5f)),
////                                    ((viewPortPosB.y * recCrossHair.sizeDelta.y) - (recCrossHair.sizeDelta.y * 0.5f)));
////                                //restringo el area par aseleccionar
////                                recCrossHairB.anchoredPosition = screenPosB;
////                                StopCoroutine("StartRedCrossHair");
////                                StartCoroutine("StartRedCrossHair");
////                            }
////
////
////                        }
////                        else
////                        {
////                            StopCoroutine("StartRedCrossHair");
////                            StartCoroutine("StartRedCrossHair");
////                        }
////
////                        StartShootLaser(andHit.collider.gameObject.transform.position);
////                    }
////
////
////                    if (touch.position.y > (Screen.height * TouchRangeY) && touch.position.y < Screen.height)
////                    {
////                        
////                    }
////                        
////                }
//            }
//        }
//        else if (Application.platform == RuntimePlatform.WindowsEditor)
//        {
//            //Debug.Log("Press");
//            Ray winRayZero = mainCamera.ScreenPointToRay(crossHairC.transform.position);
//            RaycastHit winHitZero;

//            if (Physics.Raycast(winRayZero,out winHitZero, arms.distanceHit, arms.layerMaskHit))
//            {
//                if (winHitZero.collider.gameObject.CompareTag("Meteor"))
//                {
//                    scrMeteorControll mZero = winHitZero.collider.gameObject.GetComponent<scrMeteorControll>();
//                    if (!mZero.isMeteorSelected)
//                    {
//                        mZero.isMeteorSelected = true;
//                        if (!arms.isLaserEnable)
//                            gameControll.SetTargetEnemy(winHitZero);

//                        Debug.Log("Meteor_" + mZero.meteorID.ToString());

//                        StartShootLaser(winHitZero.collider.gameObject.transform.position);
//                    }


//                }
//            }

//            //------------------------------------------------------------



//            //el target sigue a la posicion del dedo o mouse
////            Ray winRay = mainCamera.ScreenPointToRay(Input.mousePosition);
////            RaycastHit winHit;
////            if (Physics.Raycast(winRay, out winHit, arms.distanceHit,arms.layerMaskHit.value))
////            {
////
////                //Debug.Log("NO x: " + hit.point.x.ToString() + " y: " + hit.point.y.ToString());
////
////                if (winHit.collider.gameObject.CompareTag("Meteor"))
////                {
////                    scrMeteorControll m = winHit.collider.gameObject.GetComponent<scrMeteorControll>();
////                    if (!m.isMeteorSelected)
////                    {
////                        //gameControll.poolTargetEnemy[lastMeteorID].GetComponent<scrMeteorControll>().isMeteorSelected = true;
////                        //listMeteorID.Add(newID);
////                        //lastMeteorID = newID;
////                        m.isMeteorSelected = true;
////                        if (!arms.isLaserEnable)
////                            gameControll.SetTargetEnemy(winHit);
////
////                        Debug.Log("Meteor_" + m.meteorID.ToString());
////
////
////
////                        viewPortPosB = mainCamera.ScreenToViewportPoint(new Vector3(inputPos.x, inputPos.y, 0f));
////                        screenPosB = new Vector2(
////                            ((viewPortPosB.x * recCrossHair.sizeDelta.x) - (recCrossHair.sizeDelta.x * 0.5f)),
////                            ((viewPortPosB.y * recCrossHair.sizeDelta.y) - (recCrossHair.sizeDelta.y * 0.5f)));
////                        //restringo el area par aseleccionar
////                        recCrossHairB.anchoredPosition = screenPosB;
////                        StopCoroutine("StartRedCrossHair");
////                        StartCoroutine("StartRedCrossHair");
////                    }
////
////                    StartShootLaser(winHit.collider.gameObject.transform);
////                }
////            }
////
//            if (Input.mousePosition.y > (Screen.height * TouchRangeY) && Input.mousePosition.y < Screen.height)
//            {
                


//            }

//        }


	}

    void FixedUpdate(){
        acceleration = new Vector3 (CnControls.CnInputManager.GetAxis ("Horizontal"), CnControls.CnInputManager.GetAxis ("Vertical"),0);
        fixedAcceleration = new Vector3 (acceleration.x, acceleration.y, speedPlayerZ);
        rb.velocity = fixedAcceleration * speedPlayer;

        //limito el movimiento del player

        rb.position = new Vector3 (
            Mathf.Clamp (rb.position.x, -playerRange.minRange.x, playerRange.maxRange.x), 
            Mathf.Clamp (rb.position.y, -playerRange.minRange.y, playerRange.maxRange.y), 
            rb.position.z
        );


        //si se activa lanzar laser
        if (!arms.isLaserEnable)
            return;

        if (Application.platform == RuntimePlatform.Android)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                    inputPos = touch.position;
                }
            }
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            inputPos = Input.mousePosition;
        }
        // Debug.Log(inputPos.y.ToString());

        //mueve el target gracias al mouse o dedo

        Vector2 viewPortPosA = mainCamera.ScreenToViewportPoint(new Vector3(inputPos.x, inputPos.y, 0f));
        Vector2 screenPosA = new Vector2(
            ((viewPortPosA.x * recCrossHair.sizeDelta.x) - (recCrossHair.sizeDelta.x * 0.5f)),
            ((viewPortPosA.y * recCrossHair.sizeDelta.y) - (recCrossHair.sizeDelta.y * 0.5f)));
        recCrossHairA.anchoredPosition = screenPosA;


        aniPlayer.SetFloat("inputH", CnControls.CnInputManager.GetAxis("Horizontal"));

        /*
        float tiltAroundX = 0f;
        float tiltAroundY = 0f;
        float smoth = 2.0f;
        float tiltAngle = 10.0f;
        if ((CnControls.CnInputManager.GetAxis("Horizontal") > 0.01f || CnControls.CnInputManager.GetAxis("Horizontal") < -0.01f))
        {
            tiltAroundX = CnControls.CnInputManager.GetAxis("Horizontal") * tiltAngle;
        }
        if ((CnControls.CnInputManager.GetAxis("Vertical") > 0.01f || CnControls.CnInputManager.GetAxis("Vertical") < -0.01f))
        {
            tiltAroundY = CnControls.CnInputManager.GetAxis("Vertical") * tiltAngle;
        }

        Quaternion target = Quaternion.Euler(-1f * tiltAroundY, tiltAroundX, 0);
        //rotacion
        rb.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smoth);


         Vector3 inputPos = new Vector3();



        if (inputPos.y > (Screen.height * TouchRangeY) && inputPos.y < Screen.height) {

            //restringo el area par aseleccionar
            recCrossHairA.anchoredPosition = new Vector3(
                Mathf.Clamp(screenPosA.x, -1200f, 1200f),
                Mathf.Clamp(screenPosA.y, -100f, 600f), 10f);
        }

        inputPos = new Vector3(
            Mathf.Clamp(inputPos.x,100f, 2460f),
            Mathf.Clamp(inputPos.y, 600f, 1340f), 10f);





                //rotar player por 2do joystick
                float tiltAroundX = CnControls.CnInputManager.GetAxis("Horizontal_2") * tiltAngle;
                float tiltAroundY = CnControls.CnInputManager.GetAxis("Vertical_2") * tiltAngle;
                Quaternion target = Quaternion.Euler( -1f * tiltAroundY,tiltAroundX, 0);
                //rotacion
                mainCamera.transform.rotation = Quaternion.Slerp(rb.transform.rotation, target, Time.deltaTime * smoth);
        
        

        viewPortTouchPos = mainCamera.WorldToViewportPoint(crossHairRefA.GetComponent<Transform>().position);
        movimiento de player x joystick 01


        ejecuto las animaciones player
        aniPlayer.SetFloat("inputH", CnControls.CnInputManager.GetAxis("Horizontal"));

        movimiento de los crosshair con el player
        crossHairA
        crossHairRefA.GetComponent<Transform>().position
                Vector2 viewPortPosA = mainCamera.WorldToViewportPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,40f));
                Vector2 screenPosA = new Vector2(
                    ((viewPortPosA.x * recCrossHair.sizeDelta.x) - (recCrossHair.sizeDelta.x * 0.5f)),
                    ((viewPortPosA.y * recCrossHair.sizeDelta.y) - (recCrossHair.sizeDelta.y * 0.5f)));
                recCrossHairA.anchoredPosition = screenPosA;



        crossHairA
        crossHairRefB.GetComponent<Transform>().position
        crossHair B
                Vector2 viewPortPosB = mainCamera.WorldToViewportPoint(crossHairRefB.GetComponent<Transform>().position);
                Vector2 screenPosB = new Vector2(
                    ((viewPortPosB.x * recCrossHair.sizeDelta.x) - (recCrossHair.sizeDelta.x * 0.5f)),
                    ((viewPortPosB.y * recCrossHair.sizeDelta.y) - (recCrossHair.sizeDelta.y * 0.5f)));
                recCrossHairB.anchoredPosition = screenPosB;
                */
    }

    void CalibrateAccelerometer()
    {
        accelerationSnapshot = Input.acceleration;

        rotateQuaternion = Quaternion.FromToRotation(new Vector3(0f, gradesAccelerometer, -1f), accelerationSnapshot);

        //create identity matrix ... rotate our matrix to match up with down vec
        Matrix4x4 matrix = Matrix4x4.TRS(new Vector3(0,0,0), rotateQuaternion, new Vector3(1f, 1f, 1f));
        //get the inverse of the matrix
        calibrationMatrix = matrix.inverse;

    }

    public void PlayerDestroyed(){
        Instantiate (explotionPlayer);
        //ActivePlayer = false;
        gameObject.SetActive (false);
    }

    public void PlayerShoot(Lean.LeanFinger f){

        // SE USA PARA DISPARA EN CUALQUIER LUGAR con el finger o mouse tap
        //Transform ti = crossHairA.transform;
        Vector3 v = f.GetWorldPosition(arms.distanceHit, mainCamera);

        StartShootLaser(v);
     

        //Vector2 viewPortPosB;
        //Vector2 screenPosB;
        //Ray winRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        //RaycastHit winHit;

        //if (Physics.Raycast(winRay, out winHit, arms.distanceHit, arms.layerMaskHit.value))
        //{
        //    //SOLO CUANDO HIT UN METEORO
        //    //Debug.Log("NO x: " + hit.point.x.ToString() + " y: " + hit.point.y.ToString());

        //    if (winHit.collider.gameObject.CompareTag("Meteor") || winHit.collider.gameObject.CompareTag("MeteorPath"))
        //    {
        //        scrMeteorControll m = winHit.collider.gameObject.GetComponent<scrMeteorControll>();
        //        if (!m.isMeteorSelected)
        //        {
        //            //gameControll.poolTargetEnemy[lastMeteorID].GetComponent<scrMeteorControll>().isMeteorSelected = true;
        //            //listMeteorID.Add(newID);
        //            //lastMeteorID = newID;
        //            m.isMeteorSelected = true;
        //            if (!arms.isLaserEnable)
        //                gameControll.SetTargetEnemy(winHit);

        //            Debug.Log("Meteor_" + m.meteorID.ToString());



        //            viewPortPosB = mainCamera.ScreenToViewportPoint(new Vector3(inputPos.x, inputPos.y, 0f));
        //            screenPosB = new Vector2(
        //                ((viewPortPosB.x * recCrossHair.sizeDelta.x) - (recCrossHair.sizeDelta.x * 0.5f)),
        //                ((viewPortPosB.y * recCrossHair.sizeDelta.y) - (recCrossHair.sizeDelta.y * 0.5f)));
        //            //restringo el area par aseleccionar
        //            recCrossHairB.anchoredPosition = screenPosB;
        //            StopCoroutine("StartRedCrossHair");
        //            StartCoroutine("StartRedCrossHair");
        //        }

        //        StartShootLaser(winHit.collider.gameObject.transform.position);
        //        Debug.Log("JHIT");
        //    }
        //}


    }

    IEnumerator StartRedCrossHair(){
        crossHairB.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        crossHairB.SetActive(false);
    }

    #region laser

    GameObject GetObjPoolLaser(){
        for (int i = 0; i < poolLaser.Count; i++)
        {
            if (!poolLaser[i].activeInHierarchy)
                return poolLaser[i];
        }

        GameObject obj = (GameObject)Instantiate(arms.laser);
        obj.SetActive(false);
        poolLaser.Add(obj);
        return obj;
    }

    void CreatePools(){
        
        GameObject LASERS = new GameObject();
        LASERS.name = "GO_LASERS";
        for (int i = 0; i < arms.amountLaser; i++)
        {
            GameObject obj = (GameObject)Instantiate(arms.laser);
            obj.transform.parent = LASERS.transform;
            obj.name = "laser_" + i.ToString("000");
            obj.GetComponent<MoveLaser>().speed = 0;
            //obj.GetComponent<MoveLaser>().targetPoint = transform;
            obj.SetActive(false);
            poolLaser.Add(obj);
        }

    }

    //public void StartShootLaser(){
    //    if (!arms.isShooting)
    //        StartCoroutine(SpawnLaserWave());
        
    //}
    //disparo al tocar la pantalla con el mouse o dedo
    public void StartShootLaser(Vector3 t){
        if (!arms.isShooting)
            StartCoroutine(SpawnLaserWave(t));

    }
    public void StopShootLaser(){
        arms.isShooting = false;
    }

    //IEnumerator SpawnLaserWave(){
    //    Debug.Log("shoot");
    //    arms.isShooting = true;
    //    yield return null;

    //    while (arms.isShooting)
    //    {
    //        yield return new WaitForSeconds(0.2f);

    //        GameObject hazardL, hazardR = null;
    //        hazardL = GetObjPoolLaser();

    //        hazardL.GetComponent<Transform>().position = arms.shotSpawnL.position;
    //        hazardL.GetComponent<Transform>().rotation = arms.shotSpawnL.rotation;
    //        hazardL.GetComponent<MoveLaser>().speed = arms.speedLaser;
    //        hazardL.SetActive(true);

    //        hazardR = GetObjPoolLaser();
    //        hazardR.GetComponent<Transform>().position = arms.shotSpawnR.position;
    //        hazardR.GetComponent<Transform>().rotation = arms.shotSpawnR.rotation;
    //        hazardR.GetComponent<MoveLaser>().speed = arms.speedLaser;

    //        hazardR.SetActive(true);

    //        yield return new WaitForSeconds(0.2f);

    //    }

    //    yield return null;
    //    arms.isShooting = false;
    //}

    IEnumerator SpawnLaserWave(Vector3 targetPoint){
        
        //Debug.Log("shoot");
        arms.isShooting = true;
        yield return null;

        while (arms.isShooting)
        {
            //yield return new WaitForSeconds(0.1f);

            GameObject hazardL, hazardR = null;
            hazardL = GetObjPoolLaser();

            hazardL.GetComponent<Transform>().position = arms.shotSpawnL.position;
            // obtengo la posicion del player conrespecto a la nave que dispara
            Vector3 offset = targetPoint - hazardL.GetComponent<Transform>().position;
            hazardL.GetComponent<Transform>().rotation = Quaternion.LookRotation(offset); 
            //arms.shotSpawnL.rotation;
            hazardL.GetComponent<MoveLaser>().speed = arms.speedLaser;
            hazardL.SetActive(true);

            hazardR = GetObjPoolLaser();
            hazardR.GetComponent<Transform>().position = arms.shotSpawnR.position;
            Vector3 offsetB = targetPoint - hazardR.GetComponent<Transform>().position;
            hazardR.GetComponent<Transform>().rotation = Quaternion.LookRotation(offsetB); 
                //arms.shotSpawnR.rotation;
            hazardR.GetComponent<MoveLaser>().speed = arms.speedLaser;

            hazardR.SetActive(true);

            yield return null;
            //yield return new WaitForSeconds(0.15f);
            break;

        }

        yield return null;
        arms.isShooting = false;
    }
//    IEnumerator SetPools(){
//
//        GameObject LASERS = new GameObject();
//        for (int i = 0; i < arms.amountLaser; i++)
//        {
//            GameObject obj = (GameObject)Instantiate(arms.laser);
//            obj.transform.parent = LASERS.transform;
//            obj.name = "laser_" + i.ToString("000");
//            obj.GetComponent<MoveLaser>().speed = 0;
//            obj.SetActive(false);
//            poolLaser.Add(obj);
//        }
//        yield return null;
//        //yield return new WaitForSeconds(0.01f);
//    }

    #endregion


}

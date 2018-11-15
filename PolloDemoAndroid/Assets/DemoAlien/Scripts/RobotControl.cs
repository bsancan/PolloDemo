using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

[System.Serializable]
public class Done_Boundary 
{
	public float xMin, xMax, yMin, yMax;
}


public class RobotControl : MonoBehaviour {
//    public Camera _camera;
	public bool movimientoXY;
	public float grados;
	public Done_Boundary boundary;
	public float speed;

	public GameObject explosionPlayer;

//    //crosshair
//    public Canvas cnvCrossHair;
//    public GameObject GoCrossHair01;
//    public GameObject refCrossHair01;
//    public GameObject GoCrossHair02;
//    public GameObject refCrossHair02;
//
//    public GameObject laser01;
//
//    public Transform Laser01Left;
//    public Transform Laser01Right;

	Rigidbody rb;
	Animator aniPlayer;

	//Accelerometro
	Vector3 accelerationSnapshot ;
	Quaternion rotateQuaternion;
	Matrix4x4 calibrationMatrix;


	public Vector3 acceleration;
	public Vector3 fixAcceleration ;
//	public Vector3 movement;

   

	void Start () {
		rb = GetComponent<Rigidbody> ();
		CalibrateAccelerometer ();
		aniPlayer = GetComponent<Animator> ();
        aniPlayer.SetBool("StartFly", true);
        //crosshair


//        StartCoroutine(StartLaser01());
	}
	

	void Update () {
//		_cam.GetComponent<Transform>().position = new Vector3 (rb.position.x,rb.position.y + 1, _cam.GetComponent<Transform>().position.z);
        if (acceleration.x == 0)
        {
            //para pc
//            movement.x = Input.GetAxis("Horizontal") * speed;
            
//			movement.x = CnControls.CnInputManager.GetAxis("Horizontal") * speed;
//			movement.y = CnControls.CnInputManager.GetAxis("Vertical") * speed;

        }
        else
        {
            //android
//            movement = acceleration * speed;
//            aniPlayer.SetFloat("inputH", acceleration.x);
        }
		


	}

	void FixedUpdate(){
		//__ ACCELLERATOR - input del acelerometro
//		acceleration = getAccelerometer (Input.acceleration);
        if (acceleration.x == 0)
        {
            //para pc
//            rb.velocity = movement;
        }
        else
        {
            //mobiilidad del robot
          
         
        }
//        Vector3 offset;
        float smoth = 2.0f;
        float tiltAngle = 30.0f;

        // obtengo la posicion del player conrespecto a la nave que dispara
//        offset = new Vector3 (CnControls.CnInputManager.GetAxis ("Horizontal_2") * tiltAngle, 
//            CnControls.CnInputManager.GetAxis ("Vertical_2") * tiltAngle,0);
        float tiltAroundX = CnControls.CnInputManager.GetAxis("Horizontal_2") * tiltAngle;
        float tiltAroundY = CnControls.CnInputManager.GetAxis("Vertical_2") * tiltAngle;
        Quaternion target = Quaternion.Euler( -1f * tiltAroundY,tiltAroundX, 0);
        //rotacion
        rb.rotation = Quaternion.Slerp(rb.transform.rotation, target, Time.deltaTime * smoth);

//        //MOVE CROSSHAIR
//        //first you need the RectTransform component of your canvas
//        RectTransform CanvasRect = cnvCrossHair.GetComponent<RectTransform>();
//        //CROSSHAIR 01
//        RectTransform UI_Element01 = GoCrossHair01.GetComponent<RectTransform>();
//        Vector2 ViewportPosition01 = _camera.WorldToViewportPoint(refCrossHair01.GetComponent<Transform>().position);
//        Vector2 ScreenPosition01 = new Vector2(
//                                        ((ViewportPosition01.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
//                                        ((ViewportPosition01.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f))
//                                    );
//        UI_Element01.anchoredPosition = ScreenPosition01;
//
//        //CrossHair 02
//        RectTransform UI_Element02 = GoCrossHair02.GetComponent<RectTransform>();
//        Vector2 ViewportPosition02 = _camera.WorldToViewportPoint(refCrossHair02.GetComponent<Transform>().position);
//        Vector2 ScreenPosition02 = new Vector2(
//            ((ViewportPosition02.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
//            ((ViewportPosition02.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f))
//        );
//        UI_Element02.anchoredPosition = ScreenPosition02;


		//para manejo de joystick
		acceleration = new Vector3 (CnControls.CnInputManager.GetAxis ("Horizontal"), 
			CnControls.CnInputManager.GetAxis ("Vertical"),0);
		fixAcceleration = new Vector3 (acceleration.x, acceleration.y,0.0f);

		rb.velocity = fixAcceleration * speed;

//		float dec = Mathf.Round (movement.x * 100) / 100; 
//		if (!movimientoXY)
//			fixAcceleration = new Vector3(dec,0f,0f);
//		else
//			fixAcceleration = new Vector3(dec,movement.y,0f);
//
//		rb.velocity = fixAcceleration;


		rb.position = new Vector3 (
			Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax), 
			Mathf.Clamp (rb.position.y, boundary.yMin, boundary.yMax), 
			0f
		);

		aniPlayer.SetFloat("inputH", CnControls.CnInputManager.GetAxis("Horizontal"));

		//GetComponent<Rigidbody> ().velocity = new Vector3(0,0,0);

	}

	public void CalibrateAccelerometer()
	{
		accelerationSnapshot = Input.acceleration;

		rotateQuaternion = Quaternion.FromToRotation(new Vector3(0f, grados, -1f), accelerationSnapshot);
	
		//create identity matrix ... rotate our matrix to match up with down vec
		Matrix4x4 matrix = Matrix4x4.TRS(new Vector3(0,0,0), rotateQuaternion, new Vector3(1f, 1f, 1f));
		//get the inverse of the matrix
		calibrationMatrix = matrix.inverse;

	}

	//Method to get the calibrated input 
	Vector3 getAccelerometer(Vector3 accelerator){
		Vector3 accel = this.calibrationMatrix.MultiplyVector(accelerator);
		return accel;
	}

	public void PlayerDestroyed(){
		Instantiate (explosionPlayer);
		gameObject.SetActive (false);
	}

//    IEnumerator StartLaser01(){
//        bool loop = true;
//        yield return new WaitForSeconds(1f);
//
//        while (loop)
//        {
//            yield return new WaitForSeconds(0.5f);
//            Instantiate(laser01,Laser01Left.position,Laser01Left.rotation);
//            Instantiate(laser01,Laser01Right.position,Laser01Right.rotation);
//        }
//
//
//        
//    }
}

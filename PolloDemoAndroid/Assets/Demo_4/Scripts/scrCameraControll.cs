using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
//public class CameraRange{
//    public Vector2 minRange;
//    public Vector2 maxRange;
//}

public class scrCameraControll : MonoBehaviour {
    public GameObject goPlayer;
    private Transform player;
    public float speedCamera;
    //public CameraRange cameraRange;
    public bool isFollowingPlayer;
    public bool enableRotation;
    public Vector3 cameraPos;
    private Vector3 mainCamera;
    private Vector3 postionA;
    private Vector3 postionB;
    //private Vector3 startPostionCamera;
    private scrPlayerControll pcontrol;

    void Awake(){
        mainCamera = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        pcontrol = goPlayer.GetComponent<scrPlayerControll>();
        player = goPlayer.GetComponent<Transform>();

//        if (!isFollowingPlayer)
//        {
//            Vector3 min = player.GetComponent<scrPlayerControll>().playerRange.minRange;
//            Vector3 max = player.GetComponent<scrPlayerControll>().playerRange.maxRange;
//
//            cameraRange.minRange = min;
//            cameraRange.maxRange = max;
//        }
    }

	void Start () {
        //startPostionCamera = transform.position;
        postionA = transform.position;
	}

	void Update () {
		
	}

    void FixedUpdate(){
        if (!isFollowingPlayer)
        {
//            postionA = transform.position;
//            postionB = new Vector3(Mathf.Clamp(player.position.x, cameraRange.minRange.x, cameraRange.maxRange.x), Mathf.Clamp(player.position.y, cameraRange.minRange.y, cameraRange.maxRange.y), transform.position.z);
//            transform.position = Vector3.Lerp(postionA, postionB, Time.deltaTime * speedCamera);
//
        }
        else
        {
            //postionB = new Vector3(Mathf.Clamp(player.position.x, cameraRange.minRange.x, cameraRange.maxRange.x), Mathf.Clamp(player.position.y, cameraRange.minRange.y, cameraRange.maxRange.y), transform.position.z);
            postionA = new Vector3(Mathf.Clamp(transform.position.x, -pcontrol.playerRange.minRange.x + 0.8f, pcontrol.playerRange.maxRange.x - 0.8f)
                , Mathf.Clamp(transform.position.y, -pcontrol.playerRange.minRange.y + 0.2f, pcontrol.playerRange.maxRange.y - 0.2f)
                , transform.position.z);
               

            postionB = new Vector3( player.position.x, player.position.y + cameraPos.y , player.position.z + cameraPos.z);
            transform.position = Vector3.Lerp(postionA, postionB, Time.deltaTime * speedCamera);

            //transform.position = postionB;
            //Debug.Log(CnControls.CnInputManager.GetAxis("Horizontal").ToString());
            //rotar player por 2do joystick
           
            if (enableRotation)
            {
                float tiltAroundX = 0f;
                float tiltAroundY = 0f;
                float smoth = 10.0f;
                float tiltAngle = 5.0f;
                if ((CnControls.CnInputManager.GetAxis("Horizontal") > 0.01f || CnControls.CnInputManager.GetAxis("Horizontal") < -0.01f))
                {
                    tiltAroundX = CnControls.CnInputManager.GetAxis("Horizontal") * tiltAngle;
                }
                if ((CnControls.CnInputManager.GetAxis("Vertical") > 0.01f || CnControls.CnInputManager.GetAxis("Vertical") < -0.01f))
                {
                    tiltAroundY = CnControls.CnInputManager.GetAxis("Vertical") * tiltAngle;
                }

                Quaternion target = Quaternion.Euler( -1f * tiltAroundY,tiltAroundX, 0);
                //rotacion
                transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smoth);
                player.transform.rotation = transform.rotation;
            }
        }
            
    }
}

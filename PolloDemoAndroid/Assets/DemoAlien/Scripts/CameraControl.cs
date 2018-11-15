using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	public Transform m_player;
	public float speedTime;
    public Vector3 m_camRange;
    private Vector3 m_cam;

	// Use this for initialization
	void Start () {
		m_cam = new Vector3 (transform.position.x, transform.position.y, transform.position.z);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void FixedUpdate(){
		

		transform.position = Vector3.Lerp(transform.position , 
            new Vector3( Mathf.Clamp( m_player.position.x, - m_camRange.x,m_camRange.x)
                , Mathf.Clamp( m_player.position.y + m_cam.y, - m_camRange.y + m_cam.y, m_camRange.y)
                , transform.position.z), 
			Time.deltaTime * speedTime);
	}
}

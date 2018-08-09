using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputController : MonoBehaviour {

    static int s_DeadHash = Animator.StringToHash("Dead");
    static int s_RunStartHash = Animator.StringToHash("runStart");
    static int s_MovingHash = Animator.StringToHash("Moving");
    static int s_JumpingHash = Animator.StringToHash("Jumping");
    static int s_JumpingSpeedHash = Animator.StringToHash("JumpSpeed");
    static int s_SlidingHash = Animator.StringToHash("Sliding");

    public Character character;
    public float SpeedForward;
    public float Speed;

    public int maxLife = 3;
    public float laneChangeSpeed = 1.0f;

    public int currentLife { get { return m_CurrentLife; } set { m_CurrentLife = value; } }


    protected int m_CurrentLife;

    protected void Awake()
    {
 
        m_CurrentLife = 0;
  
    }

    public void Init()
    {
        //transform.position = k_StartingPosition;
        //m_TargetPosition = Vector3.zero;

        //m_CurrentLane = k_StartingLane;
        //characterCollider.transform.localPosition = Vector3.zero;

        currentLife = maxLife;

        //m_Audio = GetComponent<AudioSource>();

        //m_ObstacleLayer = 1 << LayerMask.NameToLayer("Obstacle");
    }


    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    protected void Update() {

		
	}

    protected virtual void LateUpdate()
    {

        transform.position = transform.position + (transform.forward * SpeedForward * Time.deltaTime);
        // This will move the current transform based on a finger drag gesture
        TouchSystem.MoveObjectInX(transform, TouchSystem.DragDelta, Speed);
    }
}

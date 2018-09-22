using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class CharacterCollider : MonoBehaviour {

    static int s_HitHash = Animator.StringToHash("Hit");
    static int s_BlinkingValueHash;

    public struct DeathEvent
    {
        public string character;
       // public string obstacleType;
       // public string themeUsed;
        //public int coins;
       // public int premium;
        public int score;
        public float worldDistance;
    }

    public CharacterInputController controller;


   // [Header("Sound")]
    public DeathEvent deathData { get { return m_DeathData; } }
    public new BoxCollider collider { get { return m_Collider; } }

    public new AudioSource audio { get { return m_Audio; } }

    protected bool m_Invincible;
    protected DeathEvent m_DeathData;
    protected BoxCollider m_Collider;
    protected AudioSource m_Audio;

    protected const float k_DefaultInvinsibleTime = 2f;

    void Start () {
        m_Collider = GetComponent<BoxCollider>();
        m_Audio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetInvincibleExplicit(bool invincible)
    {
        m_Invincible = invincible;
    }

    public void SetInvincible(float timer = k_DefaultInvinsibleTime)
    {
        StartCoroutine(InvincibleTimer(timer));
    }

    protected IEnumerator InvincibleTimer(float timer)
    {
        m_Invincible = true;

        float time = 0;
        float currentBlink = 1.0f;
        float lastBlink = 0.0f;
        const float blinkPeriod = 0.1f;

        while (time < timer && m_Invincible)
        {
            Shader.SetGlobalFloat(s_BlinkingValueHash, currentBlink);

            // We do the check every frame instead of waiting for a full blink period as if the game slow down too much
            // we are sure to at least blink every frame.
            // If blink turns on and off in the span of one frame, we "miss" the blink, resulting in appearing not to blink.
            yield return null;
            time += Time.deltaTime;
            lastBlink += Time.deltaTime;

            if (blinkPeriod < lastBlink)
            {
                lastBlink = 0;
                currentBlink = 1.0f - currentBlink;
            }
        }

        Shader.SetGlobalFloat(s_BlinkingValueHash, 0.0f);

        m_Invincible = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpaceShip : MonoBehaviour
{

    public Transform spot;
    private Animator enemyAnimator;
    //public GameObject laser;
    private float laserTimer = 1f;
    private float laserSpeed = -20f;

    //private Transform playerPivot;
    //private Vector3 offset;

    private void Awake()
    {
        enemyAnimator = GetComponent<Animator>();
    }

    void Start()
    {
        //offset = new Vector3(0f, 0f, 15f);
        //playerPivot = CharacterManager.characterManagerInstance.transform;
  
        //StartCoroutine(CorSpawn());
        
    }

   public void PlayEnemySpaceShipAnimation()
    {
        enemyAnimator.SetTrigger("Pos01");
        //StartCoroutine(CorSpawn());
    }

    void Update()
    {
        //transform.position = offset + playerPivot.position;
    }

    public void SetEnemyValues(float laserSpeed, float laserTimer)
    {
        this.laserSpeed = laserSpeed;
        this.laserTimer = laserTimer;
    }

   

    IEnumerator CorSpawn()
    {
        yield return null;

        while (true)
        {
            yield return new WaitForSeconds(laserTimer);

            Transform tra = AmmoManager.ammoManagerInstance.SpawnEnemyAmmo(transform.position);
            tra.GetComponent<EnemyAmmo>().speed = laserSpeed;
            
            
            
            //GameObject go = Instantiate(laser, Vector3.zero, Quaternion.identity) as GameObject;

            //go.transform.position = spot.position;
            //go.GetComponent<EnemyAmmo>().speed = laserSpeed;
            //go.SetActive(true);
        }
    }
}

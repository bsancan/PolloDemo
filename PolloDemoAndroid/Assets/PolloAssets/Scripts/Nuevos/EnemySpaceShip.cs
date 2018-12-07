using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpaceShip : MonoBehaviour
{
    //2 = low2  3 = low3
    public int typeSpaceShip;
    public Transform spot;
    private Animator enemyAnimator;
    //public GameObject laser;
    public float laserLifeTime = 1f;
    private float laserTimer = 1f;
    private float laserWaveTimer = 0.5f;
    private float laserSpeed = -20f;
    public float sphereLifeTime = 1f;
    private float sphereSpeedRotation = 1f;
    private int laserValueDamage = 5;

    private int typeAmmo = 0;
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

   public void PlayEnemySpaceShipAnimation(int opc, int typeAmmo)
    {
        this.typeAmmo = typeAmmo;
        //1- pos01  2-pos02_A 3-pos02_B
        if (opc == 1)
        {
            enemyAnimator.SetTrigger("Pos01");
            StartCoroutine(CorSpawn());

        }
        else if(opc == 2)
        {
            enemyAnimator.SetTrigger("Pos02_A");
        }
        else if (opc == 3)
        {
            enemyAnimator.SetTrigger("Pos02_B");
        }
        
        //StartCoroutine(CorSpawn());
    }

    void Update()
    {
        //transform.position = offset + playerPivot.position;
    }

    public void SetEnemyValues(float laserSpeed, float laserTimer, float laserWaveTimer, int laserValueDamage, float sphereSpeedRotation)
    {
        this.laserSpeed = laserSpeed;
        this.laserTimer = laserTimer;
        this.laserWaveTimer = laserWaveTimer;
        this.laserValueDamage = laserValueDamage;
        this.sphereSpeedRotation = sphereSpeedRotation;
    }


    public void SpaceAnimationEnd()
    {
        gameObject.SetActive(false);
    }

    public void StartSphereSpawn()
    {
        StartCoroutine(CorSphereSpawn(2));
    }

    IEnumerator CorSpawn()
    {
        yield return null;
        int count = 0;
        yield return new WaitForSeconds(laserTimer);
        while (count <2)
        {
            yield return new WaitForSeconds(laserWaveTimer);
            spot.LookAt(CharacterManager.characterManagerInstance.character.transform);
            Transform tra = AmmoManager.ammoManagerInstance.SpawnEnemyAmmo(spot);
            EnemyAmmo ammo = tra.GetComponent<EnemyAmmo>();
            ammo.speed = laserSpeed;
            ammo.valueDamage = laserValueDamage;
            ammo.lifeTime = laserLifeTime;
            ammo.speedRotation = sphereSpeedRotation;
            ammo.type = typeAmmo;
            ammo.AmmoActived();
            count++;
            //GameObject go = Instantiate(laser, Vector3.zero, Quaternion.identity) as GameObject;

            //go.transform.position = spot.position;
            //go.GetComponent<EnemyAmmo>().speed = laserSpeed;
            //go.SetActive(true);
        }
    }

    IEnumerator CorSphereSpawn(int typeAmmo)
    {
        yield return null;
        int count = 0;
        //yield return new WaitForSeconds(laserTimer);
        while (count < 3)
        {
            //yield return new WaitForSeconds(laserWaveTimer);
            spot.LookAt(CharacterManager.characterManagerInstance.character.transform);
            Transform tra = AmmoManager.ammoManagerInstance.SpawnEnemyAmmoSphere(spot);
            EnemyAmmo ammo = tra.GetComponent<EnemyAmmo>();
            ammo.speed = laserSpeed;
            ammo.valueDamage = laserValueDamage;
            //ammo.lifeTime = laserLifeTime;
            ammo.speedRotation = sphereSpeedRotation;
            ammo.type = typeAmmo;
            ammo.AmmoActived();
            yield return new WaitForSeconds(0.5f);
            count++;
            //GameObject go = Instantiate(laser, Vector3.zero, Quaternion.identity) as GameObject;

            //go.transform.position = spot.position;
            //go.GetComponent<EnemyAmmo>().speed = laserSpeed;
            //go.SetActive(true);
        }
    }
}

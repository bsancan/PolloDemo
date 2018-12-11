using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpaceShip : MonoBehaviour
{
    //1     2   3
    //public int typeSpaceShip;
    public Transform spot;
    public int currentStamina = 100;
    private Animator enemyAnimator;
    public int typeAmmo = 0;
    public int posInitialID = 0;
    //public GameObject laser;

    //variabes para laser
    [Header("LASERS")]
    public int laserValueDamage = 5;
    public float laserLifeTime = 1f;
    public int laserRound = 2;
    public float waitForNextLaserRound = 1f;
    public float laserRoundTimer = 0.5f;
    public float laserSpeed = -20f;
    public float sphereLifeTime = 1f;
    // private float sphereSpeedRotation = 1f;
    [Header("Esferas")]
    public float sphereSpeed = 1f;
    public int sphereValueDamage = 10;


   
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

    public void StartShip01_A()
    {
        //enemyAnimator.SetTrigger("Ship01_A");

        StartCoroutine(CorShip01_A_LaserSpawn());

    }

    void Update()
    {
        //transform.position = offset + playerPivot.position;
    }

    //public void SetEnemyValues(float laserSpeed, float laserTimer, float laserWaveTimer, int laserValueDamage)
    //{
    //    this.laserSpeed = laserSpeed;
    //    this.laserTimer = laserTimer;
    //    this.laserWaveTimer = laserWaveTimer;
    //    this.laserValueDamage = laserValueDamage;
    //    //this.sphereSpeedRotation = sphereSpeedRotation;
    //}


    //public void SetEnemyValues(float sphereSpeed, int sphereValueDamage)
    //{
    //    this.sphereSpeed = sphereSpeed;
    //    //this.sphereSpeedRotation = sphereSpeedRotation;
    //    this.sphereValueDamage = sphereValueDamage;
    //}


    public void SpaceAnimationEnd()
    {
        gameObject.SetActive(false);
    }

    public void TakeDamage(int damageFromPlayer)
    {
        int sum = currentStamina - damageFromPlayer;
        if (sum > 0)
        {
            currentStamina = sum;
            UIManager.uiManagerInstance.ShowNegativePoints(damageFromPlayer, transform.position);

            //print(currentStamina);
        }
        else
        {
            if (gameObject.activeInHierarchy)
            {
                gameObject.SetActive(false);
                UIManager.uiManagerInstance.ShowNegativePoints(damageFromPlayer, transform.position);
                ExplosionManager.explosionManagerInstance.SpawnExplosion(transform.position, 1f);
                //cuento la destruccion como puntaje
                UIManager.uiManagerInstance.scoreManager.currentPlayerScore += damageFromPlayer;
                EnemySpaceShipManager.enemySpaceShipManagerInstance.AddEnemyShipDestroyed(posInitialID);
            }
            currentStamina = 0;
        }

    }

    IEnumerator CorShip01_A_LaserSpawn()
    {
        while (true)
        {
                int count = 0;
            yield return new WaitForSeconds(waitForNextLaserRound);
            while (count < laserRound)
            {
                yield return new WaitForSeconds(laserRoundTimer);
                spot.LookAt(CharacterManager.characterManagerInstance.character.transform);
                Transform tra = AmmoManager.ammoManagerInstance.SpawnEnemyAmmo(spot);
           
                EnemyAmmo ammo = tra.GetComponent<EnemyAmmo>();
                ammo.speed = laserSpeed;
                ammo.valueDamage = laserValueDamage;
                ammo.lifeTime = laserLifeTime;
                ammo.type = typeAmmo;
                ammo.AmmoActived();
                count++;
            }
        }


    }



    //============pruebas

    public void PlayEnemySpaceShipAnimation(int opc, int typeAmmo)
    {
        this.typeAmmo = typeAmmo;
        //1- pos01  2-pos02_A 3-pos02_B
        if (opc == 1)
        {
            enemyAnimator.SetTrigger("Pos01");
            StartCoroutine(CorSpawn());

        }
        else if (opc == 2)
        {
            enemyAnimator.SetTrigger("Pos02_A");
        }
        else if (opc == 3)
        {
            enemyAnimator.SetTrigger("Pos02_B");
        }

        //StartCoroutine(CorSpawn());
    }

    public void StartSphereSpawn()
    {
        StartCoroutine(CorSphereSpawn(2));
    }


    IEnumerator CorSpawn()
    {
        yield return null;
        int count = 0;
        yield return new WaitForSeconds(waitForNextLaserRound);
        while (count <2)
        {
            yield return new WaitForSeconds(laserRoundTimer);
            spot.LookAt(CharacterManager.characterManagerInstance.character.transform);
            Transform tra = AmmoManager.ammoManagerInstance.SpawnEnemyAmmo(spot);
            EnemyAmmo ammo = tra.GetComponent<EnemyAmmo>();
            ammo.speed = laserSpeed;
            ammo.valueDamage = laserValueDamage;
            ammo.lifeTime = laserLifeTime;
            //ammo.speedRotation = sphereSpeedRotation;
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
            ammo.speed = sphereSpeed;
            ammo.valueDamage = sphereValueDamage;
            //ammo.lifeTime = laserLifeTime;
            //ammo.speedRotation = sphereSpeedRotation;
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

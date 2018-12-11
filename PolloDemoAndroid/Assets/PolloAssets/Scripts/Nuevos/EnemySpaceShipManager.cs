using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpaceShipManager : MonoBehaviour
{
    public static EnemySpaceShipManager enemySpaceShipManagerInstance;

    [Header("EspaceShipPrefabs")]
    [SerializeField]
    private GameObject enemySpaceShip02Prefab;
    [SerializeField]
    private int enemySpaceShip02PoolSize = 10;
    [SerializeField]
    private float enemySpaceShip02Timer = 2f;

    [Header("EspaceShip 01")]
    [SerializeField]
    private bool enemyShip01;
    [SerializeField]
    private int enemyShip01_valueStamina = 60;
    [SerializeField]
    private bool enemyShip01_A_isActive;                                             //Numero de round que apareceran las naves
    [SerializeField]
    private int enemyShip01_A_laserRound = 2;                                     //cuantos lasers puede disparar hasta la siguiente ronda
    [SerializeField]
    private float enemyShip01_A_laserRoundTimer = 0.5f;                         //tiempo entre laser
   [SerializeField]
    private float enemyShip01_A_WaitForNextLaserRound = 2f;                     //tiempo de espera para la siguiente ronda de lasers
    [SerializeField]
    private float enemyShip01_A_laserLifeTime = 1f;                               //el timepo q tarda en desactivarse cuando no golpea al player
    [SerializeField]
    private float enemyShip01_A_laserSpeed = 50f;                                 //velocidad del laser
    [SerializeField]
    private int enemyShip01_A_valueDamage = 10;                                   //daño que genera el laser
    [SerializeField]
    private Vector3[] enemyShip01_A_positions;                                    //posiciones de spawm para las naves
                                  
    private List<int> enemyShip01_A_enemyDestroyed;


    [SerializeField]
    private int enemyShip01_RepMovB;





    [Header("EspaceShip 02")]
    [SerializeField]
    private bool enemyShip02;
    [SerializeField]
    private int enemyShip02_valueStamina = 60;
    [SerializeField]
    private int enemyShip02_RepMovA;
    [SerializeField]
    private int enemyShip02_RepMovB;

    [Header("EspaceShip 03")]
    [SerializeField]
    private bool enemyShip03;
    [SerializeField]
    private int enemyShip03_valueStamina = 60;
    [SerializeField]
    private int enemyShip03_RepMovA;
    [SerializeField]
    private int enemyShip03_RepMovB;



    //[SerializeField]
    //private float laserTimer = 0.75f;
    //[SerializeField]
    //private float laserWaveTimer = 0.5f;
    //[SerializeField]
    //private float laserSpeed = -80f;
    //[SerializeField]
    //private int laserValueDamage = 5;
    [SerializeField]
    private float spaceWaveTimerA = 1f;
    [SerializeField]
    private float sphereSpeed = -80f;
    [SerializeField]
    private float sphereSpeedRotation = 1f;
    [SerializeField]
    private int sphereValueDamage = 10;
    [SerializeField]
    private float spaceWaveTimerB = 1f;
    [SerializeField]
    private Vector3[] posSpaceShip02;
    [SerializeField]
    private Vector3[] posSpaceShip02_B;

    private Vector3 offset;

    public Queue<Transform> enemySpaceShip02Queue = new Queue<Transform>();

    //para controlar las olas de las diferentes naves
   // [SerializeField]
   


  void Start()
    {
        if(enemySpaceShipManagerInstance == null)
        {
            enemySpaceShipManagerInstance = this;
        }else
        {
            Destroy(gameObject);
        }

        offset = transform.position;
        //FALTA CREAR LAS DEMAS NAVES 
        CreateEnemySpaceShip02();

        if(GameManager.gameManagerInstance.currentLevel == 2)
        {
            //enemyShip01_DicA = new Dictionary<int, Vector3>();
            //for (int i = 0; i < enemyShip01_A_positions.Length; i++)
            //{
            //    enemyShip01_DicA.Add(i, enemyShip01_A_positions[i]);
            //}

            StartCoroutine(CorStartEnemySpawn_01_A());
        }
        //StartCoroutine(CorStartEnemySpawnA());
        //StartCoroutine(CorStartEnemySpawnB());

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = CharacterManager.characterManagerInstance.transform.position + (CharacterManager.characterManagerInstance.transform.forward * offset.z);
        transform.rotation = CharacterManager.characterManagerInstance.transform.rotation;
    }

    private void CreateEnemySpaceShip02()
    {

        for (int i = 0; i < enemySpaceShip02PoolSize; i++)
        {
            GameObject go = Instantiate(enemySpaceShip02Prefab, Vector3.zero, Quaternion.identity) as GameObject;
            Transform objTrans = go.transform;
            objTrans.parent = transform;
            objTrans.gameObject.name = objTrans.gameObject.name + "_" + i;
            enemySpaceShip02Queue.Enqueue(objTrans);
            go.SetActive(false);
        }
    }

    //posicion de la nave, opcion de animacion, tipo de ammo
    private Transform SpawnEnemySpaceShip01(Vector3 position, int posID, int typeAnimation)
    {
        Transform spawnedAmmo = enemySpaceShip02Queue.Dequeue();
        EnemySpaceShip pa = spawnedAmmo.GetComponent<EnemySpaceShip>();
        spawnedAmmo.gameObject.SetActive(true);
        spawnedAmmo.localPosition = position;

        pa.waitForNextLaserRound = enemyShip01_A_WaitForNextLaserRound;
        pa.laserRound = enemyShip01_A_laserRound;
        pa.laserRoundTimer = enemyShip01_A_laserRoundTimer;
        pa.laserSpeed = enemyShip01_A_laserSpeed;
        pa.laserValueDamage = enemyShip01_A_valueDamage;
        pa.laserLifeTime = enemyShip01_A_laserLifeTime;
        pa.typeAmmo = 1;
        pa.posInitialID = posID;
        pa.currentStamina = enemyShip01_valueStamina;
        pa.StartShip01_A();

        enemySpaceShip02Queue.Enqueue(spawnedAmmo);

        return spawnedAmmo;
    }

    public void AddEnemyShipDestroyed(int posID)
    {
        enemyShip01_A_enemyDestroyed.Add(posID);
    }

    //ship 01 movemnet A
    IEnumerator CorStartEnemySpawn_01_A()
    {
        enemyShip01_A_isActive = true;
        enemyShip01_A_enemyDestroyed = new List<int>();

        for (int i = 0; i < enemyShip01_A_positions.Length; i++)
        {
            SpawnEnemySpaceShip01(enemyShip01_A_positions[i],i, 1);
            yield return new WaitForSeconds(0.5f);
        }

        while (enemyShip01_A_isActive)
        {
            if (enemyShip01_A_enemyDestroyed.Count > 0)
            {

                for (int li = 0; li < enemyShip01_A_enemyDestroyed.Count; li++)
                {
                    SpawnEnemySpaceShip01(enemyShip01_A_positions[enemyShip01_A_enemyDestroyed[li]], enemyShip01_A_enemyDestroyed[li], 1);
                    enemyShip01_A_enemyDestroyed.RemoveAt(li);
                    yield return new WaitForSeconds(0.5f);
                }

               
               
            }
          

            yield return new WaitForSeconds(2f);



        }
    }



    //======================Pruebas






    //posicion de la nave, opcion de animacion, tipo de ammo
    private Transform SpawnEnemySpaceShip02(Vector3 position, int typeAnimation, int typeAmmo)
    {
        Transform spawnedAmmo = enemySpaceShip02Queue.Dequeue();
        EnemySpaceShip pa = spawnedAmmo.GetComponent<EnemySpaceShip>();
        //pa.SetEnemyValues(laserSpeed, laserTimer,laserWaveTimer, laserValueDamage);
    

        spawnedAmmo.gameObject.SetActive(true);
        spawnedAmmo.localPosition = position;
        pa.PlayEnemySpaceShipAnimation(typeAnimation, typeAmmo);

        enemySpaceShip02Queue.Enqueue(spawnedAmmo);

        return spawnedAmmo;
    }

    //posicion de la nave, opcion de animacion, tipo de ammo
    private Transform SpawnEnemySpaceShip02B(Vector3 position, int typeAnimation, int typeAmmo)
    {
        Transform spawnedAmmo = enemySpaceShip02Queue.Dequeue();
        EnemySpaceShip pa = spawnedAmmo.GetComponent<EnemySpaceShip>();
        //pa.SetEnemyValues(sphereSpeed, sphereValueDamage);


        spawnedAmmo.gameObject.SetActive(true);
        spawnedAmmo.localPosition = position;
        pa.PlayEnemySpaceShipAnimation(typeAnimation, typeAmmo);

        enemySpaceShip02Queue.Enqueue(spawnedAmmo);

        return spawnedAmmo;
    }




    IEnumerator CorStartEnemySpawnA()
    {

        while (true)
        {
            yield return new WaitForSeconds(enemySpaceShip02Timer);
            //SpawnEnemySpaceShip02(new Vector3(8f, 2f, 15f));
            int r1 = Random.Range(0, 1);
            int r2 = Random.Range(2, 3);
            SpawnEnemySpaceShip02(posSpaceShip02[r1],1,1);
            yield return new WaitForSeconds(spaceWaveTimerA);
            SpawnEnemySpaceShip02(posSpaceShip02[r2],1,1);
            yield return new WaitForSeconds(spaceWaveTimerA);
            SpawnEnemySpaceShip02(posSpaceShip02[4],1,1);

        }

    }

    IEnumerator CorStartEnemySpawnB()
    {

        yield return new WaitForSeconds(enemySpaceShip02Timer);
        while (true)
        {
           
            //SpawnEnemySpaceShip02(new Vector3(8f, 2f, 15f));
            int r1 = Random.Range(0, 1);
            int r2 = Random.Range(2, 3);
            SpawnEnemySpaceShip02B(posSpaceShip02_B[r1] + CharacterManager.characterManagerInstance.character.transform.localPosition,2,2);
            yield return new WaitForSeconds(spaceWaveTimerB);
            SpawnEnemySpaceShip02B(posSpaceShip02_B[r2] + CharacterManager.characterManagerInstance.character.transform.localPosition, 3,2);
            yield return new WaitForSeconds(spaceWaveTimerB);
            //yield return new WaitForSeconds(0.5f);
            //SpawnEnemySpaceShip02(posSpaceShip02_B[4],2,2);

        }

    }


}

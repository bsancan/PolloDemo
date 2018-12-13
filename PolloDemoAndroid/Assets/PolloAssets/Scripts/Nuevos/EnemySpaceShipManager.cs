using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpaceShipManager : MonoBehaviour
{
    public static EnemySpaceShipManager enemySpaceShipManagerInstance;


  
    //[SerializeField]
    //private float enemySpaceShip02Timer = 2f;

    [Header("EspaceShip 01")]
    [SerializeField]
    private GameObject enemySpaceShip01Prefab;
    //[SerializeField]
    //private bool enemyShip01;
    [SerializeField]
    private GameObject enemySpaceShip01_AB_Parent;
    [SerializeField]
    private Transform[] enemyShip01_AB_Spots;
    private Animator enemySpaceShip01_AB_Animator;
    private List<int> enemyShip01_enemyDestroyed;
    private Queue<Transform> enemySpaceShip01Queue = new Queue<Transform>();
    static int s_Ship01_A_Hash = Animator.StringToHash("Ship01_A");
    static int s_Ship01_B_Hash = Animator.StringToHash("Ship01_B");

    //========= Fase A ==================
    [SerializeField]
    private bool enemyShip01_A_isActive;                                             //Numero de round que apareceran las naves
    [SerializeField]
    private int enemyShip01_valueStamina = 60;
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
    private int enemyShip01_A_laserValueDamage = 10;                                   //daño que genera el laser
    //[SerializeField]
    //private Vector3[] enemyShip01_A_positions;                                    //posiciones de spawm para las naves
    //[SerializeField]
    //private int enemyShip01_RepMovB;

    //=================== Fase B ================
    private bool enemyShip01_B_isActive;

[Header("EspaceShip 02")]
    [SerializeField]
    private GameObject enemySpaceShip02Prefab;
    [SerializeField]
    private int enemySpaceShip02PoolSize = 10;
    //[SerializeField]
    //private bool enemyShip02;
    [SerializeField]
    private int enemyShip02_valueStamina = 60;
    [SerializeField]
    private bool enemyShip02_A_isActive;
    [SerializeField]
    private float enemyShip02_A_waveTimer = 1f;
    [SerializeField]
    private int enemyShip02_A_laserRound = 2;                                     //cuantos lasers puede disparar hasta la siguiente ronda
    [SerializeField]
    private float enemyShip02_A_laserRoundTimer = 0.5f;                         //tiempo entre laser
    //[SerializeField]
    //private float enemyShip02_A_WaitForNextLaserRound = 2f;                     //tiempo de espera para la siguiente ronda de lasers
    //[SerializeField]
    private float enemyShip02_A_laserLifeTime = 1f;                               //el timepo q tarda en desactivarse cuando no golpea al player
    [SerializeField]
    private float enemyShip02_A_laserSpeed = 50f;                                 //velocidad del laser
    [SerializeField]
    private int enemyShip02_A_laserValueDamage = 10;

    //private int enemyShip02_RepMovA;
    //[SerializeField]
    //private int enemyShip02_RepMovB;
    private bool enemyShip02_B_isActive;
    [SerializeField]
    private Vector3[] posSpaceShip02;
    private Queue<Transform> enemySpaceShip02Queue = new Queue<Transform>();


    [Header("EspaceShip 03")]
    [SerializeField]
    private GameObject enemySpaceShip03Prefab;
    [SerializeField]
    private int enemySpaceShip03PoolSize = 10;
    //[SerializeField]
    //private bool enemyShip03;
    [SerializeField]
    private bool enemyShip03_A_isActive;
    [SerializeField]
    private int enemyShip03_valueStamina = 60;
    [SerializeField]
    private float enemyShip03_A_sphereSpeed = 50f;                                 //velocidad del laser
    [SerializeField]
    private int enemyShip03_A_sphereDamage = 10;                                   //daño que genera el laser
    private Queue<Transform> enemySpaceShip03Queue = new Queue<Transform>();
    [SerializeField]
    private float enemyShip03_A_WaveTimer = 2f;

    [SerializeField]
    private Vector3[] posSpaceShip03_A;




    private Vector3 offset;



    //[SerializeField]
    //private int enemyShip03_RepMovA;
    //[SerializeField]
    //private int enemyShip03_RepMovB;



    //[SerializeField]
    //private float laserTimer = 0.75f;
    //[SerializeField]
    //private float laserWaveTimer = 0.5f;
    //[SerializeField]
    //private float laserSpeed = -80f;
    //[SerializeField]
    //private int laserValueDamage = 5;

    //[SerializeField]
    //private float sphereSpeed = -80f;
    //[SerializeField]
    //private float sphereSpeedRotation = 1f;
    //[SerializeField]
    //private int sphereValueDamage = 10;



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
        CreateEnemySpaceShip01();
        //CreateEnemySpaceShip02();

        enemySpaceShip01_AB_Animator = enemySpaceShip01_AB_Parent.GetComponent<Animator>();

        if (GameManager.gameManagerInstance.currentLevel == 2)
        {
            //enemyShip01_DicA = new Dictionary<int, Vector3>();
            //for (int i = 0; i < enemyShip01_A_positions.Length; i++)
            //{
            //    enemyShip01_DicA.Add(i, enemyShip01_A_positions[i]);
            //}
            AmmoManager.ammoManagerInstance.CreateEnemyAmmo();
            

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

    public void AddEnemyShipDestroyed(int posID)
    {
        enemyShip01_enemyDestroyed.Add(posID);
    }

    public void StartEnemySpawn01_B()
    {
        StartCoroutine(CorStartEnemySpawn_01_B());
    }

    public void StartEnemySpawn02_A()
    {
        StartCoroutine(CorStartEnemySpawn_02_A());
    }

    public void StartEnemySpawn03_A()
    {
        StartCoroutine(CorStartEnemySpawn_03());
    }

    private void CreateEnemySpaceShip03()
    {

        for (int i = 0; i < enemySpaceShip03PoolSize; i++)
        {
            GameObject go = Instantiate(enemySpaceShip03Prefab, Vector3.zero, Quaternion.identity) as GameObject;
            Transform objTrans = go.transform;
            objTrans.parent = transform;
            objTrans.gameObject.name = objTrans.gameObject.name + "_" + i;
            enemySpaceShip03Queue.Enqueue(objTrans);
            go.SetActive(false);
        }
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


    private void CreateEnemySpaceShip01()
    {

        for (int i = 0; i < enemyShip01_AB_Spots.Length; i++)
        {
            GameObject go = Instantiate(enemySpaceShip01Prefab, Vector3.zero, Quaternion.identity) as GameObject;
            Transform objTrans = go.transform;
            objTrans.parent = transform;
            objTrans.gameObject.name = objTrans.gameObject.name + "_" + i;
            enemySpaceShip01Queue.Enqueue(objTrans);
            go.SetActive(false);
        }
    }

    //posicion de la nave, opcion de animacion, tipo de ammo
    private Transform SpawnEnemySpaceShip01(Transform child, int posID, int typeAnimation)
    {
        Transform ship01 = enemySpaceShip01Queue.Dequeue();
        EnemySpaceShip pa = ship01.GetComponent<EnemySpaceShip>();
        ship01.parent = child.transform;
        ship01.gameObject.SetActive(true);
        ship01.localPosition = Vector3.zero;

        pa.waitForNextLaserRound = enemyShip01_A_WaitForNextLaserRound;
        pa.laserRound = enemyShip01_A_laserRound;
        pa.laserRoundTimer = enemyShip01_A_laserRoundTimer;
        pa.ammoSpeed = enemyShip01_A_laserSpeed;
        pa.ammoDamage = enemyShip01_A_laserValueDamage;
        pa.laserLifeTime = enemyShip01_A_laserLifeTime;
        pa.typeAmmo = 1;
        pa.posInitialID = posID;
        pa.currentStamina = enemyShip01_valueStamina;
        pa.StartShip01();

        enemySpaceShip01Queue.Enqueue(ship01);

        return ship01;
    }

    //posicion de la nave, opcion de animacion, tipo de ammo
    private Transform SpawnEnemySpaceShip02(Vector3 position, int typeAmmo)
    {
        Transform ship02 = enemySpaceShip02Queue.Dequeue();
        EnemySpaceShip pa = ship02.GetComponent<EnemySpaceShip>();
        //pa.SetEnemyValues(laserSpeed, laserTimer,laserWaveTimer, laserValueDamage);

        ship02.gameObject.SetActive(true);
        ship02.localPosition = position;
        //pa.waitForNextLaserRound = enemyShip02_A_WaitForNextLaserRound;
        pa.laserRound = enemyShip02_A_laserRound;
        pa.laserRoundTimer = enemyShip02_A_laserRoundTimer;
        pa.ammoSpeed = enemyShip02_A_laserSpeed;
        pa.ammoDamage = enemyShip02_A_laserValueDamage;
        pa.laserLifeTime = enemyShip02_A_laserLifeTime;
        pa.typeAmmo = typeAmmo;
        pa.currentStamina = enemyShip02_valueStamina;
        pa.StartShip02();
        enemySpaceShip02Queue.Enqueue(ship02);

        return ship02;
    }

    //posicion de la nave, opcion de animacion, tipo de ammo
    private Transform SpawnEnemySpaceShip03(Vector3 position, string typeAnimation)
    {
        Transform spawnedAmmo = enemySpaceShip03Queue.Dequeue();
        EnemySpaceShip pa = spawnedAmmo.GetComponent<EnemySpaceShip>();
        spawnedAmmo.gameObject.SetActive(true);
        spawnedAmmo.localPosition = position;
        spawnedAmmo.localRotation = Quaternion.identity;
        pa.ammoSpeed = enemyShip03_A_sphereSpeed;
        pa.ammoDamage = enemyShip03_A_sphereDamage;
        pa.currentStamina = enemyShip03_valueStamina;
        pa.StartShip03(typeAnimation);

        enemySpaceShip03Queue.Enqueue(spawnedAmmo);

        return spawnedAmmo;
    }

    IEnumerator CorStartEnemySpawn_01_A()
    {
        enemyShip01_A_isActive = true;
        enemyShip01_enemyDestroyed = new List<int>();
        

        for (int i = 0; i < enemyShip01_AB_Spots.Length; i++)
        {
            SpawnEnemySpaceShip01(enemyShip01_AB_Spots[i], i, 1);
            yield return new WaitForSeconds(0.5f);
        }

        enemySpaceShip01_AB_Animator.SetTrigger(s_Ship01_A_Hash);

        //for (int i = 0; i < enemyShip01_A_positions.Length; i++)
        //{
        //    SpawnEnemySpaceShip01(enemyShip01_A_positions[i],i, 1);
        //    yield return new WaitForSeconds(0.5f);
        //}

        while (enemyShip01_A_isActive)
        {
            if (enemyShip01_enemyDestroyed.Count > 0)
            {

                for (int li = 0; li < enemyShip01_enemyDestroyed.Count; li++)
                {
                    SpawnEnemySpaceShip01(enemyShip01_AB_Spots[enemyShip01_enemyDestroyed[li]], enemyShip01_enemyDestroyed[li], 1);
                    enemyShip01_enemyDestroyed.RemoveAt(li);
                    yield return new WaitForSeconds(0.5f);
                }
            }
         
            yield return new WaitForSeconds(2f);
        }

        for (int i = 0; i < enemyShip01_AB_Spots.Length; i++)
        {
            enemyShip01_AB_Spots[i].GetChild(0).gameObject.SetActive(false);
            //SpawnEnemySpaceShip01(enemyShip01_AB_Spots[i], i, 1);
            //yield return new WaitForSeconds(0.5f);
        }

        enemySpaceShip01_AB_Parent.gameObject.SetActive(false);

    }

    IEnumerator CorStartEnemySpawn_01_B()
    {
        enemyShip01_A_isActive = false;
        enemyShip01_B_isActive = true;
     
        yield return new WaitForSeconds(3f);
        enemyShip01_enemyDestroyed = new List<int>();
        enemySpaceShip01_AB_Parent.gameObject.SetActive(true);

        for (int i = 0; i < enemyShip01_AB_Spots.Length; i++)
        {
            SpawnEnemySpaceShip01(enemyShip01_AB_Spots[i], i, 1);
            yield return new WaitForSeconds(0.5f);
        }

        enemySpaceShip01_AB_Animator.SetTrigger(s_Ship01_B_Hash);
        //for (int i = 0; i < enemyShip01_A_positions.Length; i++)
        //{
        //    SpawnEnemySpaceShip01(enemyShip01_A_positions[i],i, 1);
        //    yield return new WaitForSeconds(0.5f);
        //}

        while (enemyShip01_B_isActive)
        {
            if (enemyShip01_enemyDestroyed.Count > 0)
            {

                for (int li = 0; li < enemyShip01_enemyDestroyed.Count; li++)
                {
                    SpawnEnemySpaceShip01(enemyShip01_AB_Spots[enemyShip01_enemyDestroyed[li]], enemyShip01_enemyDestroyed[li], 1);
                    enemyShip01_enemyDestroyed.RemoveAt(li);
                    yield return new WaitForSeconds(0.5f);
                }
            }

            yield return new WaitForSeconds(2f);
        }

        for (int i = 0; i < enemyShip01_AB_Spots.Length; i++)
        {
            Destroy(enemyShip01_AB_Spots[i].GetChild(0).gameObject);
            //SpawnEnemySpaceShip01(enemyShip01_AB_Spots[i], i, 1);
            //yield return new WaitForSeconds(0.5f);
        }

        enemyShip01_AB_Spots = null;
        Destroy(enemySpaceShip01_AB_Parent.gameObject);
        enemySpaceShip01_AB_Parent = null; 

    }

    IEnumerator CorStartEnemySpawn_02_A()
    {
        enemyShip01_enemyDestroyed = null;
        enemyShip01_B_isActive = false;
        enemyShip02_A_isActive = true;
        CreateEnemySpaceShip02();
        yield return new WaitForSeconds(3f);


        while (enemyShip02_A_isActive)
        {
            //yield return new WaitForSeconds(enemySpaceShip02Timer);
            //SpawnEnemySpaceShip02(new Vector3(8f, 2f, 15f));
            int r1 = Random.Range(0, 1);
            int r2 = Random.Range(2, 3);
            SpawnEnemySpaceShip02(posSpaceShip02[r1], 1);
            yield return new WaitForSeconds(enemyShip02_A_waveTimer);
            SpawnEnemySpaceShip02(posSpaceShip02[r2], 1);
            yield return new WaitForSeconds(enemyShip02_A_waveTimer);
            SpawnEnemySpaceShip02(posSpaceShip02[4], 1);
            yield return new WaitForSeconds(enemyShip02_A_waveTimer);
        }

        //destruyo todas las naves del nivel 02
        for (int i = 0; i < enemySpaceShip02Queue.Count; i++)
        {
            Transform spawnedAmmo = enemySpaceShip02Queue.Dequeue();
            Destroy(spawnedAmmo.gameObject);

            //SpawnEnemySpaceShip01(enemyShip01_AB_Spots[i], i, 1);
            //yield return new WaitForSeconds(0.5f);
        }
        enemySpaceShip02Queue = null;

    }

    IEnumerator CorStartEnemySpawn_03()
    {
        enemyShip02_A_isActive = false;
        enemyShip03_A_isActive = true;
        AmmoManager.ammoManagerInstance.CreateEnemyAmmoSphere();
        CreateEnemySpaceShip03();
        yield return new WaitForSeconds(3f);

        //destruyo los laser usados en las naves 01-02
        for (int i = 0; i < AmmoManager.ammoManagerInstance.enemyAmmoQueue.Count; i++)
        {
            Transform spawnedAmmo = AmmoManager.ammoManagerInstance.enemyAmmoQueue.Dequeue();
            Destroy(spawnedAmmo.gameObject);
        }
        AmmoManager.ammoManagerInstance.enemyAmmoQueue = null;


        while (enemyShip03_A_isActive)
        {
            //SpawnEnemySpaceShip02(new Vector3(8f, 2f, 15f));
            int r1 = Random.Range(0, 1);
            int r2 = Random.Range(2, 3);
            SpawnEnemySpaceShip03(posSpaceShip03_A[r1] + CharacterManager.characterManagerInstance.character.transform.localPosition,"A");
            yield return new WaitForSeconds(enemyShip03_A_WaveTimer);
            SpawnEnemySpaceShip03(posSpaceShip03_A[r2] + CharacterManager.characterManagerInstance.character.transform.localPosition,"B");
            yield return new WaitForSeconds(enemyShip03_A_WaveTimer);
            //yield return new WaitForSeconds(0.5f);
            //SpawnEnemySpaceShip02(posSpaceShip02_B[4],2,2);
        }
    }


    //======================Pruebas

  



    //IEnumerator CorStartEnemySpawnA()
    //{

    //    while (true)
    //    {
    //        //yield return new WaitForSeconds(enemySpaceShip02Timer);
    //        //SpawnEnemySpaceShip02(new Vector3(8f, 2f, 15f));
    //        int r1 = Random.Range(0, 1);
    //        int r2 = Random.Range(2, 3);
    //        SpawnEnemySpaceShip02(posSpaceShip02[r1],1,1);
    //        yield return new WaitForSeconds(spaceWaveTimerA);
    //        SpawnEnemySpaceShip02(posSpaceShip02[r2],1,1);
    //        yield return new WaitForSeconds(spaceWaveTimerA);
    //        SpawnEnemySpaceShip02(posSpaceShip02[4],1,1);

    //    }

    //}

    //IEnumerator CorStartEnemySpawnB()
    //{

    //    //yield return new WaitForSeconds(enemySpaceShip02Timer);
    //    while (true)
    //    {
           
    //        //SpawnEnemySpaceShip02(new Vector3(8f, 2f, 15f));
    //        int r1 = Random.Range(0, 1);
    //        int r2 = Random.Range(2, 3);
    //        SpawnEnemySpaceShip02B(posSpaceShip02_B[r1] + CharacterManager.characterManagerInstance.character.transform.localPosition,2,2);
    //        yield return new WaitForSeconds(spaceWaveTimerB);
    //        SpawnEnemySpaceShip02B(posSpaceShip02_B[r2] + CharacterManager.characterManagerInstance.character.transform.localPosition, 3,2);
    //        yield return new WaitForSeconds(spaceWaveTimerB);
    //        //yield return new WaitForSeconds(0.5f);
    //        //SpawnEnemySpaceShip02(posSpaceShip02_B[4],2,2);

    //    }

    //}


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoManager : MonoBehaviour {
    public static AmmoManager ammoManagerInstance;

    public GameObject playerAmmoPrefab = null;
    public GameObject enemyAmmoPrefab = null;
    public GameObject enemyAmmoSpherePrefab = null;

    public float playerAmmoSpeed = 60f;
    public float playerAmmoLifeTime = 1f;

    //public float enemyAmmoSpeed = 4f;
    //public float enemyAmmoLifeTime = 1f;

    public int playerAmmoPoolSize = 20;
    public int enemyAmmoPoolSize = 20;
    public int enemyAmmoSpherePoolSize = 20;
    //private GameObject[] playerAmmoArray;
    //private GameObject[] enemyAmmoArray;

    public Queue<Transform> playerAmmoQueue = new Queue<Transform>();
    public Queue<Transform> enemyAmmoQueue = new Queue<Transform>();
    public Queue<Transform> enemyAmmoSphereQueue = new Queue<Transform>();

    void Start () {

        if (ammoManagerInstance == null)
        {
            ammoManagerInstance = this;
        }
        else
        {

            Destroy(gameObject);
        }

        //Sets this to not be destroyed when reloading scene
        //DontDestroyOnLoad(gameObject);

        CreateAmmo();


    }

    void CreateAmmo()
    {
        //player = new GameObject[playerAmmoPoolSize];

        for (int i = 0; i < playerAmmoPoolSize; i++)
        {
            GameObject go = Instantiate(playerAmmoPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            Transform objTrans = go.transform;
            objTrans.parent = transform;
            objTrans.gameObject.name = objTrans.gameObject.name + "_" + i;
            playerAmmoQueue.Enqueue(objTrans);
            go.SetActive(false);
        }

        //for (int i = 0; i < enemyAmmoPoolSize; i++)
        //{
        //    GameObject go = Instantiate(enemyAmmoPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        //    Transform objTrans = go.transform;
        //    objTrans.parent = transform;
        //    objTrans.gameObject.name = objTrans.gameObject.name + "_" + i;
        //    enemyAmmoQueue.Enqueue(objTrans);
        //    go.SetActive(false);
        //}
    }

    public void CreateEnemyAmmo()
    {

        for (int i = 0; i < enemyAmmoPoolSize; i++)
        {
            GameObject go = Instantiate(enemyAmmoPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            Transform objTrans = go.transform;
            objTrans.parent = transform;
            objTrans.gameObject.name = objTrans.gameObject.name + "_" + i;
            enemyAmmoQueue.Enqueue(objTrans);
            go.SetActive(false);
        }
    }

    public void CreateEnemyAmmoSphere()
    {

        for (int i = 0; i < enemyAmmoSpherePoolSize; i++)
        {
            GameObject go = Instantiate(enemyAmmoSpherePrefab, Vector3.zero, Quaternion.identity) as GameObject;
            Transform objTrans = go.transform;
            objTrans.parent = transform;
            objTrans.gameObject.name = objTrans.gameObject.name + "_" + i;
            enemyAmmoSphereQueue.Enqueue(objTrans);
            go.SetActive(false);
        }
    }

    public Transform SpawnAmmo(Transform playerAmmo)
    {
        Transform spawnedAmmo = playerAmmoQueue.Dequeue();
        PlayerAmmo pa = spawnedAmmo.GetComponent<PlayerAmmo>();
        pa.speed = playerAmmoSpeed;
        pa.lifeTime = playerAmmoLifeTime;
        //spawnedAmmo.SetParent(playerAmmo);

        spawnedAmmo.gameObject.SetActive(true);
        spawnedAmmo.position = playerAmmo.position + (playerAmmo.forward * 2f);
        spawnedAmmo.rotation = playerAmmo.rotation;
        //spawnedAmmo.position = playerAmmo.forward * 1.5f;

        //

        playerAmmoQueue.Enqueue(spawnedAmmo);

        return spawnedAmmo;
    }

    public Transform SpawnEnemyAmmo(Transform enemyAmmoSpot)
    {
        Transform spawnedAmmo = ammoManagerInstance.enemyAmmoQueue.Dequeue();
        EnemyAmmo pa = spawnedAmmo.GetComponent<EnemyAmmo>();
        spawnedAmmo.position = enemyAmmoSpot.position;
        spawnedAmmo.rotation = enemyAmmoSpot.rotation;
        //pa.speed = enemyAmmoSpeed;
        //pa.lifeTime = playerAmmoLifeTime;
        spawnedAmmo.gameObject.SetActive(true);
        //pa.AmmoActived();

        ammoManagerInstance.enemyAmmoQueue.Enqueue(spawnedAmmo);

        return spawnedAmmo;
    }

    public Transform SpawnEnemyAmmoSphere(Transform enemyAmmoSpot)
    {
        Transform spawnedAmmo = ammoManagerInstance.enemyAmmoSphereQueue.Dequeue();
        EnemyAmmo pa = spawnedAmmo.GetComponent<EnemyAmmo>();
        spawnedAmmo.position = enemyAmmoSpot.position;
        spawnedAmmo.rotation = enemyAmmoSpot.rotation;
        //pa.speed = enemyAmmoSpeed;
        //pa.lifeTime = playerAmmoLifeTime;
        spawnedAmmo.gameObject.SetActive(true);
        //pa.AmmoActived();

        ammoManagerInstance.enemyAmmoSphereQueue.Enqueue(spawnedAmmo);

        return spawnedAmmo;
    }

}

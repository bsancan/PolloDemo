using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoManager : MonoBehaviour {
    public static AmmoManager ammoManagerInstance;

    public GameObject playerAmmoPrefab = null;
    public GameObject enemyAmmoPrefab = null;

    public float playerAmmoSpeed = 60f;
    public float playerAmmoLifeTime = 1f;

    public float enemyAmmoSpeed = 4f;

    public int playerAmmoPoolSize = 20;
    public int enemyAmmoPoolSize = 20;

    //private GameObject[] playerAmmoArray;
    //private GameObject[] enemyAmmoArray;

    public Queue<Transform> playerAmmoQueue = new Queue<Transform>();
    public Queue<Transform> enemyAmmoQueue = new Queue<Transform>();

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
        DontDestroyOnLoad(gameObject);

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

    public Transform SpawnAmmo(Transform playerAmmo)
    {
        Transform spawnedAmmo = playerAmmoQueue.Dequeue();
        PlayerAmmo pa = spawnedAmmo.GetComponent<PlayerAmmo>();
        pa.speed = playerAmmoSpeed;
        pa.lifeTime = playerAmmoLifeTime;
        spawnedAmmo.SetParent(playerAmmo);

        spawnedAmmo.gameObject.SetActive(true);
        spawnedAmmo.localPosition = Vector3.zero;

        //spawnedAmmo.rotation = rotation;

        playerAmmoQueue.Enqueue(spawnedAmmo);

        return spawnedAmmo;
    }

    //public static Transform SpawnEnemyAmmo(Vector3 position, Quaternion rotation)
    //{
    //    Transform spawnedAmmo = ammoManagerInstance.enemyAmmoQueue.Dequeue();

    //    spawnedAmmo.gameObject.SetActive(true);
    //    spawnedAmmo.position = position;
    //    spawnedAmmo.rotation = rotation;

    //    ammoManagerInstance.enemyAmmoQueue.Enqueue(spawnedAmmo);

    //    return spawnedAmmo;
    //}

}

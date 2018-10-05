using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionManager : MonoBehaviour
{
    public static ExplosionManager explosionManagerInstance;


    public GameObject explosionPrefab = null;
    public Queue<Transform> explosionQueue = new Queue<Transform>();
  
    [SerializeField]
    private int explosionPoolSize = 5;

    public GameObject playerExplosionPrefab = null;
    public Queue<Transform> playerExplosionQueue = new Queue<Transform>();
    [SerializeField]
    private int playerExplosionPoolSize = 2;

    void Start()
    {
        if(explosionManagerInstance == null)
        {
            explosionManagerInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        CreateExplosions();
    }


    private void CreateExplosions()
    {
        for (int i = 0; i < explosionPoolSize; i++)
        {
            GameObject go = Instantiate(explosionPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            Transform objTrans = go.transform;
            objTrans.parent = transform;
            objTrans.gameObject.name = objTrans.gameObject.name + "_" + i;
            explosionQueue.Enqueue(objTrans);
            go.SetActive(false);
        }

        for (int i = 0; i < playerExplosionPoolSize; i++)
        {
            GameObject go = Instantiate(playerExplosionPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            Transform objTrans = go.transform;
            objTrans.parent = transform;
            objTrans.gameObject.name = objTrans.gameObject.name + "_" + i;
            playerExplosionQueue.Enqueue(objTrans);
            go.SetActive(false);
        }
    }

    public void SpawnExplosion(Vector3 pos)
    {
        Transform spawnedExplosion = explosionQueue.Dequeue();

        spawnedExplosion.gameObject.SetActive(true);
        spawnedExplosion.position = pos;

        explosionQueue.Enqueue(spawnedExplosion);
    }

    public void SpawnPlayerExplosion(Vector3 pos)
    {
        Transform spawnedExplosion = playerExplosionQueue.Dequeue();

        spawnedExplosion.gameObject.SetActive(true);
        spawnedExplosion.position = pos;

        playerExplosionQueue.Enqueue(spawnedExplosion);
    }
}

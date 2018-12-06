using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpaceShipManager : MonoBehaviour
{
    
    [SerializeField]
    private GameObject enemySpaceShip02Prefab;
    [SerializeField]
    private int enemySpaceShip02PoolSize = 10;
    [SerializeField]
    private float enemySpaceShip02Timer = 2f;
    [SerializeField]
    private float laserTimer = 1f;
    [SerializeField]
    private float laserSpeed = -80f;

    private Vector3 offset;

    public Queue<Transform> enemySpaceShip02Queue = new Queue<Transform>();

    void Start()
    {
        offset = transform.position;
        CreateEnemySpaceShip02();
        StartCoroutine(CorStartEnemySpawn());

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = offset + CharacterManager.characterManagerInstance.transform.position;
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

    private Transform SpawnEnemySpaceShip02(Vector3 position)
    {
        Transform spawnedAmmo = enemySpaceShip02Queue.Dequeue();
        EnemySpaceShip pa = spawnedAmmo.GetComponent<EnemySpaceShip>();
        pa.SetEnemyValues(laserSpeed, laserTimer);
    

        spawnedAmmo.gameObject.SetActive(true);
        spawnedAmmo.position = position;
        pa.PlayEnemySpaceShipAnimation();
        //spawnedAmmo.rotation = rotation;

        enemySpaceShip02Queue.Enqueue(spawnedAmmo);

        return spawnedAmmo;
    }

    IEnumerator CorStartEnemySpawn()
    {
        yield return new WaitForSeconds(1f);

        while (true)
        {
            yield return new WaitForSeconds(enemySpaceShip02Timer);
            SpawnEnemySpaceShip02(new Vector3(8f, 2f, 15f));
 
        }

    }
}

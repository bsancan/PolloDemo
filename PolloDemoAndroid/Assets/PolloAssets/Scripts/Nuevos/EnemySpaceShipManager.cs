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
    private float laserTimer = 0.75f;
    [SerializeField]
    private float laserWaveTimer = 0.5f;
    [SerializeField]
    private float laserSpeed = -80f;
    [SerializeField]
    private int laserValueDamage = 5;
    [SerializeField]
    private float spaceWaveTimerA = 1f;
    [SerializeField]
    private float sphereSpeed = -80f;
    [SerializeField]
    private float sphereSpeedRotation = 1f;
    [SerializeField]
    private float spaceWaveTimerB = 1f;
    [SerializeField]
    private Vector3[] posSpaceShip02;
    [SerializeField]
    private Vector3[] posSpaceShip02_B;

    private Vector3 offset;

    public Queue<Transform> enemySpaceShip02Queue = new Queue<Transform>();

    void Start()
    {
        offset = transform.position;
        CreateEnemySpaceShip02();
        //StartCoroutine(CorStartEnemySpawnA());
        StartCoroutine(CorStartEnemySpawnB());

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
    private Transform SpawnEnemySpaceShip02(Vector3 position, int typeAnimation, int typeAmmo)
    {
        Transform spawnedAmmo = enemySpaceShip02Queue.Dequeue();
        EnemySpaceShip pa = spawnedAmmo.GetComponent<EnemySpaceShip>();
        pa.SetEnemyValues(laserSpeed, laserTimer,laserWaveTimer, laserValueDamage, sphereSpeedRotation);
    

        spawnedAmmo.gameObject.SetActive(true);
        spawnedAmmo.localPosition = position;
        pa.PlayEnemySpaceShipAnimation(typeAnimation, typeAmmo);
        //spawnedAmmo.rotation = rotation;

        enemySpaceShip02Queue.Enqueue(spawnedAmmo);

        return spawnedAmmo;
    }

    //posicion de la nave, opcion de animacion, tipo de ammo
    private Transform SpawnEnemySpaceShip02B(Vector3 position, int typeAnimation, int typeAmmo)
    {
        Transform spawnedAmmo = enemySpaceShip02Queue.Dequeue();
        EnemySpaceShip pa = spawnedAmmo.GetComponent<EnemySpaceShip>();
        pa.SetEnemyValues(sphereSpeed, laserTimer, laserWaveTimer, laserValueDamage, sphereSpeedRotation);


        spawnedAmmo.gameObject.SetActive(true);
        spawnedAmmo.localPosition = position;
        pa.PlayEnemySpaceShipAnimation(typeAnimation, typeAmmo);
        //spawnedAmmo.rotation = rotation;

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
            SpawnEnemySpaceShip02B(posSpaceShip02_B[r1],2,2);
            yield return new WaitForSeconds(spaceWaveTimerB);
            SpawnEnemySpaceShip02B(posSpaceShip02_B[r2],3,2);
            yield return new WaitForSeconds(spaceWaveTimerB);
            //yield return new WaitForSeconds(0.5f);
            //SpawnEnemySpaceShip02(posSpaceShip02_B[4],2,2);

        }

    }


}

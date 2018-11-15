using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SpawnWave : MonoBehaviour
{
    public int level = 0;

    Transform player;
    //	public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazartCount;

    //manejo de laser01 player
    public bool isLoopLaser01 = true;
    public GameObject laser01;
    public Transform Laser01Left;
    public Transform Laser01Right;
    public int AmountLaser01;
    public float speedLaser01;
    public float waitLaser01;
    public List<GameObject> pooledLaser01;

    //manejo de laser enemy
    public bool isLoopSphereLaser = true;
    public GameObject EnemyLaser;
    public GameObject EnemySphere;
    public GameObject EnemyMineSphere;
    public int AmountEnemyLaser;
    public int AmountEnemySphere;
    public int AmountEnemyMineSphere;
    public float speedLaser;
    public float waitLaser;
    public List<GameObject> pooledEnemyLaser;
    public List<GameObject> pooledEnemySphere;
    public List<GameObject> pooledEnemyMineSphere;

    //meteros
    public GameObject meteor;
    public int pooledAmountM;
    public float speedMeteor;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public List<GameObject> pooledMeteors;


    //meteoros B - indestructibles
    public GameObject meteorB;
    public int pooledAmountMB;
    public float speedMeteorB;
    public List<GameObject> pooledMeteorsB;

    //Explosiones de asteroides;
    public GameObject explotionAsteroid;
    public int pooledAmountExpAst;
    public float lifeTime;
    public List<GameObject> pooledExplotionAsteroid;


    //Misil A - 
    public GameObject misilA;
    public int pooledAmountMiA;
    public float speedMisilA = -40f;
    public float spawnWaitMlA = 0.04f;
    public float startWaitMlA = 0.1f;
    public float waveWaitMlA = 0.1f;
    public List<GameObject> pooledMisilA;

    //manejo de naves
    public GameObject shipA;
    public Vector3 spawnValuesSpA;
    public int NavesRepeticion;
    public int pooledAmountSpA;
    public float speedShipA;
    public float spawnWaitSpA;
    public float startWaitSpA;
    public float waveWaitSpA;
    public List<GameObject> pooledShipsA;

    //multiplo para que los meteoros spawneen al player
    public int followMultiplePlayer;

    //items
    public GameObject[] Items;
    bool isItemOk = false;
    bool goItemH = true;

    //time
    public Text txtTime;
    public float timeLeft = 60f;
    int minutes;
    int seconds;

    //titulo de niveles
    public Text txtTitleLevel;

    //activar o terminar WAVE
    bool islvl101, islvl102, islvl103, islvl104, islvl105, islvl106, islvl107, islvl108,
         islvl02, islvl03;

    // espera unos seg para volver a usae el metoro
    //bool isMeteorBig = true;

    //guarda el tiempo temporalmente
    float startTime;

    //lugare para spwan naves
    Vector2[] spawnPlace = new Vector2[9];
    Vector2[] spawnPlaceUsed = new Vector2[9];
    void Start()
    {
        player = GetComponent<GameController>().mainPlayer.transform;

        islvl101 = false;
        islvl102 = false;
        islvl103 = false;
        islvl104 = false;
        islvl105 = false;
        islvl106 = false;
        islvl107 = false;
        islvl108 = false;

        islvl02 = false;
        islvl03 = false;

        //3 naves a usar
        pooledShipsA = new List<GameObject>();
        //meteoros A - nivel 1 - fase 1
        pooledMeteors = new List<GameObject>();
        //misiles A - Nivel 1 - fase 2
        pooledMisilA = new List<GameObject>();
        //laser01 (player)
        pooledLaser01 = new List<GameObject>();
        //explosiones (naves,laser,player,misiles)
        pooledExplotionAsteroid = new List<GameObject>();
        //naves para nivel1- fase 2 - naves cruzadas

        //enemy laser

        //enemy sphere
        pooledEnemySphere = new List<GameObject>();
        pooledEnemyMineSphere = new List<GameObject>();
        level = 0;

        StartCoroutine(SetPools());
        startTime = timeLeft;

        //lugares para spawn
        spawnPlace[0] = new Vector2(0, 0);
        spawnPlace[1] = new Vector2(-2, 0);
        spawnPlace[2] = new Vector2(2, 0);
        spawnPlace[3] = new Vector2(0, 2);
        spawnPlace[4] = new Vector2(-2, 2);
        spawnPlace[5] = new Vector2(2, 2);
        spawnPlace[6] = new Vector2(0, -2);
        spawnPlace[7] = new Vector2(-2, -2);
        spawnPlace[8] = new Vector2(2, -2);

        txtTitleLevel.text = "";
        //INICIA LASER01
        //StartLaser01();

        //Nivel02 de prueba directo
        //        StartCoroutine(SpawnMisilA1());
        //        StartCoroutine(SpawnMisilA2());

        //            if (level == 1)
        //            StartCoroutine(SpawnHazardsXY());
        //        else if (level == 2)
        //            StartCoroutine(SpawnShips(NavesRepeticion));
        //        else if (level == 3)
        //            level = 3;
    }

    // Update is called once per frame
    void Update()
    {

        if (level == 101)
        {



            //			if (minutes != 0 && seconds != 0) {
            //				txtTime.text = minutes.ToString ("00") + ":" + seconds.ToString ("00");
            //
            //			} else {
            //		
            //				GameObject.Find ("Cnv_Level").GetComponent<HealthControl> ().TimeOver ();
            //			}

            txtTime.text = minutes.ToString("00") + ":" + seconds.ToString("00");

            timeLeft -= Time.deltaTime;
            minutes = (int)(timeLeft / 60f);
            seconds = (int)(timeLeft % 60f);

            if (((seconds % 4) == 0) && goItemH)
            {
                //ItemHealing();
            }
            if (minutes == 0 && seconds == 0)
            {
                //islvl11 = false;
                //level = 12;
                //REGRESAR A SLECCION

            }



        }
        else if (level == 102)
        {

            txtTime.text = minutes.ToString("00") + ":" + seconds.ToString("00");

            timeLeft -= Time.deltaTime;
            minutes = (int)(timeLeft / 60f);
            seconds = (int)(timeLeft % 60f);
            if (minutes == 0 && seconds == 0)
            {
                //islvl12 = false;
                //                level = 13;
                //REGRESAR A NIVEL
            }
        }
        else if (level == 103)
        {

            txtTime.text = minutes.ToString("00") + ":" + seconds.ToString("00");

            timeLeft -= Time.deltaTime;
            minutes = (int)(timeLeft / 60f);
            seconds = (int)(timeLeft % 60f);
            if (minutes == 0 && seconds == 0)
            {
            }
        }
        else if (level == 104)
        {
            txtTime.text = minutes.ToString("00") + ":" + seconds.ToString("00");
            timeLeft -= Time.deltaTime;
            minutes = (int)(timeLeft / 60f);
            seconds = (int)(timeLeft % 60f);

            if (minutes == 0 && seconds == 0)
            {
            }
        }
        else if (level == 105)
        {
            txtTime.text = minutes.ToString("00") + ":" + seconds.ToString("00");
            timeLeft -= Time.deltaTime;
            minutes = (int)(timeLeft / 60f);
            seconds = (int)(timeLeft % 60f);

            if (minutes == 0 && seconds == 0)
            {
            }
        }
        else if (level == 106)
        {
            txtTime.text = minutes.ToString("00") + ":" + seconds.ToString("00");
            timeLeft -= Time.deltaTime;
            minutes = (int)(timeLeft / 60f);
            seconds = (int)(timeLeft % 60f);

            if (minutes == 0 && seconds == 0)
            {
            }
        }
        else if (level == 107)
        {
            txtTime.text = minutes.ToString("00") + ":" + seconds.ToString("00");
            timeLeft -= Time.deltaTime;
            minutes = (int)(timeLeft / 60f);
            seconds = (int)(timeLeft % 60f);

            if (minutes == 0 && seconds == 0)
            {
            }
        }
        else if (level == 108)
        {
            txtTime.text = minutes.ToString("00") + ":" + seconds.ToString("00");
            timeLeft -= Time.deltaTime;
            minutes = (int)(timeLeft / 60f);
            seconds = (int)(timeLeft % 60f);

            if (minutes == 0 && seconds == 0)
            {
            }
        }
        //else if (level == 2)
        //{
        //    if (!islvl02)
        //    {
        //        txtTime.text = "";
        //        StartCoroutine(SpawnShips(NavesRepeticion));

        //    }

        //}
        //else if (level == 3)
        //{
        //    txtTime.text = "";

        //}


    }

    GameObject GetPooledExpAst()
    {
        for (int i = 0; i < pooledExplotionAsteroid.Count; i++)
        {
            if (!pooledExplotionAsteroid[i].activeInHierarchy)
                return pooledExplotionAsteroid[i];
        }

        GameObject obj = (GameObject)Instantiate(explotionAsteroid);
        obj.SetActive(false);
        pooledExplotionAsteroid.Add(obj);
        return obj;
    }


    GameObject GetPooledLaser01()
    {
        for (int i = 0; i < pooledLaser01.Count; i++)
        {
            if (!pooledLaser01[i].activeInHierarchy)
                return pooledLaser01[i];
        }

        GameObject obj = (GameObject)Instantiate(laser01);
        obj.SetActive(false);
        pooledLaser01.Add(obj);
        return obj;
    }

    public GameObject GetPooledEnemySphere()
    {
        for (int i = 0; i < pooledEnemySphere.Count; i++)
        {
            if (!pooledEnemySphere[i].activeInHierarchy)
                return pooledEnemySphere[i];
        }

        GameObject obj = (GameObject)Instantiate(EnemySphere);
        obj.SetActive(false);
        pooledEnemySphere.Add(obj);
        return obj;
    }

    public GameObject GetPooledEnemyMineSphere()
    {
        for (int i = 0; i < pooledEnemyMineSphere.Count; i++)
        {
            if (!pooledEnemyMineSphere[i].activeInHierarchy)
                return pooledEnemyMineSphere[i];
        }

        GameObject obj = (GameObject)Instantiate(EnemyMineSphere);
        obj.SetActive(false);
        pooledEnemyMineSphere.Add(obj);
        return obj;
    }

    GameObject GetPooledShipA()
    {
        for (int i = 0; i < pooledShipsA.Count; i++)
        {
            if (!pooledShipsA[i].activeInHierarchy)
                return pooledShipsA[i];
        }

        GameObject obj = (GameObject)Instantiate(shipA);
        obj.SetActive(false);
        pooledShipsA.Add(obj);
        return obj;

    }


    GameObject GetPooledMeteor()
    {
        for (int i = 0; i < pooledMeteors.Count; i++)
        {
            if (!pooledMeteors[i].activeInHierarchy)
                return pooledMeteors[i];
        }

        GameObject obj = (GameObject)Instantiate(meteor);
        obj.SetActive(false);
        pooledMeteors.Add(obj);
        return obj;

    }

    GameObject GetPooledMisilA()
    {
        for (int i = 0; i < pooledMisilA.Count; i++)
        {
            if (!pooledMisilA[i].activeInHierarchy)
                return pooledMisilA[i];
        }

        GameObject obj = (GameObject)Instantiate(misilA);
        obj.SetActive(false);
        pooledMisilA.Add(obj);
        return obj;

    }


    IEnumerator SetPools()
    {

        for (int i = 0; i < pooledAmountExpAst; i++)
        {
            GameObject obj = (GameObject)Instantiate(explotionAsteroid);
            obj.name = explotionAsteroid.name + i.ToString();
            obj.SetActive(false);
            pooledExplotionAsteroid.Add(obj);
        }

        for (int i = 0; i < pooledAmountSpA; i++)
        {
            GameObject obj = (GameObject)Instantiate(shipA);
            obj.name = shipA.name + i.ToString();
            //depende del nivel
            obj.GetComponent<ShipControl>().shipID = 11;
            obj.SetActive(false);
            pooledShipsA.Add(obj);
        }

        yield return new WaitForSeconds(0.01f);
        GetComponent<GameController>().sldCharge.GetComponent<Slider>().value += 10f;
        for (int i = 0; i < AmountLaser01; i++)
        {
            GameObject obj = (GameObject)Instantiate(laser01);
            obj.name = laser01.name + i.ToString();
            obj.GetComponent<MoveLaser>().speed = 0;
            obj.SetActive(false);
            pooledLaser01.Add(obj);
        }

        yield return new WaitForSeconds(0.01f);

        for (int i = 0; i < AmountEnemySphere; i++)
        {
            GameObject obj = (GameObject)Instantiate(EnemySphere);
            obj.name = EnemySphere.name + i.ToString();
            obj.GetComponent<MoveLaser>().speed = 0;
            obj.SetActive(false);
            pooledEnemySphere.Add(obj);
        }

        yield return new WaitForSeconds(0.01f);

        for (int i = 0; i < AmountEnemyMineSphere; i++)
        {
            GameObject obj = (GameObject)Instantiate(EnemyMineSphere);
            obj.name = EnemyMineSphere.name + i.ToString();
            obj.GetComponent<MoveLaser>().speed = 0;
            obj.SetActive(false);
            pooledEnemyMineSphere.Add(obj);
        }

        yield return new WaitForSeconds(0.01f);

        GetComponent<GameController>().sldCharge.GetComponent<Slider>().value += 10f;
        //meteoros A
        for (int i = 0; i < pooledAmountM; i++)
        {
            GameObject obj = (GameObject)Instantiate(meteor);
            obj.name = meteor.name + i.ToString();
            obj.GetComponent<MoveMeteor>().speed = 0;
            obj.SetActive(false);
            pooledMeteors.Add(obj);


        }
        GetComponent<GameController>().sldCharge.GetComponent<Slider>().value += 40f;
        yield return new WaitForSeconds(0.01f);
        // misiles A
        for (int i = 0; i < pooledAmountMiA; i++)
        {
            GameObject obj = (GameObject)Instantiate(misilA);
            obj.name = misilA.name + i.ToString();
            obj.GetComponent<MisilControl>().speed = 0;
            obj.SetActive(false);
            pooledMisilA.Add(obj);

        }
        GetComponent<GameController>().sldCharge.GetComponent<Slider>().value += 40f;
        yield return new WaitForSeconds(0.01f);
    }

    void ItemHealing()
    {
        StartCoroutine(StartItemHealing());
    }

    IEnumerator StartItemHealing()
    {
        isItemOk = true;
        goItemH = false;
        yield return new WaitForSeconds(2f);
        goItemH = true;
    }

    //void StartToNextMeteor()
    //{
    //    StartCoroutine(WaitToNextMeteor());
    //}

    //IEnumerator WaitToNextMeteor()
    //{
    //    isMeteorBig = false;
    //    yield return new WaitForSeconds(2f);
    //    isMeteorBig = true;
    //}

    //Nivel 101: Meteroros random
    public void Start_Level101()
    {
        StartCoroutine(SpawnHazardsXY(true, false, false));
    }

    //Nivel 102: Naves cruzandose de un lado a otro
    public void Start_Level102()
    {
        StartCoroutine(ParabolicShip(false));
    }

    //Nivel 103: Meteoros Grandes random
    public void Start_Level103()
    {
        StartCoroutine(SpawnHazardsXY(false, true,false));
    }

    //Nivel 104: Naves cruzandose de un lado a otro - disparando esferas
    public void Start_Level104()
    {
        StartCoroutine(ParabolicShip(true));
    }
    //Nivel 105: Meteoros Grandes random con minas
    public void Start_Level105() {
        StartCoroutine(SpawnHazardsXY(false, false, true));
    }

    //Nivel 106: para naves en posiciones definidas que siguen al player
    //y son destruidas con toques
    public void Start_Level106() {
        StartCoroutine(SpawnArcShips(100));
    }

    //Nivel 107(prueba) nivel de proyectiles lanzados desde lejos
    public void Start_MisilA1A2()
    {
        StartCoroutine(SpawnShipAnimation01());
        StartCoroutine(SpawnMisilA1());
        StartCoroutine(SpawnMisilA2());
        
    }

    //Nivel 108 (pruaba): olas de naves
    public void StartEnemyShipA()
    {
        StartCoroutine(SpawnShips());
    }

    public void StartLaser01()
    {
        StartCoroutine(SpawnLaser01());
    }

    public void StartExplotionAsteroid(Transform tfm)
    {
        GameObject exp = null;

        exp = GetPooledExpAst();
        exp.GetComponent<Transform>().position = tfm.position;
        exp.GetComponent<Transform>().rotation = tfm.rotation;
        exp.SetActive(true);
        exp.GetComponent<DestroyByTime>().StartLifeTime(0.5f);

    }
     

    // nivel 101 - 103
    IEnumerator SpawnHazardsXY(bool islvl101, bool islvl103, bool islvl105)
    {
        //para controlar el numvero de spawn del iten
        // int itemSpawn = 2;



        //		bool startShips = false;
        yield return new WaitForSeconds(startWait);

		while (islvl101)
        {

            if (!islvl101)
                break;

            for (int i = 0; i < hazartCount; i++)
            {
                GameObject hazard = null;
                Vector3 spawnPosition = new Vector3(0, 0, 0);
                HealthControl objHealth = GetComponent<HealthControl>();
                Rigidbody PlayerPos = GetComponent<GameController>().mainPlayer.GetComponent<Rigidbody>();

                if (objHealth.currentHealth < 50 && isItemOk)
                {
                    GameObject itemH = Items[0];
                    spawnPosition = new Vector3(
                        Random.Range(-PlayerPos.position.x + 2, PlayerPos.position.x - 2)
                        , Random.Range(-PlayerPos.position.y + 0.5f, PlayerPos.position.y - 0.5f)
                        , spawnValues.z);
                    Quaternion itemRotation = Quaternion.identity;
                    Instantiate(itemH, spawnPosition, itemRotation);
                    isItemOk = false;
                }
                else
                {
                    //posicion de los meteoros que sigue al player
                    if (i % followMultiplePlayer == 0)
                    {
                        spawnPosition = new Vector3(
                            Random.Range(player.position.x - 1.4f, player.position.x + 1.4f)
                            , Random.Range(player.position.y - 1.4f, player.position.y + 1.4f)
                            , spawnValues.z);

                    }
                    else
                    {
                        //posicion de los meteoros aleatoria
                        spawnPosition = new Vector3(
                            Random.Range(-spawnValues.x, spawnValues.x)
                            , Random.Range(-spawnValues.y, spawnValues.y)
                            , spawnValues.z);

                    }

                    hazard = GetPooledMeteor();

                    hazard.GetComponent<Transform>().position = spawnPosition;
                    hazard.GetComponent<MoveMeteor>().speed = speedMeteor;
                    hazard.transform.localScale = new Vector3(10, 10, 10);

                    // apartir del seg 30 aparecen meteoros aleatorios escalados - para el 2do nivel
                    //					if (seconds <= 40 && ((seconds & 4) == 0) && isMeteorBig) {
                    //						hazard.transform.localScale = new Vector3 (60, 60, 60);
                    //						StartToNextMeteor ();
                    //					} else {
                    //						hazard.transform.localScale = new Vector3 (10, 10, 10);
                    //					}


                    hazard.SetActive(true);
                }




                //tiempo de salida de cada meteoro
                yield return new WaitForSeconds(spawnWait);
                if (!islvl101)
                    break;
            }
            //tiempo para que vuela a empezar la ola
            yield return new WaitForSeconds(waveWait);
            if (!islvl101)
                break;
            //			empiezan las naves
            //						if (!startShips) {
            //							StartCoroutine (SpawnShips ());
            //							startShips = true;
            //						}

        }

        //NIVEL 103: Meteoros aleatorias grandes
        while (islvl103)
        {

            if (!islvl103)
                break;

            for (int i = 0; i < hazartCount; i++)
            {
                GameObject hazard = null;
                Vector3 spawnPosition = new Vector3(0, 0, 0);
                HealthControl objHealth = GetComponent<HealthControl>();
                Rigidbody PlayerPos = GetComponent<GameController>().mainPlayer.GetComponent<Rigidbody>();

                if (objHealth.currentHealth < 50 && isItemOk)
                {
                    GameObject itemH = Items[0];
                    spawnPosition = new Vector3(
                        Random.Range(-PlayerPos.position.x + 2, PlayerPos.position.x - 2)
                        , Random.Range(-PlayerPos.position.y + 0.5f, PlayerPos.position.y - 0.5f)
                        , spawnValues.z);
                    Quaternion itemRotation = Quaternion.identity;
                    Instantiate(itemH, spawnPosition, itemRotation);
                    isItemOk = false;
                }
                else
                {
                    //posicion de los meteoros que sigue al player
                    if (i % followMultiplePlayer == 0)
                    {
                        spawnPosition = new Vector3(
                            Random.Range(player.position.x - 1.4f, player.position.x + 1.4f)
                            , Random.Range(player.position.y - 1.4f, player.position.y + 1.4f)
                            , spawnValues.z);

                    }
                    else
                    {
                        //posicion de los meteoros aleatoria
                        spawnPosition = new Vector3(
                            Random.Range(-spawnValues.x, spawnValues.x)
                            , Random.Range(-spawnValues.y, spawnValues.y)
                            , spawnValues.z);

                    }

                    hazard = GetPooledMeteor();
                    hazard.GetComponent<Transform>().position = spawnPosition;
                    hazard.GetComponent<MoveMeteor>().speed = speedMeteor;
                    hazard.transform.localScale = new Vector3(10, 10, 10);
                    // apartir del seg 30 aparecen meteoros aleatorios escalados - para el 2do nivel
                    if ((seconds % 8) == 0)
                    {
                        //hazard.transform.localScale = new Vector3(80, 80, 80);
                        hazard.SetActive(true);
                        hazard.GetComponent<MoveMeteor>().MeteorGrowUp(40f);
                    }
                    else
                    {
                        //hazard.transform.localScale = new Vector3(10, 10, 10);
                        hazard.SetActive(true);
                    }

                    
                }

                //tiempo de salida de cada meteoro
                yield return new WaitForSeconds(spawnWait);
                if (!islvl103)
                    break;
            }
            //tiempo para que vuela a empezar la ola
            yield return new WaitForSeconds(waveWait);
            if (!islvl103)
                break;

        }


        //NIVEL 103: Meteoros aleatorias grandes
        while (islvl105)
        {

            if (!islvl105)
                break;

            for (int i = 0; i < hazartCount; i++)
            {
                GameObject hazard = null;
                Vector3 spawnPosition = new Vector3(0, 0, 0);
                HealthControl objHealth = GetComponent<HealthControl>();
                Rigidbody PlayerPos = GetComponent<GameController>().mainPlayer.GetComponent<Rigidbody>();

                if (objHealth.currentHealth < 50 && isItemOk)
                {
                    GameObject itemH = Items[0];
                    spawnPosition = new Vector3(
                        Random.Range(-PlayerPos.position.x + 2, PlayerPos.position.x - 2)
                        , Random.Range(-PlayerPos.position.y + 0.5f, PlayerPos.position.y - 0.5f)
                        , spawnValues.z);
                    Quaternion itemRotation = Quaternion.identity;
                    Instantiate(itemH, spawnPosition, itemRotation);
                    isItemOk = false;
                }
                else
                {
                    //posicion de los meteoros que sigue al player
                    if (i % followMultiplePlayer == 0)
                    {
                        spawnPosition = new Vector3(
                            Random.Range(player.position.x - 1.4f, player.position.x + 1.4f)
                            , Random.Range(player.position.y - 1.4f, player.position.y + 1.4f)
                            , spawnValues.z);

                    }
                    else
                    {
                        //posicion de los meteoros aleatoria
                        spawnPosition = new Vector3(
                            Random.Range(-spawnValues.x, spawnValues.x)
                            , Random.Range(-spawnValues.y, spawnValues.y)
                            , spawnValues.z);

                    }

                    hazard = GetPooledMeteor();
                    hazard.GetComponent<Transform>().position = spawnPosition;
                    hazard.GetComponent<MoveMeteor>().speed = speedMeteor;
                    hazard.transform.localScale = new Vector3(10, 10, 10);
                    // apartir del seg 30 aparecen meteoros aleatorios escalados - para el 2do nivel
                    if ((seconds % 8) == 0)
                    {
                        //hazard.transform.localScale = new Vector3(80, 80, 80);
                        hazard.SetActive(true);
                        hazard.GetComponent<MoveMeteor>().MeteorGrowUp(40f);
                    }
                    else
                    {
                        //hazard.transform.localScale = new Vector3(10, 10, 10);
                        hazard.SetActive(true);
                    }

                    //MINESPHERE
                    GameObject hazardMine = GetPooledEnemyMineSphere();
                    Vector3 spawnMinePosition = new Vector3();
                    if (i % 3 == 0)
                    {
                        spawnMinePosition = new Vector3(
                            Random.Range(player.position.x - 1.4f, player.position.x + 1.4f)
                            , Random.Range(player.position.y - 1.4f, player.position.y + 1.4f)
                            , spawnValues.z);

                    }
                    else
                    {
                        //posicion de los minas aleatoria
                        spawnMinePosition = new Vector3(
                            Random.Range(-spawnValues.x, spawnValues.x)
                            , Random.Range(-spawnValues.y, spawnValues.y)
                            , spawnValues.z);

                    }
          
                    hazardMine.GetComponent<Transform>().position = spawnMinePosition;
                    hazardMine.GetComponent<MoveLaser>().speed = -speedMeteor;
                    //hazard.transform.localScale = new Vector3(1, 1, 1);
                    if ((seconds % 3) == 0)
                        hazardMine.SetActive(true);
                }

                //tiempo de salida de cada meteoro /spawnWait
                yield return new WaitForSeconds(0.3f);
                if (!islvl105)
                    break;
            }
            //tiempo para que vuela a empezar la ola
            yield return new WaitForSeconds(waveWait);
            if (!islvl105)
                break;

        }

        //para llamar a las naves
        //StartCoroutine(SpawnShips());

    }

  
    // nivel 102
    IEnumerator ParabolicShip(bool shoot)
    {
        yield return new WaitForSeconds(1f);

        while (true)
        {
            GameObject hazard = null;
            Vector3 _target = new Vector3();
            float _firingAngle = 0f;
            float _gravity = 0f;
            float _dirY, _dirz;
            hazard = GetPooledShipA();
            //new Vector3(Random.Range(-160,-150), Random.Range(-8,8), 120);
            //pos   (-40,15,30)
            hazard.GetComponent<Transform>().position = new Vector3(-38, Random.Range(0, 17), 30);
            //hazard.GetComponent<Transform>().rotation = Quaternion.identity;
            hazard.GetComponent<Transform>().rotation = Quaternion.Euler(0f, 270f, 0f);
            hazard.GetComponent<Transform>().localScale = new Vector3(3f, 3f, 3f);
            hazard.GetComponent<ShipControl>().shipID = 104;

            //target   (10,-2,5)
            _target = new Vector3(10, Random.Range(-3, 3), 5);
            _firingAngle = 30f;
            //gravedad o velocidad
            _gravity = 10f;
            _dirY = -1f;
            _dirz = -1f;

            hazard.SetActive(true);
            hazard.GetComponent<ShipControl>().Start_ParabolicShipShooter(_target, _firingAngle, _gravity, _dirY, _dirz, shoot);

            // 2da nave
            yield return new WaitForSeconds(0.5f);

            hazard = GetPooledShipA();
            //pos   (-40,15,30)
            hazard.GetComponent<Transform>().position = new Vector3(38, Random.Range(0, 17), 30);
            hazard.GetComponent<Transform>().rotation = Quaternion.Euler(0f, 270f, 0f);
            hazard.GetComponent<Transform>().localScale = new Vector3(3f, 3f, 3f);
            hazard.GetComponent<ShipControl>().shipID = 104;

            //target   (10,-2,5)
            _target = new Vector3(-10, Random.Range(-3, 3), 5);
            _firingAngle = 30f;
            //gravedad o velocidad
            _gravity = 10f;
            _dirY = -1f;
            _dirz = -1f;

            hazard.SetActive(true);
            hazard.GetComponent<ShipControl>().Start_ParabolicShipShooter(_target, _firingAngle, _gravity, _dirY, _dirz, shoot);



            yield return new WaitForSeconds(1f);

        }




    }

    // nivel 106
    //para naves en posiciones definidas que siguen al player
    //y son destruidas con toques
    IEnumerator SpawnArcShips(int repeticiones)
    {
        int conWave = 0;
        islvl106 = true;
        GameObject s = null;
        yield return new WaitForSeconds(waveWait);


        Vector3 spawnPosition = new Vector3(0, 0, 0);
        //		Quaternion spawnRotation = Quaternion.identity;

        while (islvl106 && (conWave < repeticiones))
        {
            //Centro
            spawnPosition = new Vector3(0,3f, 10f);
            s = pooledShipsA[0];
            s.transform.position = spawnPosition;
            s.GetComponent<ShipControl>().SetColor(0);
            s.GetComponent<ShipControl>().shipID = 0;
            s.SetActive(true);
            
            yield return new WaitForSeconds(0.5f);
            //iZQUIERDA
            spawnPosition = new Vector3(-2f, 2f, 10f);
            s = pooledShipsA[1];
            s.transform.position = spawnPosition;
            s.GetComponent<ShipControl>().SetColor(0);
            s.GetComponent<ShipControl>().shipID = 1;
            s.SetActive(true);

            yield return new WaitForSeconds(0.5f);
            //dERECHA
            spawnPosition = new Vector3(2f, 2f, 10f);
            s = pooledShipsA[2];
            s.transform.position = spawnPosition;
            s.GetComponent<ShipControl>().SetColor(0);
            s.GetComponent<ShipControl>().shipID = 2;
            s.SetActive(true);

            pooledShipsA[0].GetComponent<EnemyWeaponController>().StartInvokes();
            pooledShipsA[1].GetComponent<EnemyWeaponController>().StartInvokes();
            pooledShipsA[2].GetComponent<EnemyWeaponController>().StartInvokes();

            //espera para desaparecer 
            yield return new WaitForSeconds(4.5f);

            for (int i = 0; i < pooledShipsA.Count; i++)
            {
                if (pooledShipsA[i].activeInHierarchy)
                {
                    pooledShipsA[i].GetComponent<EnemyWeaponController>().CancelInvokes();
                    pooledShipsA[i].SetActive(false);

                }
            }

            yield return new WaitForSeconds(1f);

            conWave++;
        }

        //cambio al sgt nivel
        level = 3;

        //        GetComponent<GameController>().GameOver();


    }

    //pruaba para 107
    IEnumerator SpawnShipAnimation01()
    {
        bool isShipA = true;

        yield return new WaitForSeconds(0.1f);

        GameObject hazard = null;

        while (isShipA)
        {
            yield return new WaitForSeconds(0.5f);

            hazard = GetPooledShipA();
            // -140,0,120
            hazard.GetComponent<Transform>().position = new Vector3(Random.Range(-160, -150), Random.Range(-8, 8), 120);
            //hazard.GetComponent<Transform>().rotation = Quaternion.identity;
            hazard.GetComponent<Transform>().rotation = Quaternion.Euler(0f, 270f, 45f);
            hazard.GetComponent<Transform>().localScale = new Vector3(3f, 3f, 3f);
            hazard.GetComponent<ShipControl>().shipID = 102;
            hazard.GetComponent<ShipControl>().ShipVelocity = 15f;
            hazard.GetComponent<ShipControl>().targetColor.color = new Color(0, 0, 0, 0);

            hazard.SetActive(true);
        }


    }
    //prueba 107 - proyectiles
    IEnumerator SpawnMisilA1()
    {
        islvl102 = true;
        int contWave = 0;
        Vector3 offset;

        yield return new WaitForSeconds(12f);

        GameObject hazard = null;
        //        Vector3[] spawnPosition = new Vector3[4];
        //        spawnPosition[0] = new Vector3(-5,2,100);
        //        spawnPosition[1] = new Vector3(,0,100);
        //        spawnPosition[2] = new Vector3(10,-5,100);
        //        spawnPosition[3] = new Vector3(-10,5,100);
        //        spawnPosition[4] = new Vector3(-10,0,100);
        //        spawnPosition[5] = new Vector3(-10,-5,100);
        //        spawnPosition[6] = new Vector3(0,5,100);
        //        spawnPosition[7] = new Vector3(0,0,100);
        //        spawnPosition[8] = new Vector3(0,-5,100);

        Rigidbody PlayerPos = GetComponent<GameController>().mainPlayer.GetComponent<Rigidbody>();

        while (islvl102)
        {
            yield return new WaitForSeconds(1f);

            //            int PosVec = Random.Range(0, 3);



            hazard = GetPooledMisilA();
            if (contWave % followMultiplePlayer == 0)
            {
                hazard.GetComponent<Transform>().position = new Vector3(player.position.x, player.position.y, 100f);
                offset = hazard.GetComponent<Transform>().position - player.position;
                hazard.GetComponent<Transform>().rotation = Quaternion.LookRotation(offset);
            }
            else
            {
                hazard.GetComponent<Transform>().position = new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), 100f);
                hazard.GetComponent<Transform>().rotation = Quaternion.identity;
                //spawnPosition[PosVec];
            }

            // obtengo la posicion del player conrespecto a la nave que dispara

            //hazard.GetComponent<MisilControl>().playerFirstPos = player.position - hazard.GetComponent<Transform> ().position;

            hazard.GetComponent<MisilControl>().speed = speedMisilA;

            hazard.SetActive(true);

            contWave++;
            if (contWave == 100)
                contWave = 0;
        }
    }
    //prueba 107 - proyectiles
    IEnumerator SpawnMisilA2()
    {
        islvl102 = true;
        //int contWave = 0;
        //Vector3 offset;
        int PosVecX = 0;
        int PosVecY = 0;

        yield return new WaitForSeconds(14f);

        GameObject hazard = null;
        //        Vector3[] spawnPosition = new Vector3[8];
        ////        spawnPosition[0] = new Vector3(10,5,100);
        ////        spawnPosition[1] = new Vector3(10,0,100);
        ////        spawnPosition[2] = new Vector3(10,-5,100);
        ////        spawnPosition[3] = new Vector3(-10,5,100);
        //        spawnPosition[0] = new Vector3(-10,5,100);
        //        spawnPosition[1] = new Vector3(0,5,100);
        //        spawnPosition[2] = new Vector3(10,5,100);
        //        spawnPosition[3] = new Vector3(-10,0,100);
        //        spawnPosition[4] = new Vector3(10,0,100);
        //        spawnPosition[5] = new Vector3(-10,-5,100);
        //        spawnPosition[6] = new Vector3(0,-5,100);
        //        spawnPosition[7] = new Vector3(10,-5,100);

        Rigidbody PlayerPos = GetComponent<GameController>().mainPlayer.GetComponent<Rigidbody>();

        while (islvl102)
        {
            yield return new WaitForSeconds(0.5f);

            // -4,-2.3
            // 2.3,4
            //2.3,4
            //-2.3,-4

            //izquierda
            PosVecX = Random.Range(-6, -2);
            PosVecY = Random.Range(-6, 6);

            hazard = GetPooledMisilA();

            hazard.GetComponent<Transform>().position = new Vector3(PosVecX, PosVecY, 100f);
            hazard.GetComponent<Transform>().rotation = Quaternion.identity;

            hazard.GetComponent<MisilControl>().speed = speedMisilA;

            hazard.SetActive(true);

            //derecha
            hazard = null;

            PosVecX = Random.Range(2, 6);
            PosVecY = Random.Range(-6, 6);

            hazard = GetPooledMisilA();

            hazard.GetComponent<Transform>().position = new Vector3(PosVecX, PosVecY, 100f);
            hazard.GetComponent<Transform>().rotation = Quaternion.identity;

            hazard.GetComponent<MisilControl>().speed = speedMisilA;

            hazard.SetActive(true);

            //arriba
            hazard = null;

            PosVecX = Random.Range(-2, 2);
            PosVecY = Random.Range(2, 6);

            hazard = GetPooledMisilA();

            hazard.GetComponent<Transform>().position = new Vector3(PosVecX, PosVecY, 100f);
            hazard.GetComponent<Transform>().rotation = Quaternion.identity;

            hazard.GetComponent<MisilControl>().speed = speedMisilA;

            hazard.SetActive(true);

            //abajo
            //arriba
            hazard = null;

            PosVecX = Random.Range(-2, 2);
            PosVecY = Random.Range(-2, -6);

            hazard = GetPooledMisilA();

            hazard.GetComponent<Transform>().position = new Vector3(PosVecX, PosVecY, 100f);
            hazard.GetComponent<Transform>().rotation = Quaternion.identity;

            hazard.GetComponent<MisilControl>().speed = speedMisilA;

            hazard.SetActive(true);
        }
    }



    IEnumerator SpawnLaser01()
    {

        yield return new WaitForSeconds(1f);

        while (isLoopLaser01)
        {
            yield return new WaitForSeconds(0.5f);
            GameObject hazardL = null;
            GameObject hazardR = null;

            hazardL = GetPooledLaser01();

            hazardL.GetComponent<Transform>().position = Laser01Left.position;
            hazardL.GetComponent<Transform>().rotation = Laser01Left.rotation;
            hazardL.GetComponent<MoveLaser>().speed = speedLaser01;
            hazardL.SetActive(true);

            hazardR = GetPooledLaser01();
            hazardR.GetComponent<Transform>().position = Laser01Right.position;
            hazardR.GetComponent<Transform>().rotation = Laser01Right.rotation;
            hazardR.GetComponent<MoveLaser>().speed = speedLaser01;

            hazardR.SetActive(true);

        }

    }
    
    IEnumerator SpawnMisilHazardsXY()
    {
        islvl103 = true;
        timeLeft = startTime + 5f;
        yield return new WaitForSeconds(startWaitMlA + 4f);
        while (islvl103)
        {

            if (!islvl103)
                break;

            for (int i = 0; i < pooledAmountMiA; i++)
            {
                GameObject hazard = null;
                Vector3 spawnPosition = new Vector3(0, 0, 0);
                HealthControl objHealth = GetComponent<HealthControl>();
                Rigidbody PlayerPos = GetComponent<GameController>().mainPlayer.GetComponent<Rigidbody>();

                if (objHealth.currentHealth < 50 && isItemOk)
                {
                    GameObject itemH = Items[0];
                    spawnPosition = new Vector3(
                        Random.Range(-PlayerPos.position.x + 2, PlayerPos.position.x - 2)
                        , Random.Range(-PlayerPos.position.y + 0.5f, PlayerPos.position.y - 0.5f)
                        , spawnValues.z);
                    Quaternion itemRotation = Quaternion.identity;
                    Instantiate(itemH, spawnPosition, itemRotation);
                    isItemOk = false;
                }
                else
                {
                    //posicion de los misiles que siguen al player
                    if (i % followMultiplePlayer == 0)
                    {
                        spawnPosition = new Vector3(
                            Random.Range(player.position.x - 1.4f, player.position.x + 1.4f)
                            , Random.Range(player.position.y - 1.4f, player.position.y + 1.4f)
                            , spawnValues.z);

                    }
                    else
                    {
                        //posicion de los misiles aleatoria
                        spawnPosition = new Vector3(
                            Random.Range(-spawnValues.x, spawnValues.x)
                            , Random.Range(-spawnValues.y, spawnValues.y)
                            , spawnValues.z);

                    }

                    hazard = GetPooledMisilA();

                    hazard.GetComponent<Transform>().position = spawnPosition;
                    hazard.GetComponent<MisilControl>().speed = speedMisilA;

                    hazard.SetActive(true);
                }

                //tiempo de salida de cada meteoro
                yield return new WaitForSeconds(spawnWaitMlA);
                if (!islvl103)
                    break;
            }
            //tiempo para que vuela a empezar la ola
            yield return new WaitForSeconds(waveWaitMlA);
            if (!islvl103)
                break;

        }
        level = 13;
    }

    IEnumerator SpawnShips()
    {
        timeLeft = startTime + 5f;
        islvl108 = true;
        GameObject s = null;
        yield return new WaitForSeconds(startWaitSpA + 4f);


        Vector3 spawnPosition = new Vector3(0, 0, 0);
        //      Quaternion spawnRotation = Quaternion.identity;

        while (islvl108)
        {

            for (int i = 0; i < pooledShipsA.Count; i++)
            {
                spawnPosition = new Vector3(
                    Random.Range(-spawnValuesSpA.x, spawnValuesSpA.x)
                    , Random.Range(-spawnValuesSpA.y, spawnValuesSpA.y)
                    , spawnValuesSpA.z);

                s = GetPooledShipA();
                //nueva posicion para la nave
                s.transform.position = spawnPosition;
                //nueva posticion para moverse
                Vector3 newPos = new Vector3(
                    Random.Range(spawnPosition.x - 2f, spawnPosition.x + 2f)
                    , Random.Range(spawnPosition.y - 2f, spawnPosition.y + 2f)
                    , spawnPosition.z);
                s.GetComponent<ShipControl>().newShipPos = newPos;
                s.GetComponent<ShipControl>().shipID = 108;
                s.SetActive(true);
                s.GetComponent<EnemyWeaponController>().StartInvokes();

                //                txtTitleLevel.text = s.transform.position.x.ToString()
                //                + " - " + s.transform.position.y.ToString()
                //                + " - " + s.transform.position.z.ToString();

                //activo el spawn de posicion
                s.GetComponent<ShipControl>().isChangePos = true;
                s.GetComponent<ShipControl>().StartChangePawnPosition();

                //tiempo de salida de cada shipA
                yield return new WaitForSeconds(spawnWaitSpA);
                if (!islvl102)
                    break;
            }

            //tiempo para que vuela a empezar la ola
            yield return new WaitForSeconds(waveWaitSpA);
            if (!islvl108)
                break;
        }

        //cambio al sgt nivel
        level = 13;

    }

   

    //REVISAR
    Vector3 GetSpawnShipPlace()
    {
        Vector3 resPlace = new Vector3();

        for (int i = 0; i < spawnPlace.Length; i++)
        {
            for (int j = 0; j < spawnPlaceUsed.Length; j++)
            {
                if (spawnPlace[i] != spawnPlaceUsed[j])
                {
                    resPlace = new Vector3(spawnPlace[i].x, spawnPlace[i].y, 1f);
                    return resPlace;
                }
            }
        }
        //
        //        GameObject obj = (GameObject)Instantiate(misilA);
        //        obj.SetActive(false);
        //        pooledMisilA.Add(obj);
        //        return obj;
        return resPlace;

    }


    //  IEnumerator SpawnHazardsX(){
    //      islvl01 = true;
    ////        bool startShips = false;
    //      yield return new WaitForSeconds (startWait);
    //      while (islvl01) {
    //          
    //          for (int i = 0; i< hazartCount;i++){
    //              GameObject hazard = null;
    //              Vector3 spawnPosition = new Vector3(0,0,0);
    //
    //              //posicion de los meteoros
    //              if (i % 4 == 0) {
    //                  spawnPosition = new Vector3 (
    //                      player.position.x
    //                      ,spawnValues.y
    //                      ,spawnValues.z);
    //              } else {
    //                  spawnPosition = new Vector3 (
    //                      Random.Range(-spawnValues.x,spawnValues.x)
    //                      ,spawnValues.y
    //                      ,spawnValues.z);
    //                  
    //              }
    //
    //              //escogo los hazard
    ////                hazard = hazards[Random.Range(0,hazards.Length)];
    ////                Quaternion spwanRotation = Quaternion.identity;
    //
    ////                GameObject go = (GameObject) Instantiate (hazard, spawnPosition, spwanRotation);
    ////                go.GetComponent<MoveMeteor> ().position = spawnPosition;
    //              //              Instantiate (hazard, spawnPosition, spwanRotation);
    //              hazard = GetPooledMeteor();
    //
    //              hazard.GetComponent<Transform> ().position = spawnPosition;
    //              hazard.GetComponent<MoveMeteor> ().speed = speedMeteor;
    //              hazard.SetActive (true);
    //
    //              yield return new WaitForSeconds (spawnWait);
    //
    //          }
    //
    //          yield return new WaitForSeconds (waveWait);
    //
    //          //empiezan las naves
    ////            if (!startShips) {
    ////                StartCoroutine (SpawnShips ());
    ////                startShips = true;
    ////            }
    //
    //      }
    //
    //
    //
    //  }
}


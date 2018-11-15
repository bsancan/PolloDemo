using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class ManualVectors
{
    public List<Vector3> vectors;
}

[System.Serializable]
public class PlayerStatus{
    //HEALTH
    //public GameObject waitReStartHP;
    public Text txtReStartHP;
    public int secondReStartHP;
    public int startHealth;                           // The amount of health the player starts the game with.
    public int currentHealth;                                   // The current health the player has.
    public Slider sldHealth;                                 // Reference to the UI's health bar.
    public int startEnergy;
    public int currentEnergy;
    public int energyConsumption;
    public Slider sldEnergy;
    public Image imgDamage;  
    public Color flashColour = new Color(1f, 0f, 0f, 0.6f);     // The colour the damageImage is set to, to flash.
    public float flashSpeed;                               // The speed the damageImage will fade at.
    public bool damaged;

    //GameOver
    public CanvasGroup cnvGameOver;

}

[System.Serializable]
public class UIArrows{
    public GameObject arrowLeft;
    public GameObject arrowRight;
    public GameObject arrowUp;
    public GameObject arrowDown;
}

[System.Serializable]
public class Items{
    //energia
    public GameObject energy;
    public int amountEnergy;
    public float speedEnergy;
    public float energyWait;
    public float waveEnergyWait;
    public Vector3 energyRange;
    public List<GameObject> poolEnergy;
    //armas
    public GameObject weapon;
}
[System.Serializable]
public class Meteor {
    public GameObject meteor;
    public int amountMeteor;
    public float speedMeteor;
    public float meteorWait;
    public float waveMeteorWait;
    public List<GameObject> poolMeteor;
}
public class scrGameControll : MonoBehaviour {
    public Text txtFrames;

    public enum Level{
        level_00,
        level_01,
        level_02
    }


    public Camera mainCamera;
    public GameObject player;
    public GameObject galaxySphere;
    public GameObject galaxyDust;
    public Transform traBoundary;
    //public Slider sldHealth;
    //level
    public Level level;
    //arrow
    public UIArrows arrow;
    //Items
    public Items item;
    //playerstatus
    public PlayerStatus status;
    //targets
    public GameObject ParentTargetEnemy;
    public GameObject targetEnemy;
    public int amountTarget;
    public Vector3 meteorRange;
    public List<GameObject> poolTargetEnemy;
    //Meteors
    public Meteor meteorA;
    public Meteor meteorSide;
    public Meteor meteorV1;
    public Meteor meteorV2;

    //public GameObject meteor;
    //public int amountMeteor;
    //public float speedMeteor;
    //public float meteorWait;
    //public float waveMeteorWait;
    //public List<GameObject> poolMeteor;
    //explotion Meteor
    public GameObject explotionMeteor;
    public int amountExplotionMeteor;
    public List<GameObject> poolExplotionMeteor;

//    public GameObject crossHair;
//    public GameObject crossHairA;

//    public GameObject crossHairB;
//    public GameObject crossHairRefA;
//    public GameObject crossHairRefB;

    private Rigidbody rbPlayer;
    private Rigidbody rbGalaxy;
//    private RectTransform recCrossHair;
//    private RectTransform recCrossHairA;
//    private RectTransform recCrossHairB;

    private scrPlayerControll playerControll;

    private RectTransform recTargetEnemy;

    private bool isHealthActive;

    private Vector3 initialGalaxyDust;
    private Vector3 initialBoundary;

    //solo para crear xmls
    public string pathMeteorLevel01;
    public string pathHealthLevel01;
    //public string FileName;
    public List<GameObject> goGroup;
    //public List<Vector3> mv;
    public ManualVectors mv;
    public ManualVectors mhv;

    void Awake(){
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 60;

        rbPlayer = player.GetComponent<Rigidbody>();
        rbGalaxy = galaxySphere.GetComponent<Rigidbody>();
//        recCrossHair = crossHair.GetComponent<RectTransform>();
//        recCrossHairA = crossHairA.GetComponent<RectTransform>();
//        recCrossHairB = crossHairB.GetComponent<RectTransform>();

        playerControll = player.GetComponent<scrPlayerControll>();
        recTargetEnemy = ParentTargetEnemy.GetComponent<RectTransform>();

        meteorA.poolMeteor = new List<GameObject>();
        meteorSide.poolMeteor = new List<GameObject>();
        poolTargetEnemy = new List<GameObject>();
        poolExplotionMeteor = new List<GameObject>();
        item.poolEnergy = new List<GameObject>();

        initialGalaxyDust = galaxyDust.transform.position;
        initialBoundary = traBoundary.transform.position;

        mv = SaveLoadXml.LoadVectorsFromResources(pathMeteorLevel01);
        mhv = SaveLoadXml.LoadVectorsFromResources(pathHealthLevel01);

        //mv = SaveLoadXml.LoadXml(pathMeteorLevel01 + "/" + FileName);

        //SaveLoadXml.SaveDialogueXml(pathMeteorLevel01 , new ManualVectors());

        //GetVectors();

    }

	void Start () {
        //health
        status.sldHealth.maxValue = status.startHealth;
        status.sldHealth.value = status.startHealth;
        status.currentHealth = status.startHealth;

        //energy
        status.sldEnergy.maxValue = status.startEnergy;
        status.sldEnergy.value = status.startEnergy;
        status.currentEnergy = status.startEnergy;

        arrow.arrowDown.SetActive(false);
        arrow.arrowUp.SetActive(false);
        arrow.arrowRight.SetActive(false);
        arrow.arrowLeft.SetActive(false);

     
        //CreatePools();


        //if (level == Level.level_01) {
        //    StartCoroutine(WaveMeteorXY());
        //}

        StartCoroutine(EnergyStatus());

        //indicador de espera para HP
        status.txtReStartHP.text = "";
        //StartCoroutine(WaveMeteorXY_Sides_BIG());

        StartCoroutine(RecalculateFPS());

        //QualitySettings.vSyncCount = 0;

        //StartCoroutine(WaveMeteorV1());


    }
	
	void Update () {
        //Application.targetFrameRate = 24;
        //health
        if (status.damaged)
        {
            status.imgDamage.color = status.flashColour;
        }
        else
        {
            status.imgDamage.color = Color.Lerp(status.imgDamage.color, Color.clear, status.flashSpeed * Time.deltaTime);
        }
        status.damaged = false;

        //if (level == Level.level_00)
        //{
        //    if (status.currentEnergy < (int)(status.startEnergy * 40 / 100)){
        //        if (!isHealthActive){
        //            StartCoroutine(WaveItemEnergyXY());
        //        }
        //    }
        //}
        //else if (level == Level.level_01)
        //{
        //    if (status.currentEnergy < (int)(status.startEnergy * 40 / 100))
        //    {
        //        if (!isHealthActive)
        //        {
        //            StartCoroutine(WaveItemEnergyXY());
        //        }
        //    }
        //}

    }

    void FixedUpdate(){
        //giro de galaxia
        Vector3 vt = new Vector3(Mathf.Clamp(rbPlayer.position.y/2f,-1f,1f) , Mathf.Clamp( (rbPlayer.position.x/2f),-2-5f,2.5f), 0f);
        Quaternion deltaGalaxy = Quaternion.Euler(vt);
        rbGalaxy.MoveRotation(deltaGalaxy);

        Vector3 playerPos = rbPlayer.position;
        PlayerRange pr = player.GetComponent<scrPlayerControll>().playerRange;

        rbGalaxy.position = new Vector3(rbGalaxy.position.x,rbGalaxy.position.y, playerPos.z);

        galaxyDust.transform.position = initialGalaxyDust + playerPos;
        
        traBoundary.position = initialBoundary + playerPos;

            //new Vector3(galaxyDust.transform.position.x, galaxyDust.transform.position.y, initialGalaxyDust.z + playerPos.z);

        #region Arrows
        //left
        if ((playerPos.x < (-pr.minRange.x + 0.01f)) && (playerPos.x > (-pr.minRange.x - 0.01f)))
        {
            if (!arrow.arrowLeft.activeInHierarchy)
                arrow.arrowLeft.SetActive(true);
        }
        else
        {
            if (arrow.arrowLeft.activeInHierarchy)
                arrow.arrowLeft.SetActive(false);
        }
        //right
        if ((playerPos.x < (pr.maxRange.x + 0.01f)) && (playerPos.x > (pr.maxRange.x - 0.01f)))
        {
            if (!arrow.arrowRight.activeInHierarchy)
                arrow.arrowRight.SetActive(true);
        }
        else
        {
            if (arrow.arrowRight.activeInHierarchy)
                arrow.arrowRight.SetActive(false);
        }
        //DOWN
        if ((playerPos.y < (-pr.minRange.y + 0.01f)) && (playerPos.y > (-pr.minRange.y - 0.01f)))
        {
            if (!arrow.arrowDown.activeInHierarchy)
                arrow.arrowDown.SetActive(true);
        }
        else
        {
            if (arrow.arrowDown.activeInHierarchy)
                arrow.arrowDown.SetActive(false);
        }
        //UP
        if ((playerPos.y < (pr.maxRange.y + 0.01f)) && (playerPos.y > (pr.maxRange.y - 0.01f)))
        {
            if (!arrow.arrowUp.activeInHierarchy)
                arrow.arrowUp.SetActive(true);
        }
        else
        {
            if (arrow.arrowUp.activeInHierarchy)
                arrow.arrowUp.SetActive(false);
        }
        #endregion



//        //movimiento de los crosshair con el player
//        //crossHairA
//        Vector2 viewPortPosA = mainCamera.WorldToViewportPoint(crossHairRefA.GetComponent<Transform>().position);
//        Vector2 screenPosA = new Vector2(
//                                 ((viewPortPosA.x * recCrossHair.sizeDelta.x) - (recCrossHair.sizeDelta.x * 0.5f)),
//                                 ((viewPortPosA.y * recCrossHair.sizeDelta.y) - (recCrossHair.sizeDelta.y * 0.5f)));
//        recCrossHairA.anchoredPosition = screenPosA;
//
//        //crossHairA
//        Vector2 viewPortPosB = mainCamera.WorldToViewportPoint(crossHairRefB.GetComponent<Transform>().position);
//        Vector2 screenPosB = new Vector2(
//            ((viewPortPosB.x * recCrossHair.sizeDelta.x) - (recCrossHair.sizeDelta.x * 0.5f)),
//            ((viewPortPosB.y * recCrossHair.sizeDelta.y) - (recCrossHair.sizeDelta.y * 0.5f)));
//        recCrossHairB.anchoredPosition = screenPosB;
    }

    IEnumerator RecalculateFPS() {
        while (true)
        {
            float fps = 1 / Time.deltaTime;

            txtFrames.text = fps.ToString();
            yield return new WaitForSeconds(1f);
        }
    }

    //solo para obtener los vectores de los meteoros seteados

    void GetVectors() {

        //crear un lista de los vectores de cada meteoro;
        foreach (GameObject gm in goGroup)
        {

            for (int c = 0; c < gm.transform.childCount - 1; c++)
            {
                Vector3 pos = gm.transform.GetChild(c).position;
                mhv.vectors.Add(pos);
            }

            gm.SetActive(false);

        }
        SaveLoadXml.SaveDialogueXml(pathHealthLevel01, mhv);
        
    }


    #region PlayerStatus


    public void TakeDamage (int amount){
        status.damaged = true;
        status.currentHealth -= amount;
        status.sldHealth.value = status.currentHealth;

        StopCoroutine(WaitForReStartHP());
        StartCoroutine(WaitForReStartHP());

        if (status.currentHealth == 0) {
            //
            StartCoroutine(ShowGameOver());
        }

    }
    IEnumerator WaitForReStartHP() {

        yield return new WaitForSeconds(4f);
        float timeLeft = status.secondReStartHP;
        int seconds;
        status.txtReStartHP.text = "5";
        while (true) {
            timeLeft -= Time.deltaTime;
            //minutes = (int)(timeLeft / 60f);
            seconds = (int)(timeLeft % 60f);
            status.txtReStartHP.text = seconds.ToString();
            yield return new WaitForEndOfFrame();
            if (seconds == 0)
                break;
        }

        status.currentHealth = status.startHealth;
        status.sldHealth.maxValue = status.startHealth;
        status.sldHealth.value = status.startHealth;

        status.txtReStartHP.text = "";
    }

    IEnumerator EnergyStatus() {
        yield return new WaitForSeconds(1f);

        while (status.currentEnergy > 0) {
            yield return new WaitForSeconds(1f);
            status.currentEnergy -= status.energyConsumption;
            status.sldEnergy.value = status.currentEnergy;

        }
        StartCoroutine(ShowGameOver());
    }

    IEnumerator ShowGameOver(){
        //GetComponent<GameController> ().GameOver ();
        player.SetActive(false);

        yield return new WaitForSeconds (2f);
        status.cnvGameOver.alpha = 1f;

        yield return new WaitForSeconds (3.5f);
        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
    }

    #endregion

  #region POOLS

    void CreatePools(){

        //GameObject LASERS = new GameObject();
        //LASERS.name = "GO_LASERS";
        for (int i = 0; i < amountTarget; i++)
        {
            GameObject obj = (GameObject)Instantiate(targetEnemy);
            obj.transform.parent = ParentTargetEnemy.transform;
            obj.name = targetEnemy.name + "_" + i.ToString("00");
            obj.transform.localScale = new Vector3(1f,1f,1f);
            obj.SetActive(false);
            poolTargetEnemy.Add(obj);
        }
        //METEOROS
        GameObject parentMeteor = new GameObject();
        parentMeteor.name = "GO_METEORS";
        parentMeteor.transform.position = Vector3.zero;
        for (int i = 0; i < meteorA.amountMeteor; i++)
        {
            GameObject obj = (GameObject)Instantiate(meteorA.meteor);
            obj.transform.parent = parentMeteor.transform;
            obj.name = meteorA.meteor.name + "_" + i.ToString("00");
            obj.transform.localScale = new Vector3(1f,1f,1f);
            obj.GetComponent<scrMeteorControll>().meteorID = i;
            obj.GetComponent<scrMeteorControll>().gameControll = GetComponent<scrGameControll>();
            obj.SetActive(false);
            meteorA.poolMeteor.Add(obj);
        }
        //METEOROS SIDES
        GameObject parentMeteorS = new GameObject();
        parentMeteorS.name = "GO_METEORS_SIDE";
        parentMeteorS.transform.position = Vector3.zero;
        for (int i = 0; i < meteorSide.amountMeteor; i++)
        {
            GameObject obj = (GameObject)Instantiate(meteorSide.meteor);
            obj.transform.parent = parentMeteorS.transform;
            obj.name = meteorSide.meteor.name + "_" + i.ToString("00");
            obj.transform.localScale = new Vector3(1f, 1f, 1f);
            obj.GetComponent<scrMeteorControll>().meteorID = i;
            obj.GetComponent<scrMeteorControll>().gameControll = GetComponent<scrGameControll>();
            obj.SetActive(false);
            meteorSide.poolMeteor.Add(obj);
        }
        //EXPLOTION METEOR
        GameObject parentExplotion = new GameObject();
        parentExplotion.name = "GO_EXPLOTION_METEOR";
        parentExplotion.transform.position = Vector3.zero;
        for (int i = 0; i < amountExplotionMeteor; i++)
        {
            GameObject obj = (GameObject)Instantiate(explotionMeteor);
            obj.transform.parent = parentExplotion.transform;
            obj.name = explotionMeteor.name + "_" + i.ToString("00");
            obj.transform.localScale = new Vector3(1f,1f,1f);
            obj.SetActive(false);
            poolExplotionMeteor.Add(obj);
        }



        //ITEM-ENERGY
        GameObject parentItemHealth = new GameObject();
        parentItemHealth.name = "GO_ITEM_ENERGY";
        parentItemHealth.transform.position = Vector3.zero;
        //item.amountEnergy
        for (int i = 0; i < mhv.vectors.Count-1; i++)
        {
            GameObject obj = (GameObject)Instantiate(item.energy);
            obj.transform.parent = parentItemHealth.transform;
            obj.name = item.energy.name + "_" + i.ToString("00");
            //obj.transform.localScale = new Vector3(1f,1f,1f);
            obj.GetComponent<scrItemControll>().itemID = i;
            obj.GetComponent<scrItemControll>().gameControll = GetComponent<scrGameControll>();
            obj.GetComponent<scrItemControll>().itemChild.SetActive(false);
            obj.transform.position = mhv.vectors[i];
            //obj.SetActive(false);
            item.poolEnergy.Add(obj);
        }

        //-------------------new level 01
        //METEOROS en base a la posicion de vectores
        GameObject parentMeteorV1= new GameObject();
        parentMeteorV1.name = "GO_METEORS_V1";
        parentMeteorV1.transform.position = Vector3.zero;

        for (int i = 0; i < mv.vectors.Count-1; i++)
        {
            GameObject obj = (GameObject)Instantiate(meteorV1.meteor);
            obj.transform.parent = parentMeteorV1.transform;
            obj.name = meteorV1.meteor.name + "_" + i.ToString("00");
            //obj.transform.localScale = new Vector3(1f, 1f, 1f);
            obj.GetComponent<scrMeteorControll>().meteorID = i;
            obj.GetComponent<scrMeteorControll>().gameControll = GetComponent<scrGameControll>();
            obj.GetComponent<scrMeteorControll>().mtrChild.SetActive(false);
            obj.transform.position = mv.vectors[i];
            //obj.SetActive(false);

            meteorV1.poolMeteor.Add(obj);
        }

    }

    public GameObject GetObjPoolTargetEnemy(){
        for (int i = 0; i < poolTargetEnemy.Count; i++)
        {
            if (!poolTargetEnemy[i].activeInHierarchy)
                return poolTargetEnemy[i];
        }

        GameObject obj = (GameObject)Instantiate(targetEnemy);
        obj.transform.parent = ParentTargetEnemy.transform;
        obj.SetActive(false);
        poolTargetEnemy.Add(obj);
        return obj;
    }

    public GameObject GetObjPoolMeteor(){
        for (int i = 0; i < meteorA.poolMeteor.Count; i++)
        {
            if (!meteorA.poolMeteor[i].activeInHierarchy)
                return meteorA.poolMeteor[i];
        }
        GameObject parent = GameObject.Find("GO_METEORS");
        GameObject obj = (GameObject)Instantiate(meteorA.meteor);
        obj.transform.parent = parent.transform;
//        obj.name = meteor.name + "_" + i.ToString("00");
        obj.transform.localScale = new Vector3(1f,1f,1f);
        obj.SetActive(false);
        meteorA.poolMeteor.Add(obj);
        return obj;
    }

    public GameObject GetObjPoolMeteorSide()
    {
        for (int i = 0; i < meteorSide.poolMeteor.Count; i++)
        {
            if (!meteorSide.poolMeteor[i].activeInHierarchy)
                return meteorSide.poolMeteor[i];
        }
        GameObject parent = GameObject.Find("GO_METEORS_SIDES");
        GameObject obj = (GameObject)Instantiate(meteorSide.meteor);
        obj.transform.parent = parent.transform;
        //        obj.name = meteor.name + "_" + i.ToString("00");
        obj.transform.localScale = new Vector3(1f, 1f, 1f);
        obj.SetActive(false);
        meteorSide.poolMeteor.Add(obj);
        return obj;
    }

    public GameObject GetObjPoolExplotion(){
        for (int i = 0; i < poolExplotionMeteor.Count; i++)
        {
            if (!poolExplotionMeteor[i].activeInHierarchy)
                return poolExplotionMeteor[i];
        }

        GameObject parent = GameObject.Find("GO_EXPLOTION_METEOR");
        GameObject obj = (GameObject)Instantiate(explotionMeteor);
        obj.transform.parent = parent.transform;
        //        obj.name = meteor.name + "_" + i.ToString("00");
        obj.transform.localScale = new Vector3(1f,1f,1f);
        obj.SetActive(false);
        poolExplotionMeteor.Add(obj);
        return obj;
    }

    public GameObject GetObjPoolItemEnergy(){
        for (int i = 0; i < item.poolEnergy.Count; i++)
        {
            if (!item.poolEnergy[i].activeInHierarchy)
                return item.poolEnergy[i];
        }
        GameObject parent = GameObject.Find("GO_ITEM_ENERGY");
        GameObject obj = (GameObject)Instantiate(item.energy);
        obj.transform.parent = parent.transform;
        //        obj.name = meteor.name + "_" + i.ToString("00");
        obj.transform.localScale = new Vector3(1f,1f,1f);
        obj.SetActive(false);
        item.poolEnergy.Add(obj);
        return obj;
    }


    //---- new level01

    public GameObject GetObjPoolMeteorV1()
    {
        for (int i = 0; i < meteorV1.poolMeteor.Count; i++)
        {
            if (!meteorV1.poolMeteor[i].activeInHierarchy)
                return meteorV1.poolMeteor[i];
        }
        GameObject parent = GameObject.Find("GO_METEORS_V1");
        GameObject obj = (GameObject)Instantiate(meteorV1.meteor);
        obj.transform.parent = parent.transform;
        obj.GetComponent<scrMeteorControll>().mtrChild.SetActive(false);
        //        obj.name = meteor.name + "_" + i.ToString("00");
        //obj.transform.localScale = new Vector3(1f, 1f, 1f);
        obj.SetActive(false);
        meteorV1.poolMeteor.Add(obj);
        return obj;
    }
    #endregion

    #region cortinas Pools

    IEnumerator WaveItemEnergyXY(){
        isHealthActive = true;
        //para meteoros a los lados
        int cont = 0;
        yield return new WaitForSeconds(1f);
        while (true)
        {
            for (int i = 0; i < item.amountEnergy; i++)
            {
                GameObject hazard = null;
                Vector3 spawnPosition = new Vector3(0, 0, 0);

                //posicion de los meteoros aleatoria
                spawnPosition = new Vector3(
                    Random.Range(-item.energyRange.x, item.energyRange.x)
                    , Random.Range(-item.energyRange.y, item.energyRange.y)
                    , player.transform.position.z + item.energyRange.z);

                hazard = GetObjPoolItemEnergy();

                hazard.GetComponent<Transform>().position = spawnPosition;
                hazard.GetComponent<scrItemControll>().speed = item.speedEnergy;
                hazard.GetComponent<scrItemControll>().amount = item.amountEnergy;
                hazard.transform.localScale = new Vector3(5, 5, 5);

                hazard.SetActive(true);

                cont++;
                //tiempo de salida de cada health
                yield return new WaitForSeconds(item.energyWait);

                if (cont > 5)
                    break;

            }

            //tiempo de espera para la siguiente wave
            yield return new WaitForSeconds(item.waveEnergyWait);
            if (cont > 3)
                break;
        }

        isHealthActive = false;
    }

    IEnumerator WaveMeteorXY(){
        //para meteoros a los lados
        //StartCoroutine(WaveMeteorXY_Sides_BIG());
        yield return new WaitForSeconds(1f);
        while (true)
        {
            for (int i = 0; i < meteorA.amountMeteor; i++)
            {
                GameObject hazard = null;
                Vector3 spawnPosition = new Vector3(0, 0, 0);
                float sc = 15f;
                //posicion de los meteoros aleatoria
                spawnPosition = new Vector3(
                    Random.Range(-meteorRange.x, meteorRange.x)
                    , Random.Range(-meteorRange.y, meteorRange.y)
                    , meteorRange.z);

                hazard = GetObjPoolMeteor();

                hazard.GetComponent<Transform>().position = spawnPosition;
                hazard.GetComponent<scrMeteorControll>().speedMeteor = meteorA.speedMeteor;
                hazard.transform.localScale = new Vector3(sc, sc, sc);

                hazard.SetActive(true);

                //tiempo de salida de cada meteoro
                yield return new WaitForSeconds(meteorA.meteorWait);

            }

            //tiempo de espera para la siguiente wave
            yield return new WaitForSeconds(meteorA.waveMeteorWait);
        }

    }

    IEnumerator WaveMeteorXY_Sides_BIG()
    {
        float sx = 44f;
        float sy = 30f;
        float lx, ly = 0f;
        float sc = 200f;
        Vector3[] side = new Vector3[12];
        Vector3 bdy = traBoundary.position;
        //I - 1
      
        lx = bdy.x - playerControll.playerRange.minRange.x - sx;
        ly = bdy.y + playerControll.playerRange.maxRange.y + sy;
        side[1] = new Vector3(lx,ly,0f);
        //I - 0
        lx = bdy.x - playerControll.playerRange.minRange.x - sx;
        ly = bdy.y;
        side[0] = new Vector3(lx, ly, 0f);
        //I - -1
        lx = bdy.x - playerControll.playerRange.minRange.x - sx;
        ly = bdy.y - playerControll.playerRange.minRange.y - sy;
        side[2] = new Vector3(lx, ly, 0f);

        //D 1
        lx = bdy.x + playerControll.playerRange.maxRange.x + sx;
        ly = bdy.y + playerControll.playerRange.maxRange.y + sy;
        side[4] = new Vector3(lx, ly, 0f);
        //D - 0
        lx = bdy.x + playerControll.playerRange.maxRange.x + sx;
        ly = bdy.y;
        side[3] = new Vector3(lx, ly, 0f);
        //D - -1
        lx = bdy.x + playerControll.playerRange.maxRange.x + sx;
        ly = bdy.y - playerControll.playerRange.minRange.y - sy;
        side[5] = new Vector3(lx, ly, 0f);

        //A 2
        lx = bdy.x + playerControll.playerRange.maxRange.x + (sx/2f);
        ly = bdy.y - playerControll.playerRange.maxRange.y + sy;
        side[7] = new Vector3(lx, ly, 0f);
        //A 1
        lx = bdy.x - playerControll.playerRange.minRange.x - (sx/2f);
        ly = bdy.y - playerControll.playerRange.maxRange.y + sy;
        side[6] = new Vector3(lx, ly, 0f);
        //AB 1
        lx = bdy.x - playerControll.playerRange.minRange.x + (sx/2f);
        ly = bdy.y - playerControll.playerRange.minRange.y - sy;
        side[8] = new Vector3(lx, ly, 0f);
        // AB 2
        lx = bdy.x + playerControll.playerRange.maxRange.x - (sx/2f);
        ly = bdy.y - playerControll.playerRange.minRange.y - sy;
        side[9] = new Vector3(lx, ly, 0f);

        //A 0
        lx = bdy.x;
        ly = bdy.y + playerControll.playerRange.maxRange.y + sy + 40f;
        side[10] = new Vector3(lx, ly, 0f);

        //AB 0
        lx = bdy.x;
        ly = bdy.y - playerControll.playerRange.minRange.y - sy - 40f;
        side[11] = new Vector3(lx, ly, 0f);
        //float waitLxy = 0.01f;

        yield return new WaitForSeconds(1f);
        while (true)
        {
            for (int i = 0; i < meteorSide.amountMeteor; i++)
            {
                GameObject hazard = null;
                Vector3 spawnPosition = new Vector3(0, 0, 0);

                //I 1
                spawnPosition = new Vector3(side[1].x, side[1].y, meteorRange.z);
                hazard = GetObjPoolMeteorSide();
                hazard.GetComponent<Transform>().position = spawnPosition;
                hazard.GetComponent<scrMeteorControll>().speedMeteor = meteorSide.speedMeteor;
                hazard.transform.localScale = new Vector3(sc, sc, sc);
                hazard.SetActive(true);

                // A 2
                spawnPosition = new Vector3(side[7].x, side[7].y, meteorRange.z);
                hazard = GetObjPoolMeteorSide();
                hazard.GetComponent<Transform>().position = spawnPosition;
                hazard.GetComponent<scrMeteorControll>().speedMeteor = meteorSide.speedMeteor;
                hazard.transform.localScale = new Vector3(sc, sc, sc);
                hazard.SetActive(true);

                yield return new WaitForSeconds(meteorSide.meteorWait);
                //D 0
                spawnPosition = new Vector3(side[3].x, side[3].y, meteorRange.z);
                hazard = GetObjPoolMeteorSide();
                hazard.GetComponent<Transform>().position = spawnPosition;
                hazard.GetComponent<scrMeteorControll>().speedMeteor = meteorSide.speedMeteor;
                hazard.transform.localScale = new Vector3(sc, sc, sc);
                hazard.SetActive(true);

                // AB 1
                spawnPosition = new Vector3(side[8].x, side[8].y, meteorRange.z);
                hazard = GetObjPoolMeteorSide();
                hazard.GetComponent<Transform>().position = spawnPosition;
                hazard.GetComponent<scrMeteorControll>().speedMeteor = meteorSide.speedMeteor;
                hazard.transform.localScale = new Vector3(sc, sc, sc);
                hazard.SetActive(true);

                // A 0
                spawnPosition = new Vector3(side[10].x, side[10].y, meteorRange.z);
                hazard = GetObjPoolMeteorSide();
                hazard.GetComponent<Transform>().position = spawnPosition;
                hazard.GetComponent<scrMeteorControll>().speedMeteor = meteorSide.speedMeteor;
                hazard.transform.localScale = new Vector3(sc, sc, sc);
                hazard.SetActive(true);

                yield return new WaitForSeconds(meteorSide.meteorWait);

                //I 2
                spawnPosition = new Vector3(side[2].x, side[2].y, meteorRange.z);
                hazard = GetObjPoolMeteorSide();
                hazard.GetComponent<Transform>().position = spawnPosition;
                hazard.GetComponent<scrMeteorControll>().speedMeteor = meteorSide.speedMeteor;
                hazard.transform.localScale = new Vector3(sc, sc, sc);
                hazard.SetActive(true);

                // A1
                spawnPosition = new Vector3(side[6].x, side[6].y, meteorRange.z);
                hazard = GetObjPoolMeteorSide();
                hazard.GetComponent<Transform>().position = spawnPosition;
                hazard.GetComponent<scrMeteorControll>().speedMeteor = meteorSide.speedMeteor;
                hazard.transform.localScale = new Vector3(sc, sc, sc);
                hazard.SetActive(true);

                // AB 0
                spawnPosition = new Vector3(side[11].x, side[11].y, meteorRange.z);
                hazard = GetObjPoolMeteorSide();
                hazard.GetComponent<Transform>().position = spawnPosition;
                hazard.GetComponent<scrMeteorControll>().speedMeteor = meteorSide.speedMeteor;
                hazard.transform.localScale = new Vector3(sc, sc, sc);
                hazard.SetActive(true);

                yield return new WaitForSeconds(meteorSide.meteorWait);

                //D 1
                spawnPosition = new Vector3(side[4].x, side[4].y, meteorRange.z);
                hazard = GetObjPoolMeteorSide();
                hazard.GetComponent<Transform>().position = spawnPosition;
                hazard.GetComponent<scrMeteorControll>().speedMeteor = meteorSide.speedMeteor;
                hazard.transform.localScale = new Vector3(sc, sc, sc);
                hazard.SetActive(true);

                // AB 2
                spawnPosition = new Vector3(side[9].x, side[9].y, meteorRange.z);
                hazard = GetObjPoolMeteorSide();
                hazard.GetComponent<Transform>().position = spawnPosition;
                hazard.GetComponent<scrMeteorControll>().speedMeteor = meteorSide.speedMeteor;
                hazard.transform.localScale = new Vector3(sc, sc, sc);
                hazard.SetActive(true);

                yield return new WaitForSeconds(meteorSide.meteorWait);

                //I 0
                spawnPosition = new Vector3(side[0].x, side[0].y, meteorRange.z);
                hazard = GetObjPoolMeteorSide();
                hazard.GetComponent<Transform>().position = spawnPosition;
                hazard.GetComponent<scrMeteorControll>().speedMeteor = meteorSide.speedMeteor;
                hazard.transform.localScale = new Vector3(sc, sc, sc);
                hazard.SetActive(true);
                yield return new WaitForSeconds(meteorSide.meteorWait);

                //D 2
                spawnPosition = new Vector3(side[5].x, side[5].y, meteorRange.z);
                hazard = GetObjPoolMeteorSide();
                hazard.GetComponent<Transform>().position = spawnPosition;
                hazard.GetComponent<scrMeteorControll>().speedMeteor = meteorSide.speedMeteor;
                hazard.transform.localScale = new Vector3(sc, sc, sc);
                hazard.SetActive(true);

                //tiempo de salida de cada meteoro por ola
                yield return new WaitForSeconds(meteorSide.meteorWait);

            }

            //tiempo de espera para la siguiente wave
            yield return new WaitForSeconds(meteorSide.waveMeteorWait);
        }

    }

    // cantidad alta de meteoros y radpidos en escala mediana
    IEnumerator WaveMeteorXY_Sides(){
        float sc = 100f;
        Vector3[] side = new Vector3[16];
        //izq
        side[0] = new Vector3(-meteorRange.x - 20f, -meteorRange.y - 10f, 0f);
        side[1] = new Vector3(-meteorRange.x - 20f, meteorRange.y + 10f, 0f);
        //dere
        side[2] = new Vector3(meteorRange.x + 20f, -meteorRange.y - 10f, 0f);
        side[3] = new Vector3(meteorRange.x + 20f, meteorRange.y + 10f, 0f);
        //arriba
        side[4] = new Vector3(-meteorRange.x - 20f, meteorRange.y + 16f, 0f);
        side[5] = new Vector3(meteorRange.x + 20f, meteorRange.y + 16f, 0f);
        //abajo
        side[6] = new Vector3(-meteorRange.x - 20f, -meteorRange.y - 16f, 0f);
        side[7] = new Vector3(meteorRange.x + 20f, -meteorRange.y - 16f, 0f);
        //izq 2
        side[8] = new Vector3(-meteorRange.x - 60f, -meteorRange.y - 30f, 0f);
        side[9] = new Vector3(-meteorRange.x - 60f, meteorRange.y + 30f, 0f);
        //dere 2
        side[10] = new Vector3(meteorRange.x + 60f, -meteorRange.y - 30f, 0f);
        side[11] = new Vector3(meteorRange.x + 60f, meteorRange.y + 30f, 0f);
        //arriba 2
        side[12] = new Vector3(-meteorRange.x - 40f, meteorRange.y + 40f, 0f);
        side[13] = new Vector3(meteorRange.x + 40f, meteorRange.y + 40f, 0f);
        //abajo 2
        side[14] = new Vector3(-meteorRange.x - 40f, -meteorRange.y - 40f, 0f);
        side[15] = new Vector3(meteorRange.x + 40f, -meteorRange.y - 40f, 0f);

        yield return new WaitForSeconds(1f);
        while (true)
        {
            for (int i = 0; i < meteorSide.amountMeteor; i++)
            {
                GameObject hazard = null;
                Vector3 spawnPosition = new Vector3(0, 0, 0);

                //lado izq
                spawnPosition = new Vector3(
                    Random.Range(side[0].x, side[1].x)
                    , Random.Range(side[0].y, side[1].y)
                    , meteorRange.z);

                hazard = GetObjPoolMeteorSide();

                hazard.GetComponent<Transform>().position = spawnPosition;
                hazard.GetComponent<scrMeteorControll>().speedMeteor = meteorSide.speedMeteor;
                hazard.transform.localScale = new Vector3(sc, sc, sc);

                hazard.SetActive(true);

                //lado derecho
                spawnPosition = new Vector3(
                    Random.Range(side[2].x, side[3].x)
                    , Random.Range(side[2].y, side[3].y)
                    , meteorRange.z);

                hazard = GetObjPoolMeteorSide();

                hazard.GetComponent<Transform>().position = spawnPosition;
                hazard.GetComponent<scrMeteorControll>().speedMeteor = meteorSide.speedMeteor;
                hazard.transform.localScale = new Vector3(sc, sc, sc);

                hazard.SetActive(true);

                //lado arriba
                spawnPosition = new Vector3(
                    Random.Range(side[4].x, side[5].x)
                    , Random.Range(side[4].y, side[5].y)
                    , meteorRange.z);

                hazard = GetObjPoolMeteorSide();

                hazard.GetComponent<Transform>().position = spawnPosition;
                hazard.GetComponent<scrMeteorControll>().speedMeteor = meteorSide.speedMeteor;
                hazard.transform.localScale = new Vector3(sc, sc, sc);

                hazard.SetActive(true);

                //lado abajo
                spawnPosition = new Vector3(
                    Random.Range(side[6].x, side[7].x)
                    , Random.Range(side[6].y, side[7].y)
                    , meteorRange.z);

                hazard = GetObjPoolMeteorSide();

                hazard.GetComponent<Transform>().position = spawnPosition;
                hazard.GetComponent<scrMeteorControll>().speedMeteor = meteorSide.speedMeteor;
                hazard.transform.localScale = new Vector3(sc, sc, sc);

                hazard.SetActive(true);

                //tiempo de salida de cada meteoro por ola
                //yield return new WaitForSeconds(meteorWait + 0.01f);
                //2da matriz

                //lado izq
                spawnPosition = new Vector3(
                    Random.Range(side[8].x, side[9].x)
                    , Random.Range(side[8].y, side[9].y)
                    , meteorRange.z);

                hazard = GetObjPoolMeteorSide();

                hazard.GetComponent<Transform>().position = spawnPosition;
                hazard.GetComponent<scrMeteorControll>().speedMeteor = meteorSide.speedMeteor;
                hazard.transform.localScale = new Vector3(sc, sc, sc);

                hazard.SetActive(true);
                //lado derecho
                spawnPosition = new Vector3(
                    Random.Range(side[10].x, side[11].x)
                    , Random.Range(side[10].y, side[11].y)
                    , meteorRange.z);

                hazard = GetObjPoolMeteorSide();

                hazard.GetComponent<Transform>().position = spawnPosition;
                hazard.GetComponent<scrMeteorControll>().speedMeteor = meteorSide.speedMeteor;
                hazard.transform.localScale = new Vector3(sc, sc, sc);

                hazard.SetActive(true);

                //lado arriba
                spawnPosition = new Vector3(
                    Random.Range(side[12].x, side[13].x)
                    , Random.Range(side[12].y, side[13].y)
                    , meteorRange.z);

                hazard = GetObjPoolMeteorSide();

                hazard.GetComponent<Transform>().position = spawnPosition;
                hazard.GetComponent<scrMeteorControll>().speedMeteor = meteorSide.speedMeteor;
                hazard.transform.localScale = new Vector3(sc, sc, sc);

                hazard.SetActive(true);

                //lado abajo
                spawnPosition = new Vector3(
                    Random.Range(side[14].x, side[15].x)
                    , Random.Range(side[14].y, side[15].y)
                    , meteorRange.z);

                hazard = GetObjPoolMeteorSide();

                hazard.GetComponent<Transform>().position = spawnPosition;
                hazard.GetComponent<scrMeteorControll>().speedMeteor = meteorSide.speedMeteor;
                hazard.transform.localScale = new Vector3(sc, sc, sc);

                hazard.SetActive(true);


                //tiempo de salida de cada meteoro por ola
                yield return new WaitForSeconds(meteorSide.meteorWait);

            }

            //tiempo de espera para la siguiente wave
            yield return new WaitForSeconds(meteorSide.waveMeteorWait);
        }

    }


    //-- new level 01

    IEnumerator WaveMeteorV1()
    {
        //para meteoros a los lados
        //StartCoroutine(WaveMeteorXY_Sides_BIG());
        //yield return new WaitForSeconds(meteorV1.waveMeteorWait);
        yield return null;



        while (true)
        {
            for (int i = 0; i < meteorV1.amountMeteor; i++)
            {
                GameObject hazard = null;
                Vector3 spawnPosition = new Vector3(0, 0, 0);
                //float sc = 15f;
                //posicion de los meteoros aleatoria
                //spawnPosition = new Vector3(
                //    Random.Range(-meteorRange.x, meteorRange.x)
                //    , Random.Range(-meteorRange.y, meteorRange.y)
                //    , meteorRange.z);

                spawnPosition = mv.vectors[i];

                hazard = GetObjPoolMeteorV1();

                hazard.GetComponent<Transform>().position = spawnPosition;
                hazard.GetComponent<scrMeteorControll>().speedMeteor = meteorA.speedMeteor;

                //hazard.transform.localScale = new Vector3(sc, sc, sc);

                hazard.SetActive(true);

                //tiempo de salida de cada meteoro
                yield return new WaitForSeconds(meteorV1.meteorWait);
                

            }

            //tiempo de espera para la siguiente wave
            //yield return new WaitForSeconds(meteorV1.waveMeteorWait);
            yield return new WaitForSeconds(1f);
            break;
        }

    }
    #endregion

    #region TouchMeteor

    public void SetTargetEnemy(RaycastHit hit){
        
        GameObject obj = GetObjPoolTargetEnemy();
        scrTargetEnemyControll t = obj.GetComponent<scrTargetEnemyControll>();

        t.mainCamera = mainCamera;
        t.target = hit.transform;
        t.ParentTargetEnemy = ParentTargetEnemy;

        obj.SetActive(true);
        scrMeteorControll m = hit.collider.gameObject.GetComponent<scrMeteorControll>();

        /*
        Vector2 viewPortPos = mainCamera.WorldToViewportPoint(hit.transform.position);
        Vector2 screenPos = new Vector2(
            ((viewPortPos.x * recTargetEnemy.sizeDelta.x) - (recTargetEnemy.sizeDelta.x * 0.5f)),
            ((viewPortPos.y * recTargetEnemy.sizeDelta.y) - (recTargetEnemy.sizeDelta.y * 0.5f)));
        //recCrossHairA.anchoredPosition = screenPos;
        GameObject obj = GetObjPoolTargetEnemy();
        obj.GetComponent<RectTransform>().anchoredPosition = screenPos;
        obj.SetActive(true);
        */
    }

    #endregion
}

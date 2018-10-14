using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager uiManagerInstance;

    public RectTransform rtCrossHairParent;
    public RectTransform rtCrossHair;
    public RectTransform rtCrossHairB;

    public LeftJoystick leftJoystick; // the game object containing the LeftJoystick script
    public RightJoystick rightJoystick; // the game object containing the RightJoystick script

    [SerializeField]
    private Color crossHairColor00;
    [SerializeField]
    private Color crossHairColor01;
    [SerializeField]
    private Color crossHairColor02;

    public RectTransform rtPointParent;

    [SerializeField]
    private GameObject pointHolder;
    [SerializeField]
    private Color negativePoints;
    [SerializeField]
    private Color positivePoints;

    public Text txtPlayerEnergy;

    public Text txtPlayerShield;

    [SerializeField]
    private GameObject goMenu;
    [HideInInspector]
    public PlayerMenu playerMenu;

    [SerializeField]
    private GameObject goJoyStick;

    [SerializeField]
    private GameObject goBars;

    [SerializeField]
    private GameObject goCrossHairPoints;

    [SerializeField]
    private GameObject goScoreManager;
    [HideInInspector]
    public ScoreManager scoreManager;

    [SerializeField]
    private GameObject goFade;
    private Animator animaFade;

    //[SerializeField]
    private float lifeTime = 0.45f;

    private Image imgCrossHair;
    private Image imgCrossHairB;
    private GameObject goCrossHairB;

	void Start () {
		if(uiManagerInstance == null)
        {
            uiManagerInstance = this;
        }
        else
        {

            Destroy(gameObject);
        }

        //Sets this to not be destroyed when reloading scene
        //DontDestroyOnLoad(gameObject);

        DefaultConfiguration();
    }
	

	void Update () {
		
	}

    private void DefaultConfiguration()
    {
        imgCrossHair = rtCrossHair.GetComponent<Image>();
        imgCrossHairB = rtCrossHairB.GetComponent<Image>();

        goCrossHairB = rtCrossHairB.gameObject;

        //MENU
        playerMenu = goMenu.GetComponent<PlayerMenu>();

        if (!goMenu.activeInHierarchy)
            goMenu.SetActive(true);
        playerMenu.ShowMenuButton(true);
        playerMenu.ShowMenuPanel(false);
        playerMenu.ShowMenuQuestion(false);

        if (!goJoyStick.activeInHierarchy)
            goJoyStick.SetActive(true);

        if (!goBars.activeInHierarchy)
            goBars.SetActive(true);

        if (!goCrossHairPoints.activeInHierarchy)
            goCrossHairPoints.SetActive(true);

        if (!goScoreManager.activeInHierarchy)
        {
            goScoreManager.SetActive(true);
        }
        scoreManager = goScoreManager.GetComponent<ScoreManager>();
        
        scoreManager.ShowScoreList(false);

        if (!goFade.activeInHierarchy)
        {
            goFade.SetActive(true);
           
        }
        
        animaFade = goFade.GetComponent<Animator>();
        FadeAnimationIN();


    }

    public void SetCrossHairColor00()
    {
        imgCrossHair.color = crossHairColor00;

    }

    public void SetCrossHairColor01()
    {
        imgCrossHair.color = crossHairColor01;

    }

    public void SetCrossHairColor02()
    {
        imgCrossHair.color = crossHairColor02;

    }

    public void SetCrossHairBColor00()
    {
        imgCrossHairB.color = crossHairColor00;

    }

    public void SetCrossHairBColor01()
    {
        imgCrossHairB.color = crossHairColor01;

    }

    public void SetCrossHairBColor02()
    {
        imgCrossHairB.color = crossHairColor02;

    }

    public void ShowCrossHairB(bool b) {
        goCrossHairB.SetActive(b);
    }

    public void ShowNegativePoints(int values, Vector3 pos) {
        GameObject go = Instantiate(pointHolder, Vector3.zero, Quaternion.identity);
        go.transform.SetParent(rtPointParent.transform);
        Vector2 viewPortPosA = CharacterManager.characterManagerInstance.mainCamera.WorldToViewportPoint(pos);

        Vector2 screenPos = new Vector2(
            ((viewPortPosA.x * rtPointParent.sizeDelta.x) - (rtPointParent.sizeDelta.x * 0.5f)),
            ((viewPortPosA.y * rtPointParent.sizeDelta.y) - (rtPointParent.sizeDelta.y * 0.5f)));

        go.GetComponent<RectTransform>().anchoredPosition = screenPos;
        go.transform.localScale = Vector3.one;
        go.SetActive(true);

        //asigno los valores al texto
        go.GetComponent<Points>().StartAnimation("-" + values, negativePoints, lifeTime);

    }

    public void ShowPositivePoints(int values, Vector3 pos)
    {
        GameObject go = Instantiate(pointHolder, Vector3.zero, Quaternion.identity);
        go.transform.SetParent(rtPointParent.transform);
        Vector2 viewPortPosA = CharacterManager.characterManagerInstance.mainCamera.WorldToViewportPoint(pos);

        Vector2 screenPos = new Vector2(
            ((viewPortPosA.x * rtPointParent.sizeDelta.x) - (rtPointParent.sizeDelta.x * 0.5f)),
            ((viewPortPosA.y * rtPointParent.sizeDelta.y) - (rtPointParent.sizeDelta.y * 0.5f)));

        go.GetComponent<RectTransform>().anchoredPosition = screenPos;
        go.transform.localScale = Vector3.one;
        go.SetActive(true);
        //asigno los valores al texto
        //asigno los valores al texto
        go.GetComponent<Points>().StartAnimation("+" + values, positivePoints, lifeTime);

    }

    public void FadeAnimationIN()
    {
        animaFade.SetTrigger("FadeIN");
    }

    public void FadeAnimationOUT()
    {
        animaFade.SetTrigger("FadeOUT");
    }

    public void ShowScoreCoreCanvas(bool b)
    {
        goScoreManager.SetActive(b);
    }

    public void ShowCrossHairCanvas(bool b)
    {
        goCrossHairPoints.SetActive(b);
    }

    public void ShowBars(bool b)
    {
        goBars.SetActive(b);
    }

    public void ShowJoyStickCanvas(bool b)
    {
        goJoyStick.SetActive(b);
    }

    public void ShowMenuCanvas(bool b)
    {
        goMenu.SetActive(b);
    }
}

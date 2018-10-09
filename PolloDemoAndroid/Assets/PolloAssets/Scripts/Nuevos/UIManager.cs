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
    //[SerializeField]
    private float lifeTime = 0.45f;

    private Image imgCrossHair;
    private Image imgCrossHairB;
    private GameObject goCrossHairB;
	// Use this for initialization
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
        DontDestroyOnLoad(gameObject);

        imgCrossHair = rtCrossHair.GetComponent<Image>();
        imgCrossHairB = rtCrossHairB.GetComponent<Image>();

        goCrossHairB = rtCrossHairB.gameObject;
    }
	
	// Update is called once per frame
	void Update () {
		
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

        //asigno los valores al texto
        //asigno los valores al texto
        go.GetComponent<Points>().StartAnimation("+" + values, positivePoints, lifeTime);

    }
}

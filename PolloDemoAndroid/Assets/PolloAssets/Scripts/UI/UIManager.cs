using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager uiLevelsInstance;
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

    private Image imgCrossHair;
	// Use this for initialization
	void Start () {
		if(uiLevelsInstance == null)
        {
            uiLevelsInstance = this;
        }
        else
        {

            Destroy(gameObject);
        }

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        imgCrossHair = rtCrossHair.GetComponent<Image>();
        
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
}

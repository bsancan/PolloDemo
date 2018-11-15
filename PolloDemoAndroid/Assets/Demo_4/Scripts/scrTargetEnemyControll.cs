using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrTargetEnemyControll : MonoBehaviour {
    public GameObject ParentTargetEnemy;
    public Transform target;
    public Camera mainCamera;
    private RectTransform recTargetEnemy;

    void Awake(){
        recTargetEnemy = ParentTargetEnemy.GetComponent<RectTransform>();
    }

	void Start () {
		
	}
	
	
	void Update () {
        if (gameObject.activeInHierarchy)
        {
            if (!target.gameObject.activeInHierarchy)
            {
                gameObject.SetActive(false);
            }

            Vector2 viewPortPos = mainCamera.WorldToViewportPoint(target.position);
            Vector2 screenPos = new Vector2(
                ((viewPortPos.x * recTargetEnemy.sizeDelta.x) - (recTargetEnemy.sizeDelta.x * 0.5f)),
                ((viewPortPos.y * recTargetEnemy.sizeDelta.y) - (recTargetEnemy.sizeDelta.y * 0.5f)));
            transform.GetComponent<RectTransform>().anchoredPosition = screenPos;


            //recCrossHairA.anchoredPosition = screenPos;
            //GameObject obj = GetObjPoolTargetEnemy();
            //obj.GetComponent<RectTransform>().anchoredPosition = screenPos;
            //obj.SetActive(true);


        }


        //transform.position = target.position;	
	}



}

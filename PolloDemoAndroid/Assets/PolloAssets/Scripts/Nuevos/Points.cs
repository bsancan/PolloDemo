using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Points : MonoBehaviour
{
 
   // public RectTransform rtPoints;
    [SerializeField]
    private Text txtPoints;
    [SerializeField]
    private Animator animator;
    //[SerializeField]
    //private float lifeTime;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void StartAnimation(string value, Color colortext, float lifeTime) {
        txtPoints.text = value;
        txtPoints.color = colortext;
        animator.SetTrigger("UP");
        CancelInvoke();
        Invoke("Die", lifeTime);
    }

    void Die()
    {
        CancelInvoke();
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }
}

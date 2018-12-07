using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAmmo : MonoBehaviour
{
    public float speed = 2f;
    public float lifeTime = 2f;
    public int valueDamage = 10;
    public float speedRotation = 1f;
    //1- laser      2- spera
    public int type = 0;

    void Start()
    {

    }

    private void OnEnable()
    {
        //if(type == 1)
        //{
        //    StartCoroutine(CorLifeTime());
        //}
        //else if(type ==2)
        //{
        //    StartCoroutine(CorFollowPlayer());
        //}

    }

    // Update is called once per frame
    void Update()
    {
        if(type == 1)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        else if (type == 2)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
            
        }

    }

    public void AmmoActived()
    {
        //1 = laser en linea recta
        //2 = sphere qie sigue al player
        if (type == 1)
        {
            StartCoroutine(CorLifeTime());
        }
        else if (type == 2)
        {
            StartCoroutine(CorFollowPlayer());
        }
    }

    IEnumerator CorLifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
        //Destroy(gameObject);

    }

    IEnumerator CorFollowPlayer()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.1f);
            transform.rotation = Quaternion.Lerp(transform.rotation, CharacterManager.characterManagerInstance.character.transform.rotation, speedRotation * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAmmo") && type == 2)
        {
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}

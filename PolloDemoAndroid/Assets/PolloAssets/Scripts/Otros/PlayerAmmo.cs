using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAmmo : MonoBehaviour {

    public float speed = 2f;
    public float lifeTime = 2f;

	void Start () {

	}

    private void OnEnable()
    {
        StartCoroutine(CorLifeTime());
    }

    // Update is called once per frame
    void Update () {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    IEnumerator CorLifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);

    }
}

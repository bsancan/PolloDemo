using UnityEngine;
using System.Collections;

public class TriggerSpawnEnemies : MonoBehaviour {
    public GameObject[] ships;

    public GameObject[] spawnSides;

    public GameObject misils;

    public GameObject Izq;
    public GameObject Der;
	
	void Awake () {
        for (int i = 0; i < spawnSides.Length; i++)
        {
            spawnSides[i].GetComponent<SpawnMisil>().misil = misils;
        }
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}

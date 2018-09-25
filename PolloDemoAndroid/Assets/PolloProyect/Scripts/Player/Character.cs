using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Mainly used as a data container to define a character. This script is attached to the prefab
/// (found in the Bundles/Characters folder) and is to define all data related to the character.
/// </summary>
public class Character : MonoBehaviour {

    public string characterName;
    public Animator animator;

    public Transform characterModel;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

 
}

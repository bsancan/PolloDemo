using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthControl : MonoBehaviour {

	//HEALTH
	public int startingHealth;                           // The amount of health the player starts the game with.
	public int currentHealth;                                   // The current health the player has.
	public Slider healthSlider;                                 // Reference to the UI's health bar.
	public Image damageImage;  
	public Color flashColour = new Color(1f, 0f, 0f, 0.6f);     // The colour the damageImage is set to, to flash.
	public float flashSpeed;                               // The speed the damageImage will fade at.
	bool damaged;

	//GameOver
	public CanvasGroup cnvGameOver;

	void Start () {
		healthSlider.maxValue = startingHealth;
		healthSlider.value = startingHealth;
		currentHealth = startingHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if (damaged) {
			damageImage.color = flashColour;
		} else {
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		damaged = false;
	}

	public void TakeDamage (int amount){
		damaged = true;
		currentHealth -= amount;
		healthSlider.value = currentHealth;

		if (currentHealth == 0) {
			//
			StartCoroutine(ShowGameOver());
		}

	}

	public void TimeOver(){
		StartCoroutine(ShowGameOver());
	}

	IEnumerator ShowGameOver(){
		GetComponent<GameController> ().GameOver ();
		yield return new WaitForSeconds (2f);
		cnvGameOver.alpha = 1f;
	}




}

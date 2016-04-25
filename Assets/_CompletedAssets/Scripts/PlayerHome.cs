using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHome : MonoBehaviour {
	public int homeHealth = 500;
	public int currentHomeHealth;
	public Slider homeHealthSlider;  // Home health slider for the UI

	void Start () {
		currentHomeHealth = homeHealth;
	}
	
	// Update is called once per frame
	void Update () {	
	}

	public void DamageHome(int loss) {
		currentHomeHealth -= loss;
		homeHealthSlider.value = currentHomeHealth;

		if (currentHomeHealth <= 0) {
			// GAMEOVER
			// Based on PlayerHealth.cs, nothing needs to be done here.
		}
	}
}

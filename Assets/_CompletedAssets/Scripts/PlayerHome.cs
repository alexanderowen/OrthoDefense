using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHome : MonoBehaviour {
	public int homeHealth = 500;
	public int currentHomeHealth;
	public Image damageImage;
	public Slider homeHealthSlider;  // Home health slider for the UI
	public float flashSpeed = 5f;
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

	bool damaged;

	void Start () {
		currentHomeHealth = homeHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if(damaged)
		{
			damageImage.color = flashColour;
		}
		else
		{
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		damaged = false;	
	}

	public void DamageHome(int loss) {
		currentHomeHealth -= loss;
		homeHealthSlider.value = currentHomeHealth;
		damaged = true;

		if (currentHomeHealth <= 0) {
			// GAMEOVER
			// Based on PlayerHealth.cs, nothing needs to be done here.
		}
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TurretHealth : MonoBehaviour {
	public int maxhealth;
	public Slider towerHealthSlider;
	public int currenthealth;

	// Use this for initialization
	void Start () {
		currenthealth = maxhealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void DamageTower(int loss) {
		currenthealth -= loss;
		towerHealthSlider.value = currenthealth;

		if (currenthealth <= 0) {
			Destroy (this.gameObject);
		}
	}
}

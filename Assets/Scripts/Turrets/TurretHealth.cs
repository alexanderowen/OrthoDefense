using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TurretHealth : MonoBehaviour {
	public int maxhealth;
	public Slider towerHealthSlider;

	private int currenthealth;

	// Use this for initialization
	void Start () {
		
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

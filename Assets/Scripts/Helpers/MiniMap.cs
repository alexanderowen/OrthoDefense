using UnityEngine;
using System.Collections;

public class MiniMap : MonoBehaviour {

	public Light _light;

	bool DisplayOn;
	public GameObject player;
	public GameObject house;

	void Start () {
		transform.position = house.transform.position;
		DisplayOn = false;
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			DisplayOn = !DisplayOn;
			if (DisplayOn) {
				GetComponent<Camera> ().enabled = true;
			} else {
				GetComponent<Camera> ().enabled = false;
			}
		}
	}


	void OnPreCull ()
	{
		_light.enabled = false;
	}

	void OnPreRender() {
		_light.enabled = false;
	}
	void OnPostRender() {
		_light.enabled = true;
	}
}

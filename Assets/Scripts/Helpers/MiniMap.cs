using UnityEngine;
using System.Collections;

public class MiniMap : MonoBehaviour {

	public Light _light;

	bool onPlayer;
	public GameObject player;
	public GameObject house;

	void Start () {
		onPlayer = true;
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			onPlayer = !onPlayer;
		}

		if (onPlayer) {
			transform.position = player.transform.position;
			transform.position = player.transform.position;
		} else {
			transform.position = house.transform.position;
			transform.position = house.transform.position;
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

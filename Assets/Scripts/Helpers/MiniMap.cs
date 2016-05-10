using UnityEngine;
using System.Collections;

public class MiniMap : MonoBehaviour {

	public Light light;

	bool onPlayer;
	public GameObject player;
	public GameObject house;

	void Start () {
		onPlayer = true;
	}

	void Update ()
	{
		if (Input.GetKeyDown("space"))
            onPlayer = !onPlayer;


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
		light.enabled = false;
	}

	void OnPreRender() {
		light.enabled = false;
	}
	void OnPostRender() {
		light.enabled = true;
	}
}

using UnityEngine;
using System.Collections;

public class MapFollow : MonoBehaviour {
	bool onPlayer;
	public GameObject player;
	GameObject camera;

	void Start () {
		onPlayer = true;
		//camera = GetComponent <Camera>();
	}

	void Update () {
		transform.position = player.transform.position;
		transform.position = player.transform.position;
	}
	
}

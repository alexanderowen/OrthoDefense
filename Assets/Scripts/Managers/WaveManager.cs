using UnityEngine;
using System.Collections;

//The goal of this script is so that when you attach it to the scene's level manager, it would count down the number of spawns to simulate a wave of enemies, and end the level when the player successfully survives.

public class WaveManager: MonoBehaviour {
	public int spawnCount;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (spawnCount < 0) {
			Destroy (gameObject);
		}
	}

	public void spawnDecrementer(){
		spawnCount--;
	}
	public int getSpawnCount (){
		return spawnCount;
	}
}

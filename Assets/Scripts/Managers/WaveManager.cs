using UnityEngine;
using System.Collections;

/*
The goal of this script is so that when you attach it to 
the scene's level manager, it would count down the number 
of spawns to simulate a wave of enemies, and end the level when the player successfully survives.
*/
public class WaveManager: MonoBehaviour {
	public int spawnCount;
	public bool isSpawning;
	private GameManager GM;

	void Start () {
		isSpawning = true;
		GM = GameObject.Find ("GameManager").GetComponent<GameManager> ();
	}
		
	void Update () {
		if (!isSpawning && GameObject.FindWithTag ("Enemy") == null && GameObject.FindWithTag("InvisEnemy") == null) {
			GM.LevelComplete ();
			Destroy (gameObject);
		}
	}

	public void spawnDecrementer(){
		spawnCount--;
		if (spawnCount <= 0) {
			isSpawning = false;
		}
	}

	public int getSpawnCount (){
		return spawnCount;
	}
}

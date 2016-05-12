using UnityEngine;

public class BossSpawnManager : MonoBehaviour
{
	public PlayerHealth playerHealth;       // Reference to the player's heatlh.
	public GameObject enemy;                // The enemy prefab to be spawned.
	public Transform spawnPoint;         // An array of the spawn points this enemy can spawn from.
	bool isBossReleased;
	WaveManager waveManager;

	void Start ()
	{       
		waveManager = GetComponent<WaveManager>();
		isBossReleased = false;
	}

	void Update ()
	{
		if (waveManager.getSpawnCount () < 1 && !isBossReleased) {
			isBossReleased = true;
			// Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
			Instantiate (enemy, spawnPoint.position, spawnPoint.rotation);
		}
	}
}

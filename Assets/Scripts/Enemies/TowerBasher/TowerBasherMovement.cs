using UnityEngine;
using System.Collections;


public class TowerBasherMovement : MonoBehaviour
{
	GameObject turret;             // Reference to the turret's position.
	TurretHealth turretHealth;      	// Reference to the turret's health.
	EnemyHealth enemyHealth;        // Reference to this enemy's health.
	NavMeshAgent nav;               // Reference to the nav mesh agent.
	Transform player;				// Reference to the player's position.
	PlayerHealth playerHealth;      // Reference to the player's health.
	bool playerLockOn;				// Is the enemy locked onto the player?
	bool anyTowersLeft;
	GameObject[] turrets;

	void Awake ()
	{
		// Set up the references.
		anyTowersLeft = false;
		if (GameObject.FindWithTag ("Turret") != null) {
			turrets = GameObject.FindGameObjectsWithTag ("Turret");
			turret = turrets [0];
			turretHealth = turret.GetComponent <TurretHealth> ();
			anyTowersLeft = true;
		}
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerHealth = player.GetComponent <PlayerHealth> ();
		enemyHealth = GetComponent <EnemyHealth> ();
		nav = GetComponent <NavMeshAgent> ();
	}

	void Update ()
	{
		if (!anyTowersLeft) {
			playerLockOn = true;
		}

		if (anyTowersLeft) {
			// If the enemy and the player have health left...
			if (enemyHealth.currentHealth > 0 && turretHealth.currenthealth > 0) {
				// ... set the destination of the nav mesh agent to the base.
				nav.SetDestination (turret.transform.position);
			}
		} else if (playerLockOn && enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0) {
			nav.SetDestination (player.transform.position);
		}
		// Otherwise...
		else
		{
			// ... disable the nav mesh agent.
			nav.enabled = false;
		}
	}

	public bool isPlayerLockedOn(){
		return playerLockOn;
	}
}

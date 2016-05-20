using UnityEngine;
using System.Collections;


public class TowerBasherAttack : MonoBehaviour
{
	public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
	public int attackDamage = 10;               // The amount of health taken away per attack.


	Animator anim;                              // Reference to the animator component.
	GameObject turret;                          // Reference to the turret GameObject.
	TurretHealth turretHealth;                  // Reference to the turret's health.
	EnemyHealth enemyHealth;                    // Reference to this enemy's health.
	GameObject player;                          // Reference to the player GameObject.
	PlayerHealth playerHealth;                  // Reference to the player's health.
	GameObject[] turrets;
	TowerBasherMovement movement;

	bool InRange;                         // Whether player is within the trigger collider and can be attacked.
	float timer;                                // Timer for counting up to the next attack.
	bool anyTowersLeft;


	void Awake ()
	{
		// Setting up the references.
		movement = GetComponent<TowerBasherMovement>();
		anyTowersLeft = false;
		if (GameObject.FindWithTag ("Turret") != null) {
			turrets = GameObject.FindGameObjectsWithTag ("Turret");
			turret = turrets [0];
			turretHealth = turret.GetComponent <TurretHealth> ();
			anyTowersLeft = true;
		}
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent <PlayerHealth> ();
		enemyHealth = GetComponent<EnemyHealth>();
		anim = GetComponent <Animator> ();
	}


	void OnTriggerEnter (Collider other)
	{
		// If the entering collider is the base...
		if(other.gameObject == turret || other.gameObject == player)
		{
			// ... the base is in range.
			InRange = true;
		}
	}


	void OnTriggerExit (Collider other)
	{
		// If the exiting collider is the base...
		if(other.gameObject == turret || other.gameObject == player)
		{
			// ... the base is no longer in range.
			InRange = false;
		}
	}


	void Update ()
	{
		// Add the time since Update was last called to the timer.
		timer += Time.deltaTime;

		// If the timer exceeds the time between attacks, the player/tower is in range and this enemy is alive...
		if(timer >= timeBetweenAttacks && InRange && enemyHealth.currentHealth > 0)
		{
			// ... attack.
			Attack ();
		}

		if (anyTowersLeft) {
			if (turretHealth.currenthealth < 0) {
				enemyHealth.Death ();
			}
		} else if(playerHealth.currentHealth <= 0)
		{
			// ... tell the animator the player is dead.
			anim.SetTrigger ("PlayerDead");
		}
	}


	void Attack ()
	{
		// Reset the timer.
		timer = 0f;

		if (anyTowersLeft) {
			// If the turret has health to lose...
			if (turretHealth.currenthealth > 0) {
				// ... damage the turret.
				turretHealth.DamageTower (attackDamage);
			}
		}
		else if(playerHealth.currentHealth > 0 && movement.isPlayerLockedOn())
		{
			// ... damage the player.
			playerHealth.TakeDamage(attackDamage);
		}
	}
}

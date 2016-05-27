using UnityEngine;
using System.Collections;
public class TowerBasherAttackAndMovement : MonoBehaviour
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
	NavMeshAgent nav;               // Reference to the nav mesh agent.

	bool InRange;                         // Whether player is within the trigger collider and can be attacked.
	float timer;                                // Timer for counting up to the next attack.
	bool playerLockOn;				// Is the enemy locked onto the player?


	void Awake ()
	{
		// Setting up the references.
		/*<--------Attack------------>*/
		playerLockOn = true;
		if (GameObject.FindWithTag ("Turret") != null) {
			turrets = GameObject.FindGameObjectsWithTag ("Turret");
			turret = turrets [0];
			turretHealth = turret.GetComponent <TurretHealth> ();
			playerLockOn = false;
		}
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent <PlayerHealth> ();
		enemyHealth = GetComponent<EnemyHealth>();
		anim = GetComponent <Animator> ();

		/*<------- Movement ------------>*/
		// Set up the references.
		nav = GetComponent <NavMeshAgent> ();
	}


	void OnTriggerEnter (Collider other)
	{
		if (!playerLockOn) {
			if (other.gameObject == turret) {
				InRange = true;
			}
		} else if (playerLockOn) {
			if (other.gameObject == player) {
				InRange = true;
			}
		}
	}


	void OnTriggerExit (Collider other)
	{
		if (!playerLockOn) {
			if (other.gameObject == turret) {
				InRange = false;
			}
		} else if (playerLockOn) {
			if (other.gameObject == player) {
				InRange = false;
			}
		}
	}


	void Update ()
	{
		/*<-----------Attack---------->*/
		// Add the time since Update was last called to the timer.
		timer += Time.deltaTime;

		// If the timer exceeds the time between attacks, the player/tower is in range and this enemy is alive...
		if(timer >= timeBetweenAttacks && InRange && enemyHealth.currentHealth > 0)
		{
			// ... attack.
			Attack ();
		}

		if (!playerLockOn) {
			if (turretHealth.currenthealth < 0) {
				enemyHealth.Death ();
			}
		} else if(playerHealth.currentHealth <= 0)
		{
			// ... tell the animator the player is dead.
			anim.SetTrigger ("PlayerDead");
		}

		/*<----------Movement------------>*/

		if (!playerLockOn && enemyHealth.currentHealth > 0 && turretHealth.currenthealth > 0) {
			// If the enemy and the player have health left...
				// ... set the destination of the nav mesh agent to the base.
			nav.SetDestination (turret.transform.position);

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


	void Attack ()
	{
		// Reset the timer.
		timer = 0f;

		if (!playerLockOn && turretHealth.currenthealth > 0) {
			// If the turret has health to lose...
				// ... damage the turret.
				turretHealth.DamageTower (attackDamage);
		}
		if(playerHealth.currentHealth > 0 && playerLockOn)
		{
			// ... damage the player.
			playerHealth.TakeDamage(attackDamage);
		}
	}
}



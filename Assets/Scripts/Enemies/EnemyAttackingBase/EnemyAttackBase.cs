using UnityEngine;
using System.Collections;


public class EnemyAttackBase : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
    public int attackDamage = 10;               // The amount of health taken away per attack.


    Animator anim;                              // Reference to the animator component.
    GameObject homeBase;                          // Reference to the homebase GameObject.
	PlayerHome homeBaseHealth;                  // Reference to the homebase's health.
	GameObject player;                          // Reference to the player GameObject.
	PlayerHealth playerHealth;                  // Reference to the player's health.
    EnemyHealth enemyHealth;                    // Reference to this enemy's health.
	EnemyMovementBase movement;					// Reference to this enemy's movement.
    bool InRange;                         // Whether player is within the trigger collider and can be attacked.
    float timer;                                // Timer for counting up to the next attack.


    void Awake ()
    {
        // Setting up the references.
		homeBase = GameObject.FindGameObjectWithTag ("Base");
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent <PlayerHealth> ();
        homeBaseHealth = homeBase.GetComponent <PlayerHome> ();
        enemyHealth = GetComponent<EnemyHealth>();
		movement = GetComponent<EnemyMovementBase> ();
        anim = GetComponent <Animator> ();
    }


    void OnTriggerEnter (Collider other)
    {
        // If the entering collider is the base...
		if(other.gameObject == homeBase || other.gameObject == player)
        {
            // ... the base is in range.
            InRange = true;
        }
    }


    void OnTriggerExit (Collider other)
    {
        // If the exiting collider is the base...
		if(other.gameObject == homeBase || other.gameObject == player)
        {
            // ... the base is no longer in range.
            InRange = false;
        }
    }


    void Update ()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
        if(timer >= timeBetweenAttacks && InRange && enemyHealth.currentHealth > 0)
        {
            // ... attack.
            Attack ();
        }

        // If the base has zero or less health...
		if(homeBaseHealth.currentHomeHealth <= 0 || playerHealth.currentHealth <= 0)
        {
            // ... tell the animator the player is dead.
            anim.SetTrigger ("PlayerDead");
        }
    }


    void Attack ()
    {
        // Reset the timer.
        timer = 0f;

        // If the base has health to lose...
		if(homeBaseHealth.currentHomeHealth > 0 && !movement.isPlayerLockedOn())
        {
            // ... damage the base.
			homeBaseHealth.DamageHome(attackDamage);
        }
		// If the player has health to lose...
		else if(playerHealth.currentHealth > 0 && movement.isPlayerLockedOn())
		{
			// ... damage the player.
			playerHealth.TakeDamage(attackDamage);
		}
    }
}

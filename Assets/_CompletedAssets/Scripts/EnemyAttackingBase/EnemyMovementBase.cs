using UnityEngine;
using System.Collections;


public class EnemyMovementBase : MonoBehaviour
{
    Transform homeBase;             // Reference to the base's position.
	Transform player;				// Reference to the player's position.
	PlayerHome homeHealth;      	// Reference to the base's health.
	PlayerHealth playerHealth;      // Reference to the player's health.
    EnemyHealth enemyHealth;        // Reference to this enemy's health.
    NavMeshAgent nav;               // Reference to the nav mesh agent.
	bool playerNearby;				// Is the player nearby?
	public bool canFollowPlayer;	// Some enemies will target the player if he's close enough, others will not. 	
									// This boolean will determine that.

    void Awake ()
    {
        // Set up the references.
		playerNearby = false;
		homeBase = GameObject.FindGameObjectWithTag ("Base").transform;
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		homeHealth = homeBase.GetComponent <PlayerHome> ();
		playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <NavMeshAgent> ();
    }
		
    void Update ()
    {
		if (canFollowPlayer) {
			float dist = Vector3.Distance (player.position, transform.position);
			if (dist < 9) {
				playerNearby = true;
			}
		}
		// If the player is nearby the enemy, the enemy will focus, follow, and attack the player.
		if (playerNearby) {
			if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0) {
				nav.SetDestination (player.position);
			}
		}
        // If the enemy and the player have health left...
		else if(enemyHealth.currentHealth > 0 && homeHealth.currentHomeHealth > 0)
        {
            // ... set the destination of the nav mesh agent to the base.
			nav.SetDestination (homeBase.position);
        }
        // Otherwise...
        else
        {
            // ... disable the nav mesh agent.
            nav.enabled = false;
        }
    }
}

using UnityEngine;
using System.Collections;


public class DasherMovement : MonoBehaviour
{
	Transform homeBase;             // Reference to the base's position.
	PlayerHome homeHealth;      	// Reference to the base's health.
	EnemyHealth enemyHealth;        // Reference to this enemy's health.
	NavMeshAgent nav;               // Reference to the nav mesh agent.

	void Awake ()
	{
		// Set up the references.
		homeBase = GameObject.FindGameObjectWithTag ("Base").transform;
		homeHealth = homeBase.GetComponent <PlayerHome> ();
		enemyHealth = GetComponent <EnemyHealth> ();
		nav = GetComponent <NavMeshAgent> ();
	}

	void Update ()
	{
		
		// If the enemy and the player have health left...
		if(enemyHealth.currentHealth > 0 && homeHealth.currentHomeHealth > 0)
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

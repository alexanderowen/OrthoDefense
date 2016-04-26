using UnityEngine;
using System.Collections;


public class EnemyMovementBase : MonoBehaviour
{
    Transform homeBase;               // Reference to the player's position.
    PlayerHome homeHealth;      // Reference to the player's health.
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
            // ... set the destination of the nav mesh agent to the player.
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

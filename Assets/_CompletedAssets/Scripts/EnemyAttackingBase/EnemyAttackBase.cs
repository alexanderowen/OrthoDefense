﻿using UnityEngine;
using System.Collections;


public class EnemyAttackBase : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
    public int attackDamage = 10;               // The amount of health taken away per attack.


    Animator anim;                              // Reference to the animator component.
    GameObject homeBase;                          // Reference to the player GameObject.
    PlayerHome homeBaseHealth;                  // Reference to the player's health.
    EnemyHealth enemyHealth;                    // Reference to this enemy's health.
    bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
    float timer;                                // Timer for counting up to the next attack.


    void Awake ()
    {
        // Setting up the references.
        homeBase = GameObject.FindGameObjectWithTag ("Base");
        homeBaseHealth = homeBase.GetComponent <PlayerHome> ();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();
    }


    void OnTriggerEnter (Collider other)
    {
        // If the entering collider is the player...
		if(other.gameObject == homeBase)
        {
            // ... the player is in range.
            playerInRange = true;
        }
    }


    void OnTriggerExit (Collider other)
    {
        // If the exiting collider is the player...
		if(other.gameObject == homeBase)
        {
            // ... the player is no longer in range.
            playerInRange = false;
        }
    }


    void Update ()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
        if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            // ... attack.
            Attack ();
        }

        // If the player has zero or less health...
		if(homeBaseHealth.currentHomeHealth <= 0)
        {
            // ... tell the animator the player is dead.
            anim.SetTrigger ("PlayerDead");
        }
    }


    void Attack ()
    {
        // Reset the timer.
        timer = 0f;

        // If the player has health to lose...
        if(homeBaseHealth.currentHomeHealth > 0)
        {
            // ... damage the player.
			homeBaseHealth.DamageHome(attackDamage);
        }
    }
}

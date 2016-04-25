using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform homeBase;
    //PlayerHealth playerHealth;
    //EnemyHealth enemyHealth;
    NavMeshAgent nav;


    void Awake ()
    {
        homeBase = GameObject.FindGameObjectWithTag ("Base").transform;
        //playerHealth = player.GetComponent <PlayerHealth> ();
        //enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <NavMeshAgent> ();
    }


    void Update ()
    {
        //if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        //{
            nav.SetDestination (homeBase.position);
        //}
        //else
        //{
        //    nav.enabled = false;
        //}
    }
}

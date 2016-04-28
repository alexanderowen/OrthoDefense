using UnityEngine;
using System.Collections;

public class TurretShooter : MonoBehaviour {

    public float fireRate;
    public GameObject shot;

    public bool enemyInRange;

    private float timer;
    private Transform enemyTransform;
    private Vector3 direction;
    private Vector3 position;

	/* TODO this code needs cleaned up;
	 * I recommend using a priority queue to store turret enemy list
	 * on trigger entry update priority queue with new enemy
	 * on trigger exit remove entry from priority queue
	 * while pq not empty turret focuses element 0
	 * include logic each update to check if element 0 is null
	 * if null remove from pq else perform turret look/shoot in update
	 */


    void Update()
    {
		if(enemyInRange)
        {
			if (enemyTransform != null) {
				direction = enemyTransform.position - transform.position;
				Vector3 lookDirection = new Vector3 (direction.x, transform.position.y, direction.z);
				transform.LookAt (lookDirection);
			}

            if (Time.time > timer + fireRate)
            {
                Shoot();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (enemyTransform != null && enemyTransform.parent != null)
            {
                enemyInRange = true;

                if (Vector3.Distance(transform.position, enemyTransform.position) > Vector3.Distance(transform.position, other.transform.position))
                {
                    enemyTransform = other.transform;
                }
            }
            else
            {
                enemyTransform = other.transform;
            }
            direction = enemyTransform.position - transform.position;
            Vector3 lookDirection = new Vector3(direction.x, transform.position.y, direction.z);
            transform.LookAt(lookDirection);
            enemyInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyInRange = false;
        }
    }

    void Shoot()
    {
        Instantiate(shot, new Vector3(transform.position.x + 1, transform.position.y + 1, transform.position.z + 1), Quaternion.LookRotation(direction));
        timer = Time.time;
    }
}

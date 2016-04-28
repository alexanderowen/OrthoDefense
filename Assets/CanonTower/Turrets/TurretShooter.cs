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

    void Update()
    {
        if(enemyInRange)
        {
            direction = enemyTransform.position - transform.position;
            Vector3 lookDirection = new Vector3(direction.x, transform.position.y, direction.z);
            transform.LookAt(lookDirection);

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

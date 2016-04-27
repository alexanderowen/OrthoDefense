using UnityEngine;
using System.Collections;

public class TurretShooter : MonoBehaviour {

    public float fireRate;
    public GameObject shot;

    private bool enemyInRange;
    private float timer;
    private Transform enemyTransform;
    private Vector3 direction;
    private Vector3 position;

    void Update()
    {
        if(enemyInRange && Time.time > timer + fireRate)
        {
            Shoot();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyTransform = other.transform;
            direction = transform.position - enemyTransform.position;
            transform.LookAt(other.transform);
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
        Instantiate(shot, transform.position, Quaternion.LookRotation(-direction));
        timer = Time.time;
    }
}

using UnityEngine;
using System.Collections;

public class TurretDamager : MonoBehaviour {

    public int damagePerHit;
    public float forceTime;

    private EnemyHealth enemyHealth;
    private new Renderer renderer;
    private Rigidbody rb;
    private float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        timer = Time.time + forceTime;
    }

    void FixedUpdate()
    {
        if (Time.time < timer)
        {
            rb.AddForce(transform.forward * 50);
			rb.AddForce (transform.up * 7);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Floor") || other.gameObject.CompareTag("Enemy")) 
        {
            createExplosion(transform);
        }
    }

    void createExplosion(Transform atPos)
    {
        renderer = GetComponent<Renderer>();
        renderer.enabled = false;
        rb.velocity = new Vector3(0,0,0);
        rb.useGravity = false;
        rb.isKinematic = true;
        StartCoroutine(explosionDestroy());
    }

    IEnumerator explosionDestroy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}

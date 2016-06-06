using UnityEngine;
using System.Collections;

public class TurretDamager : MonoBehaviour {

    public int damagePerHit;
    public float forceTime;
    public GameObject puff;

    private GameObject puffCopy;
    private bool isExploding;
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
            if (!isExploding)
            {
                createExplosion(transform);
            }
        }
    }

    void createExplosion(Transform atPos)
    {
        isExploding = true;
        renderer = GetComponent<Renderer>();
        renderer.enabled = false;
        rb.velocity = new Vector3(0,0,0);
        rb.useGravity = false;
        rb.isKinematic = true;
        StartCoroutine(explosionDestroy());
    }

    IEnumerator explosionDestroy()
    {
        GameObject boom = (GameObject)Instantiate(puff, this.gameObject.transform.position, this.gameObject.transform.rotation);
        for(int i = 0; i < 30; i++)
        {
            yield return new WaitForSeconds(.05f);
            boom.GetComponent<ExplosionMat>()._alpha = Mathf.Lerp(boom.GetComponent<ExplosionMat>()._alpha, -.5f, .15f);
        }
        yield return new WaitForSeconds(.25f);
        Destroy(boom);
        Destroy(this.gameObject);
    }
}

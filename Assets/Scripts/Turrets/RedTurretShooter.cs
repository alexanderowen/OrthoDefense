using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RedTurretShooter : MonoBehaviour {


    //private LineRenderer lr;
    private Queue<GameObject> enemyQueue;

    private float timer;

    void Start()
    {
        //lr = GetComponent<LineRenderer>();
        enemyQueue = new Queue<GameObject>();

        this.GetComponentInChildren<LightningBolt>().target = this.GetComponentInChildren<LightningBolt>().transform;

        //lr.SetVertexCount(2);
        //lr.enabled = false;
    }
	
	void Update () {

        if(enemyQueue.Count != 0)
        {
            if(enemyQueue.Peek() == null)
            {
                enemyQueue.Dequeue();
            }
            if (enemyQueue.Count != 0)
            {
                ShootLightning();
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyQueue.Enqueue(other.gameObject);
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyQueue.Dequeue();
            other.GetComponent<EnemyHealth>().isZapped = false;
        }
    }

    void ShootLightning()
    {
        //lr.enabled = true;
        if (enemyQueue.Peek() != null)
        {
            GameObject enemy = enemyQueue.Peek();
            if (enemy.GetComponent<EnemyHealth>().isDead == false)
            {
                enemy.GetComponent<EnemyHealth>().isZapped = true;

                //lr.SetPosition(0, new Vector3(this.transform.position.x, this.transform.position.y + 5.8f, this.transform.position.z));
                //lr.SetPosition(1, enemy.transform.position);
                this.GetComponentInChildren<LightningBolt>().target = enemy.transform;
            }
        }
        else
        {
            //lr.enabled = false;
            this.GetComponentInChildren<LightningBolt>().target = this.transform;
        }

        
    }
}

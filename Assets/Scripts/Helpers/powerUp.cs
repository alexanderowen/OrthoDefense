using UnityEngine;
using System.Collections;

public class powerUp : MonoBehaviour {

	public BuildManager buildManager;

	// Use this for initialization
	void Start () {
	}
	
	void OnTriggerEnter (Collider other)
	{
		// If the entering collider is the player...
		if(other.gameObject.tag == "Player")
		{
			if (this.transform.name.StartsWith ("red"))
				BuildManager.magelimit++;
			else if (this.transform.name.StartsWith ("blue"))
				BuildManager.cannonlimit++;

			Destroy (this.gameObject);
		}
	}
}

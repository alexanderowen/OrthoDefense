using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	void Awake () {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}

		DontDestroyOnLoad(gameObject); // When reloading the scene, don't destroy this.

		InitGame ();
	}

	void InitGame() {
	}
	

	void Update () {	
	}
}

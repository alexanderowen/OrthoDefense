using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public int currentLevel = 1;
	public int finalLevel = 5;

	void Awake () {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}

		DontDestroyOnLoad(gameObject); // When reloading the scene, don't destroy this.
	}

	public void RestartLevel () {
		// Tell the BM to set the turret values to what they were at the beginning of the level.
		BuildManager BM = GameObject.Find("BuildCanvas").GetComponent<BuildManager> ();
		BM.ResetStaticTurretLimits ();

        // Reload the level that is currently loaded.
		SceneManager.LoadScene (currentLevel);
	}

	public void LevelComplete() {
		Animator anim = GameObject.Find("HUDCanvas").GetComponent<Animator> ();
		anim.SetTrigger ("LevelComplete");

		// Play Animator
		// Within animator, call GoToNextLevel. 
	}
		
	public void GoToNextLevel() {
		if (currentLevel == finalLevel) {
			GameComplete ();
			return;
		}

		currentLevel++;

		SceneManager.LoadScene (currentLevel);
	}

	void GameComplete() {
		// TODO: YOU WIN text.
		SceneManager.LoadScene ("Load Screen");
	}
}

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

	public void LevelComplete() {
		Animator anim = GameObject.Find ("HUDCanvas").GetComponent<Animator> ();
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

		string sceneName = "Level 0" + currentLevel.ToString();
		SceneManager.LoadScene (sceneName);

	}

	void GameComplete() {


	}

	void Update () {	
	}
}

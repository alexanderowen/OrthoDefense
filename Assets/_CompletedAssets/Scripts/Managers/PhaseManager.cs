using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PhaseManager : MonoBehaviour {

	public static PhaseManager instance = null;
	public Text phaseText;
	public bool IsBuildPhase;

	private GameObject enemyManager;
	private GameObject player;
	Animator anim;

	void Awake() {
		anim = GetComponent<Animator> ();
		enemyManager = GameObject.Find ("EnemyManager");
		player = GameObject.Find ("Player");

		BeginBuildPhase ();
	}

	void BeginBuildPhase() {
		IsBuildPhase = true;
		enemyManager.SetActive (false);
		player.SetActive (false);

		phaseText.text = "Start Build Phase";
		anim.SetTrigger ("StartBuildPhase");
	}

	public void BeginAttackPhase() {
		enemyManager.SetActive (true);
		enemyManager.GetComponent<EnemyManager> ().BeginSpawning();
		player.SetActive (true);


		// Do the animation thing again
		phaseText.text = "Attack Phase";
	}

	void Update () {
	
	}
}

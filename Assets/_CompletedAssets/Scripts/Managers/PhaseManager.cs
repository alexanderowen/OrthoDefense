using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PhaseManager : MonoBehaviour {

	public static PhaseManager instance = null;
	public GameObject enemyManager;

	public Text phaseText;

	Animator anim;

	public bool IsBuildPhase;

	void Awake() {
		anim = GetComponent<Animator> ();
		enemyManager = GameObject.Find ("EnemyManager");

		BeginBuildPhase ();
	}

	void BeginBuildPhase() {
		IsBuildPhase = true;
		enemyManager.SetActive (false);

		phaseText.text = "Start Build Phase";
		anim.SetTrigger ("StartBuildPhase");


	}

	public void BeginAttackPhase() {
		enemyManager.SetActive (true);
		phaseText.text = "Attack Phase";
		// TODO rebake nav mesh
		//enemyManager.BeginSpawning ();
	}

	void Update () {
	
	}
}

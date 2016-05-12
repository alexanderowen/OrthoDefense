using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PhaseManager : MonoBehaviour {

	public static PhaseManager instance = null;
	public Text phaseText;
	public bool IsBuildPhase;
	public GameObject healthUI;
	public GameObject homeHealthUI;
	public GameObject scoreText;
	public GameObject buildCanvas;

	private GameObject enemyManager;
	private GameObject player;
	private Animator anim;

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
		buildCanvas.GetComponent<Animator> ().SetTrigger ("FadeIn");
	}

	public void BeginAttackPhase() {
		GameObject nobuildzone = GameObject.Find ("NoBuildZone");
		nobuildzone.SetActive (false);
		enemyManager.SetActive (true);

		foreach (EnemyManager enemy in enemyManager.GetComponents<EnemyManager> ()) {
			enemy.BeginSpawning();
		}

		player.SetActive (true);
		healthUI.SetActive (true);
		homeHealthUI.SetActive (true);
		scoreText.SetActive (true);
		buildCanvas.SetActive (false);


		// Do the animation thing again
		//phaseText.text = "Attack Phase";
		phaseText.text = "";
	}

	void Update () {
		
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class BuildManager : MonoBehaviour {

	private PhaseManager phaseManager;

	Canvas canvas;

	void Start()
	{
		canvas = GetComponent<Canvas>();
		phaseManager = GameObject.Find("HUDCanvas").GetComponent<PhaseManager> ();
	}

	void Update()
	{
		if (phaseManager.IsBuildPhase) {
			canvas.enabled = true;
		}
	}

	public void spawnTower() {
		//TODO attach prefab to mouse for placement
	}

	public void gameStart()
	{
		phaseManager.IsBuildPhase = false;
		phaseManager.BeginAttackPhase ();
	}

	public void helpMesh() {
		//TODO placeable mesh pattern towers can be placed
	}


	public void Quit()
	{
		#if UNITY_EDITOR 
		EditorApplication.isPlaying = false;
		#else 
		Application.Quit();
		#endif
	}
}

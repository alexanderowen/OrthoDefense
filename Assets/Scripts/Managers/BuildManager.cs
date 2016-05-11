using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class BuildManager : MonoBehaviour {

	private PhaseManager phaseManager;
	private AudioSource backgroundmusic;
	public int towerLimit;
	public GameObject towerprefab;
	private bool buildPhase;

	Canvas canvas;

	void Start()
	{
		backgroundmusic = GameObject.Find ("BackgroundMusic").GetComponent<AudioSource> ();
		canvas = GetComponent<Canvas>();
		phaseManager = GameObject.Find("HUDCanvas").GetComponent<PhaseManager> ();
		buildPhase = false;
	}

	void Update ()
	{
		if (phaseManager.IsBuildPhase && buildPhase) {
			canvas.enabled = true;

			Vector3 mousePos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0f);

			if (Input.GetMouseButtonDown (0)) {
				Vector3 wordPos;
				Ray ray = Camera.main.ScreenPointToRay (mousePos);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit)) {
					if (hit.collider.tag == "Buildable") {
						wordPos = hit.point;
						if (towerLimit > 0) {
							Instantiate (towerprefab, wordPos, Quaternion.identity); //or for tandom rotarion use Quaternion.LookRotation(Random.insideUnitSphere)
							towerLimit--;
						} else {
							wordPos = Camera.main.ScreenToWorldPoint (mousePos);
						}
					}
				}								
			}
		}
	}


	public void spawnTower() {
		buildPhase = !buildPhase;
	}


	public void gameStart()
	{
		backgroundmusic.enabled = true;
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

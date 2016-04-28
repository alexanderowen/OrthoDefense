using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class BuildManager : MonoBehaviour {

	private PhaseManager phaseManager;
	private AudioSource backgroundmusic;
	private GameObject tower;

	public GameObject towerprefab;

	Canvas canvas;

	void Start()
	{
		backgroundmusic = GameObject.Find ("BackgroundMusic").GetComponent<AudioSource> ();
		canvas = GetComponent<Canvas>();
		phaseManager = GameObject.Find("HUDCanvas").GetComponent<PhaseManager> ();
	}

	void Update()
	{
		if (phaseManager.IsBuildPhase) {
			canvas.enabled = true;

			Vector3 mousePos=new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);

			if (tower != null)
				tower.transform.position = mousePos;

			if(Input.GetMouseButtonDown(0)) {
				Vector3 wordPos;
				Ray ray=Camera.main.ScreenPointToRay(mousePos);
				RaycastHit hit;
				if(Physics.Raycast(ray,out hit,1000f)){
					wordPos=hit.point;
				} else {
					wordPos=Camera.main.ScreenToWorldPoint(mousePos);
				}
				if (hit.transform.tag == "Floor") {
				}
					Instantiate(towerprefab, wordPos, Quaternion.identity); 
					//or for tandom rotarion use Quaternion.LookRotation(Random.insideUnitSphere)						
			}
		}
	}


	public void spawnTower() {
		//TODO attach prefab to mouse for placement

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

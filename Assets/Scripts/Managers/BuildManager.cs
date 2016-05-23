using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class BuildManager : MonoBehaviour {

	public static int cannonlimit = 2;
	public static int magelimit = 2;
	public int currentCannonLimit;
	public int currentMageLimit;

	public GameObject towerprefab;
	public GameObject mageprefab;
	public Camera miniMap;

	private PhaseManager phaseManager;
	private AudioSource backgroundmusic;
	private AudioSource towerBuildAudio;
	private Canvas canvas;
	private Button cannonbutton;
	private Button magebutton;
	private bool buildPhase;
	private bool magetower;
	private bool cannontower;



	void Start() {
		towerBuildAudio = GetComponent<AudioSource> ();
		canvas 			= GetComponent<Canvas>();
		backgroundmusic = GameObject.Find ("BackgroundMusic").GetComponent<AudioSource> ();
		phaseManager 	= GameObject.Find ("HUDCanvas").GetComponent<PhaseManager> ();
		cannonbutton	= GameObject.Find ("CannonButton").GetComponent<Button> ();
		magebutton 		= GameObject.Find ("MageButton").GetComponent<Button> ();

		cannonbutton.GetComponentInChildren<Text> ().text = cannonlimit.ToString ();
		magebutton.GetComponentInChildren<Text> ().text = magelimit.ToString ();

		magetower 		= false;
		cannontower 	= false;
		buildPhase 		= true;
		miniMap.enabled = false;

		currentCannonLimit = cannonlimit;
		currentMageLimit = magelimit;
	}

	void Update ()
	{
		if (phaseManager.IsBuildPhase && buildPhase) {
			canvas.enabled = true;

			Vector3 mousePos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0f);

			if (Input.GetMouseButtonDown (0)) {
				if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject () == false) {
					Vector3 wordPos;
					Ray ray = Camera.main.ScreenPointToRay (mousePos);
					RaycastHit hit;
					if (Physics.Raycast (ray, out hit)) {
						if (hit.collider.tag == "Buildable") {
							wordPos = hit.point;
							if (cannontower && cannonlimit > 0) {
								Instantiate (towerprefab, wordPos, Quaternion.identity); //or for tandom rotarion use Quaternion.LookRotation(Random.insideUnitSphere)
								cannonlimit--;
								cannonbutton.GetComponentInChildren<Text> ().text = cannonlimit.ToString ();
								towerBuildAudio.Play ();
							} else if (magetower && magelimit > 0) {
								Instantiate (mageprefab, wordPos, Quaternion.Euler (0, 60, 0)); //or for tandom rotarion use Quaternion.LookRotation(Random.insideUnitSphere)
								magelimit--;
								magebutton.GetComponentInChildren<Text> ().text = magelimit.ToString ();
								towerBuildAudio.Play ();
							} else {
								wordPos = Camera.main.ScreenToWorldPoint (mousePos);
							}
						}
					}
				}								
			}
		} 

		if (miniMap.enabled)
			miniMap.enabled = false;
	}

	public void ResetStaticTurretLimits() {
		// This method is to be called outside the class, for when the level needs to be restarted. 
		// Resets the static variables. 
		cannonlimit = currentCannonLimit;
		magelimit = currentMageLimit;
	}

	public void spawnTower() {
		if (magetower)
			magetower = false;
		cannontower = !cannontower;
	}

	public void mageTower() {
		if (cannontower)
			cannontower = false;
		magetower = !magetower;
	}

	public void gameStart()
	{
		backgroundmusic.enabled = true;
		phaseManager.BeginAttackPhase ();
		miniMap.enabled = true;
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

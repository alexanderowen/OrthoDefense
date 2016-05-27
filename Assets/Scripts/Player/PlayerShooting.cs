using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;


public class PlayerShooting : MonoBehaviour
{
	
    //public int damagePerShot = 20;                  // The damage inflicted by each bullet.
    //public float timeBetweenBullets = 0.15f;        // The time between each shot.
    //public float range = 100f;                      // The distance the gun can fire.


    float timer;                                    // A timer to determine when to fire.

    Ray shootRay;                                   // A ray from the gun end forwards.
	Ray shootLeft;
	Ray shootRight;
	public LineRenderer leftline;
	public LineRenderer rightline;
	public ParticleSystem gunparticle;

    RaycastHit shootHit;                            // A raycast hit to get information about what was hit.
    int shootableMask;                              // A layer mask so the raycast only hits things on the shootable layer.
    ParticleSystem gunParticles;                    // Reference to the particle system.
    LineRenderer gunLine;                           // Reference to the line renderer.
    AudioSource[] gunShotClips;
    Light gunLight;                                 // Reference to the light component.
	public Light faceLight;								// Duh
    float effectsDisplayTime = 0.2f;                // The proportion of the timeBetweenBullets that the effects will display for.

    private enum Gun { Rifle, Shotgun, Laser };

    Gun gunType;
    private int currentDamage;
	private float currentTime;
	private float currentRange;

    class GunClass {
		public int damagePerShot;
		public float timeBetweenBullets;
		public float range;
		public AudioSource gunShotClip;
		public float accuracy;

		public GunClass (int _damage, float _time, float _range, AudioSource _audio, float accuracy) {
			damagePerShot = _damage;
			timeBetweenBullets = _time;
    		range = _range;
			gunShotClip = _audio;
			this.accuracy = accuracy; 
    	}
    }

	GunClass[] gunList = new GunClass[3];

    void Awake ()
    {
        // Create a layer mask for the Shootable layer.
        shootableMask = LayerMask.GetMask ("Shootable");

        // Set up the references.
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunShotClips = GetComponents<AudioSource> ();
        gunLight = GetComponent<Light> ();
		//faceLight = GetComponentInChildren<Light> ();

		gunShotClips[2].volume = 0.8f;

		gunList [0] = new GunClass (20, 0.15f, 32f, gunShotClips[0], 0.8f); // Rifle
		gunList [1] = new GunClass (50, 0.8f, 15f, gunShotClips[2], 0.2f); // Shotgun
		gunList [2] = new GunClass (200, 1.5f, 100f, gunShotClips[1], 1.1f); // Laser
		gunType = Gun.Rifle;
    }

    void Update ()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

#if !MOBILE_INPUT
        // If the Fire1 button is being press and it's time to fire...
		if((Input.GetButton ("Fire1") || Input.GetKey("space")) && timer >= currentTime && Time.timeScale != 0)
        {
            // ... shoot the gun.
            Shoot(gunType);
        }
#else
        // If there is input on the shoot direction stick and it's time to fire...
        if ((CrossPlatformInputManager.GetAxisRaw("Mouse X") != 0 || CrossPlatformInputManager.GetAxisRaw("Mouse Y") != 0) && timer >= timeBetweenBullets)
        {
            // ... shoot the gun
            Shoot();
        }
#endif
        // If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
        if(timer >= currentTime * effectsDisplayTime)
        {
            // ... disable the effects.
            DisableEffects ();
        }

		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			gunType = Gun.Rifle;
		}
		else if (Input.GetKeyDown (KeyCode.Alpha2)) {
			gunType = Gun.Shotgun;
        } 
		else if (Input.GetKeyDown (KeyCode.Alpha3)) {
			gunType = Gun.Laser;
		}
    }


    public void DisableEffects ()
    {
        // Disable the line renderer and the light.
        gunLine.enabled = false;
		leftline.enabled = false;
		rightline.enabled = false;
		faceLight.enabled = false;
        gunLight.enabled = false;
    }

	void ifHit(Ray shot, float currentRange, int currentDamage, LineRenderer outline) {
		// Perform the raycast against gameobjects on the shootable layer and if it hits something...
		if (Physics.Raycast (shot, out shootHit, currentRange, shootableMask)) {
			// Try and find an EnemyHealth script on the gameobject hit.
			EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth> ();

			// If the EnemyHealth component exist...
			if (enemyHealth != null) {
				// ... the enemy should take damage.
				enemyHealth.TakeDamage (currentDamage, shootHit.point);
			}

			// Set the second position of the line renderer to the point the raycast hit.
			outline.SetPosition (1, shootHit.point);
		}
		// If the raycast didn't hit anything on the shootable layer...
		else {
			// ... set the second position of the line renderer to the fullest extent of the gun's range.
			outline.SetPosition (1, shot.origin + shot.direction * currentRange);
		}
	}


	void Shoot(Gun gunType)
    {
        currentDamage = gunList[(int)gunType].damagePerShot;
        currentTime = gunList[(int)gunType].timeBetweenBullets;
        currentRange = gunList[(int)gunType].range;
		/*
		if (gunType == Gun.Rifle)
			gunLine.SetColors(Color.yellow);
		if (gunType == Gun.Shotgun)
        	gunLine.SetColors(Color.green);
		if (gunType == Gun.Laser)
			gunLine.SetColors (Color.red);
			*/

        // Reset the timer.
        timer = 0f;

        // Play the gun shot audioclip.
        gunList[(int)gunType].gunShotClip.Play();

        // Enable the lights.
        gunLight.enabled = true;
        faceLight.enabled = true;

        // Stop the particles from playing if they were, then start the particles.
        gunParticles.Stop();
        gunParticles.Play();

        // Enable the line renderer and set it's first position to be the end of the gun.
        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        // Set the shootRay so that it starts at the end of the gun and points forward from the barrel.

        shootRay.origin = transform.position;
		shootLeft.origin = transform.position; //for shotgun
		shootRight.origin = transform.position; //for shotgun

		if (gunType == Gun.Rifle) {
			faceLight.color = Color.yellow;
			gunLine.material.color = Color.yellow;
			gunLine.SetColors (Color.yellow, Color.yellow);
			gunparticle.startColor = Color.yellow;
			gunLight.color = Color.yellow;
			gunLine.SetWidth (0.05f, 0.05f);
			shootRay.direction = transform.forward;
			ifHit (shootRay, currentRange, currentDamage, gunLine);
		}


		if (gunType == Gun.Shotgun) {
			faceLight.color = Color.red;
			gunLine.material.color = Color.red;
			leftline.material.color = Color.red;
			rightline.material.color = Color.red;
			gunparticle.startColor = Color.red;
			gunLine.SetColors (Color.red, Color.red);
			gunLine.SetWidth (0.05f, 0.05f);
			gunLight.color = Color.red;
			leftline.enabled = true;
			leftline.SetPosition (0, transform.position);
			rightline.enabled = true;
			rightline.SetPosition (0, transform.position);

			shootLeft = new Ray(transform.position, Quaternion.Euler (0, -10, 0) * transform.forward);
			shootRight = new Ray(transform.position, Quaternion.Euler (0, 10, 0) * transform.forward);
			shootRay.direction = transform.forward;

			ifHit (shootRay, currentRange, currentDamage, gunLine);
			ifHit (shootLeft, currentRange, currentDamage, leftline);
			ifHit (shootRight, currentRange, currentDamage, rightline);

		}
		if (gunType == Gun.Laser) {
			faceLight.color = Color.blue;
			gunLine.material.color = Color.blue;
			gunLine.SetColors (Color.blue, Color.blue);
			gunparticle.startColor = Color.blue;
			gunLight.color = Color.blue;
			gunLine.SetWidth (0.05f, 1f);
			shootRay.direction = transform.forward;

			RaycastHit[] rayhits;
			rayhits = Physics.SphereCastAll (shootRay, 0.7f);
			for (int i = 0; i < rayhits.Length; i++) {
				EnemyHealth enemyHealth = rayhits[i].collider.GetComponent<EnemyHealth> ();

				// If the EnemyHealth component exist...
				if (enemyHealth != null) {
					// ... the enemy should take damage.
					enemyHealth.TakeDamage (currentDamage, shootHit.point);
				}
			}
			gunLine.SetPosition (1, shootRay.origin + shootRay.direction * currentRange);
		}
    }
}

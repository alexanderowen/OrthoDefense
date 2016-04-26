using UnityEngine;

namespace CompleteProject {
    public class GameOverManager : MonoBehaviour {
        public PlayerHealth playerHealth;
		public PlayerHome playerHome;
		private GameObject player;

        Animator anim;

        void Awake () {
            anim = GetComponent <Animator> ();
			player = GameObject.Find ("Player");

        }

        void Update () {
			if (!player.activeInHierarchy) {
				return;
			}

            // If the player or playerHome has run out of health...
			if(playerHealth.currentHealth <= 0 || playerHome.currentHomeHealth <= 0) {
                anim.SetTrigger ("GameOver");
            }
        }
    }
}
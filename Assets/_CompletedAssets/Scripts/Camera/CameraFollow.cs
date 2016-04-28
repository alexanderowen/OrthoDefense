using UnityEngine;
using System.Collections;

namespace CompleteProject
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;            // The position that that camera will be following.
        public float smoothing = 5f;        // The speed with which the camera will be following.
		public PhaseManager phaseManager;

        Vector3 offset;                     // The initial offset from the target.

        void Start () {
            // Calculate the initial offset.
            offset = transform.position - target.position;

			phaseManager = GameObject.Find ("HUDCanvas").GetComponent<PhaseManager> ();
        }

        void FixedUpdate () {
			if (phaseManager.IsBuildPhase) {
				transform.Translate(new Vector3(Input.GetAxis("Horizontal") * 6.5f * Time.deltaTime,
											Input.GetAxis("Vertical") * 6.5f * Time.deltaTime, 0.0f));
				return;
			}

            // Create a postion the camera is aiming for based on the offset from the target.
            Vector3 targetCamPos = target.position + offset;

            // Smoothly interpolate between the camera's current position and it's target position.
            transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
        }
    }
}
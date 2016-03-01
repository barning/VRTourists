using UnityEngine;
using System.Collections;

public class RayCaster : MonoBehaviour {

	private GameObject gameController;
	private AudioClip clip;
	private AudioSource audio;

	private bool hasPlayed;

	private CardboardHead head;
	private Vector3 startingPosition;

	// Use this for initialization
	void Start () {
		hasPlayed = false;
		gameController = GameObject.Find ("GameController");
		audio = gameController.GetComponent<AudioSource> ();
		transform.Find ("Canvas").gameObject.SetActive (false);
		head = Camera.main.GetComponent<StereoController>().Head;
		startingPosition = transform.localPosition;

	}
	
	void Update() {

		RaycastHit hit;
		bool isLookedAt = GetComponent<Collider>().Raycast(head.Gaze, out hit, Mathf.Infinity);

		if (isLookedAt) { 
			transform.Find ("Canvas").gameObject.SetActive (true);
			//audio.PlayOneShot(Resources.Load("audio/plop") as AudioClip);
			if (!hasPlayed) {
				audio.Play ();
				hasPlayed = true;
			}
		} else {
			transform.Find ("Canvas").gameObject.SetActive (false);
			hasPlayed = false;
		}
	}
}

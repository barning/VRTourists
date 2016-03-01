using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using System.Collections;

public class Activate : MonoBehaviour {

	public GameObject theSettings;

	public GameObject thePanel;
	public GameObject theInfo;
	public RawImage theInfoImage;

	public Camera hmdEffect;
	public GameObject hmd;
	public AudioClip audioClip;

	public AudioSource speaker;

	public bool speakerEnabled;
	public bool textEnabled;

	float TargetFOV = 35f;
	public float Speed = 0.5f;
	bool firstClick;


	// Use this for initialization
	void Start () {
		firstClick = true;
		speaker = GetComponent<AudioSource> ();
	}

	public void theSpeaker (){
		if(speakerEnabled){
			speakerEnabled = false;
		} else {
			speakerEnabled = true;
		}
	}
	public void theText (){
		if(textEnabled){
			textEnabled = false;
		} else {
			textEnabled = true;
		}
	}

	public void zoom(string buttonName){
		if (firstClick) {
			StartCoroutine (zoomCameraIn ());
			firstClick = false;
			hmd.GetComponent<CardboardHead> ().trackRotation = false;
			hmd.GetComponent<CardboardHead> ().trackPosition = false;
			if (speakerEnabled) {
				audioClip = Resources.Load (buttonName) as AudioClip;
				speaker.clip = audioClip;
				speaker.Play ();
			}
			if (textEnabled) {
				theSettings.SetActive(false);
				hmdEffect.GetComponent<BlurOptimized>().enabled = true;
				theInfo = GameObject.Find(buttonName);
				theInfoImage = theInfo.GetComponent<RawImage> ();
				theInfoImage.enabled = true;
				thePanel.GetComponent<getAnimBool> ().panelShowHide ();
			}
		} else {
			StartCoroutine (zoomCameraOut ());
			firstClick = true;
			hmd.GetComponent<CardboardHead> ().trackRotation = true;
			hmd.GetComponent<CardboardHead> ().trackPosition = true;
			if (speakerEnabled) {
				speaker.Stop ();
			}
			if (textEnabled) {
				hmdEffect.GetComponent<BlurOptimized>().enabled = false;
				thePanel.GetComponent<getAnimBool> ().panelShowHide ();
				theInfoImage.enabled = false;
				theSettings.SetActive(true);
			}
		}
	}

	private IEnumerator zoomCameraIn(){
		float deltaT = 0;
		while (deltaT < Speed) {
			Camera.main.fieldOfView = Mathf.Lerp (Camera.main.fieldOfView, TargetFOV, deltaT / Speed);
			deltaT += Time.deltaTime;
			yield return null;
		}
	}
	private IEnumerator zoomCameraOut(){
		float deltaT = 0;
		while (deltaT < Speed) {
			Camera.main.fieldOfView = Mathf.Lerp (Camera.main.fieldOfView, 60, deltaT / Speed);
			deltaT += Time.deltaTime;
			yield return null;
		}
	}
}

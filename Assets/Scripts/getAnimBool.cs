using UnityEngine;
using System.Collections;

public class getAnimBool : MonoBehaviour {

	Animator anim;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	public void closeAndOpen () {
		bool open = anim.GetBool ("Open");
		if (open) {
			anim.SetBool ("Open", false);
		} else {
			anim.SetBool ("Open", true);
		}
	}
	public void panelShowHide () {
		bool open = anim.GetBool ("IsHidden");
		if (open) {
			anim.SetBool ("IsHidden", false);
		} else {
			anim.SetBool ("IsHidden", true);
		}
	}
}

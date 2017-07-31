using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HostageState))]
[RequireComponent(typeof(HostageMovement))]
public class HostageAnimation : MonoBehaviour {
	public GameObject animatorOwner;

	private HostageState myHostageState;
	private HostageMovement myHostageMovement;
	private Animator myAnimator;

	public void Start() {
		myHostageState = gameObject.GetComponent<HostageState> ();
		myHostageMovement = gameObject.GetComponent<HostageMovement> ();
		myAnimator = animatorOwner.GetComponent<Animator> ();

	}

	public void Update() {
		if (myHostageState.isAlive) {
			float movementSpeed = myHostageMovement.GetVelocity ().magnitude;
			if(myAnimator.GetFloat ("Speed") != movementSpeed)
				myAnimator.SetFloat ("Speed", movementSpeed);
		} else {
			myAnimator.SetTrigger ("Death");
		}
	}
}

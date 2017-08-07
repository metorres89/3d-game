using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorState : MonoBehaviour {

	public GameObject animatorOwner;

	private Animator myAnimator;

	// Use this for initialization
	void Start () {
		myAnimator = animatorOwner.GetComponent<Animator> ();

	}
	
	public void TriggerAnimation(string triggerName, float animationSpeed){

		int animatorDefaultLayer = 0;

		string animatorLayerName = myAnimator.GetLayerName (animatorDefaultLayer);

		AnimatorStateInfo stateInfo = myAnimator.GetCurrentAnimatorStateInfo (animatorDefaultLayer);

		string stateInfoName = string.Format ("{0}.{1}", animatorLayerName, triggerName);

		if (!stateInfo.IsName (stateInfoName)) {
			myAnimator.speed = animationSpeed;
			myAnimator.SetTrigger (triggerName);
		}
	}
}

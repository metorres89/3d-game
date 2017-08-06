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

		//Debug.Log (stateInfoName);

		if (!stateInfo.IsName (stateInfoName)) {
			//Debug.LogFormat ("myAnimator.SetTrigger:{0} myAnimator.speed:{1}", triggerName, animationSpeed);

			myAnimator.speed = animationSpeed;
			myAnimator.SetTrigger (triggerName);
		}
	}
}

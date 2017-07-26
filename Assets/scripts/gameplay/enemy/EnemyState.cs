﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (CapsuleCollider))]
public class EnemyState : MonoBehaviour {

	private float currentHealthPoints = 100.0f;
	private Animator myAnimator;
	private ShowEnemyStats myShowEnemyStats;
	private CapsuleCollider myCapsuleCollider;

	public float initialHealthPoints = 100.0f;
	public bool isAlive;
	public bool isAttacking;

	public GameObject animatorOwner;

	public void Start () {
		currentHealthPoints = initialHealthPoints;

		myShowEnemyStats = gameObject.GetComponent<ShowEnemyStats> ();

		myAnimator = animatorOwner.GetComponent<Animator> ();

		myCapsuleCollider = gameObject.GetComponent<CapsuleCollider> ();

		isAlive = true;
		isAttacking = false;
	}

	public void ReceiveDamage(float damage) {
		currentHealthPoints -= damage;

		currentHealthPoints = Mathf.Clamp (currentHealthPoints, 0.0f, initialHealthPoints);

		myShowEnemyStats.UpdateHealthBar (currentHealthPoints, initialHealthPoints);

		if (currentHealthPoints == 0.0f) {
			Debug.Log ("Enemy is Dead!");

			isAlive = false;

			TriggerAnimation ("dead", 1.0f);

			myCapsuleCollider.enabled = false;
		}
	}

	public float GetHealthPoints(){
		return currentHealthPoints;
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

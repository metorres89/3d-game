using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaState : MonoBehaviour {
	
	private float currentStaminaPoints = 100.0f;
	public float initialStaminaPoints = 100.0f;
	public float minStaminaToRecoverFromTiredState = 25.0f;
	public bool isTired = false;

	void Awake () {
		currentStaminaPoints = initialStaminaPoints;
		if (currentStaminaPoints >= minStaminaToRecoverFromTiredState) {
			isTired = false;
		}
	}
	
	public float GetStaminaPoints(){
		return currentStaminaPoints;
	}

	public void UpdateStaminaPoints(float staminaPoint) {
		currentStaminaPoints += staminaPoint;

		currentStaminaPoints = Mathf.Clamp (currentStaminaPoints, 0.0f, initialStaminaPoints);

		if (currentStaminaPoints == 0.0f) {
			isTired = true;
		} 

		if (currentStaminaPoints >= minStaminaToRecoverFromTiredState) {
			isTired = false;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthState : MonoBehaviour {
	
	public float initialHealthPoints = 100.0f;
	public bool isAlive = true;

	private float currentHealthPoints;

	void Awake () {
		currentHealthPoints = initialHealthPoints;

		if (currentHealthPoints > 0.0f) {
			isAlive = true;
		}
	}

	public void ReceiveDamage(float damage) {
		float newHealthPoints = currentHealthPoints - damage;
		currentHealthPoints = Mathf.Clamp (newHealthPoints, 0.0f, initialHealthPoints);

		if (currentHealthPoints == 0.0f) {
			isAlive = false;
		}
	}

	public void RestoreHealthPoints(float amount) {
		if( amount > 0.0f)
			currentHealthPoints = Mathf.Clamp (currentHealthPoints + amount, 0, initialHealthPoints);
	}

	public float GetHealthPoints(){
		return currentHealthPoints;
	}
}

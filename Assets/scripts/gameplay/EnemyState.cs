using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour {

	[SerializeField] private float healthPoints = 100.0f;
	public float initialHealthPoints = 100.0f;
	public bool isAlive = true;

	public void Start () {
		healthPoints = initialHealthPoints;
	}

	public void ReceiveDamage(float damage) {
		healthPoints -= damage;

		healthPoints = Mathf.Clamp (healthPoints, 0.0f, initialHealthPoints);

		if (healthPoints == 0.0f) {
			Debug.Log ("Enemy is Dead!");
			Destroy (gameObject);
		}
	}
}

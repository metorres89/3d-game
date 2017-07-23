using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour {

	[SerializeField] private float healthPoints = 100.0f;

	private ShowEnemyStats myShowEnemyStats;

	public float initialHealthPoints = 100.0f;
	public bool isAlive = true;

	public void Start () {
		healthPoints = initialHealthPoints;

		myShowEnemyStats = gameObject.GetComponent<ShowEnemyStats> ();
	}

	public void ReceiveDamage(float damage) {
		healthPoints -= damage;

		healthPoints = Mathf.Clamp (healthPoints, 0.0f, initialHealthPoints);

		myShowEnemyStats.UpdateHealthBar (healthPoints, initialHealthPoints);

		if (healthPoints == 0.0f) {
			Debug.Log ("Enemy is Dead!");
			Destroy (gameObject);
		}
	}

	public float GetHealthPoints(){
		return healthPoints;
	}
}

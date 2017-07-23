using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour {

	[SerializeField] private float healthPoints = 100.0f;
	[SerializeField] private float minDamage = 10.0f;
	[SerializeField] private float maxDamage = 30.0f;
	[SerializeField] private float playerDistanceAttackTrigger = 2.0f;

	private GameObject myPlayerRef;

	public float initialHealthPoints = 100.0f;
	public bool isAlive = true;

	public void Start () {
		myPlayerRef = GameObject.FindGameObjectWithTag ("Player");
		healthPoints = initialHealthPoints;
	}
	
	public void FixedUpdate () {

		float distanceFromPlayer = Vector3.Distance (gameObject.transform.position, myPlayerRef.transform.position);

		if (distanceFromPlayer <= playerDistanceAttackTrigger) {
			Attack ();
		} 	
	}

	private void Attack() {
		
		float currentAttackDamage = Random.Range (minDamage, maxDamage);

		PlayerState playerState = myPlayerRef.GetComponent<PlayerState> ();

		Debug.LogFormat ("Enemy is attacking player , player will take damage: {0}", currentAttackDamage);

		playerState.ReceiveDamage (currentAttackDamage);
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

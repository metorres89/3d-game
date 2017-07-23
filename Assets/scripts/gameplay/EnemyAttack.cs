using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {
	[SerializeField] private float minDamage = 10.0f;
	[SerializeField] private float maxDamage = 30.0f;
	[SerializeField] private float playerDistanceAttackTrigger = 2.0f;
	[SerializeField] private float attackTimer = 1.0f;

	private GameObject myPlayerRef;
	private EnemyState myEnemyState;

	public float attackDelay = 1.0f;

	// Use this for initialization
	void Start () {
		myPlayerRef = GameObject.FindGameObjectWithTag ("Player");
		myEnemyState = gameObject.GetComponent<EnemyState> ();
		attackTimer = attackDelay;
	}
	
	public void FixedUpdate () {
		
		if (myEnemyState.isAlive) {
			float distanceFromPlayer = Vector3.Distance (gameObject.transform.position, myPlayerRef.transform.position);

			if (distanceFromPlayer <= playerDistanceAttackTrigger) {

				attackTimer -= Time.fixedDeltaTime;

				if (attackTimer <= 0.0f) {
					Attack ();
					attackTimer = attackDelay;
				}

			} 	
		}
	}

	private void Attack() {

		float currentAttackDamage = Random.Range (minDamage, maxDamage);

		PlayerState playerState = myPlayerRef.GetComponent<PlayerState> ();

		playerState.ReceiveDamage (currentAttackDamage);
	}
}

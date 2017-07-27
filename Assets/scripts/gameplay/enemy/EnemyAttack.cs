using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (EnemyState))]
public class EnemyAttack : MonoBehaviour {
	[SerializeField] private float minDamage = 10.0f;
	[SerializeField] private float maxDamage = 30.0f;
	[SerializeField] private float playerDistanceAttackTrigger = 2.0f;
	[SerializeField] private float attackSpeed = 1.0f;

	private GameObject myPlayerRef;
	private EnemyState myEnemyState;
	private float attackTimer = 1.0f;


	// Use this for initialization
	void Start () {
		myPlayerRef = GameObject.FindGameObjectWithTag ("Player");
		myEnemyState = gameObject.GetComponent<EnemyState> ();
		attackTimer = attackSpeed;
	}
	
	public void FixedUpdate () {
		
		if (myEnemyState.isAlive) {
			
			float distanceFromPlayer = Vector3.Distance (gameObject.transform.position, myPlayerRef.transform.position);

			if (distanceFromPlayer <= playerDistanceAttackTrigger) {

				gameObject.transform.LookAt (myPlayerRef.transform.position);

				attackTimer -= Time.fixedDeltaTime;
				myEnemyState.isAttacking = true;
				myEnemyState.TriggerAnimation ("attack", attackSpeed);
				if (attackTimer <= 0.0f) {
					Attack ();
					attackTimer = attackSpeed;
				}

			} else {
				myEnemyState.isAttacking = false;
			}
		}
	}

	private void Attack() {
		RaycastHit hitInfo;
		bool hasHit = Physics.Raycast (gameObject.transform.position, gameObject.transform.forward, out hitInfo, playerDistanceAttackTrigger);

		if (hasHit) {

			Debug.LogFormat ("El enemigo goleó a algo!: {0}", hitInfo.collider.gameObject.tag);

			if (hitInfo.collider.gameObject.tag == "Player") {
				float currentAttackDamage = Random.Range (minDamage, maxDamage);
				PlayerState playerState = myPlayerRef.GetComponent<PlayerState> ();
				playerState.ReceiveDamage (currentAttackDamage);
			}
		}
	}
}

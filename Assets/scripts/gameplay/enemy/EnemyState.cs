﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Collider))]
public class EnemyState : HealthState {
	private EnemyMovement myEnemyMovement;
	private AnimatorState myAnimatorState;
	private ShowEnemyStats myShowEnemyStats;
	private EnemyItemDrop myEnemyItemDrop;
	private Collider myCollider;
	private bool enemiesAlerted;

	private int alertBitMask;

	[SerializeField] private float alertOtherEnemiesRadius = 10.0f;

	public bool isAttacking;

	public void Start () {
		myEnemyMovement = gameObject.GetComponent<EnemyMovement> ();
		myAnimatorState = gameObject.GetComponent<AnimatorState> ();
		myShowEnemyStats = gameObject.GetComponent<ShowEnemyStats> ();
		myEnemyItemDrop = gameObject.GetComponent<EnemyItemDrop> ();
		myCollider = gameObject.GetComponent<Collider> ();

		isAttacking = false;

		enemiesAlerted = false;

		alertBitMask = 1 << LayerMask.NameToLayer ("Enemy");
	}

	public override void ReceiveDamage(float damage) {
		if (isAlive) {

			base.ReceiveDamage (damage);

			myShowEnemyStats.healthBar.UpdateAmount (base.GetHealthPoints ());

			if (isAlive) {
				myEnemyMovement.StartFollowingPlayer ();
			} else {
				GameplayState.KilledEnemies++;
				myAnimatorState.TriggerAnimation ("dead", 1.0f);
				myEnemyItemDrop.DropRandomItem ();
				myCollider.enabled = false;
				myShowEnemyStats.enabled = false;
				myEnemyMovement.enabled = false;
			}

			AlertNearEnemies ();
		}
	}

	private void AlertNearEnemies() {
		if (enemiesAlerted == false && alertOtherEnemiesRadius > 0.0f) {

			enemiesAlerted = true;

			Collider[] enemiesToAlert = Physics.OverlapSphere (gameObject.transform.position, alertOtherEnemiesRadius, alertBitMask);

			foreach (Collider enemy in enemiesToAlert) {
				EnemyMovement em = enemy.gameObject.GetComponent<EnemyMovement> ();

				if (em != null) {
					em.StartFollowingPlayer ();
				}
			}
		}
	}

	public void SetEnemiesAlerted(bool state)
	{
		enemiesAlerted = state;
	}

	public AnimatorState GetAnimatorState(){
		return myAnimatorState;
	}
}

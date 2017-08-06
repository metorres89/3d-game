using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Collider))]
public class EnemyState : HealthState {
	
	private AnimatorState myAnimatorState;
	private ShowEnemyStats myShowEnemyStats;
	private Collider myCollider;
	private bool enemiesAlerted;

	[SerializeField] private float alertOtherEnemiesRadius = 10.0f;

	public bool isAttacking;

	public void Start () {
		myAnimatorState = gameObject.GetComponent<AnimatorState> ();
		myShowEnemyStats = gameObject.GetComponent<ShowEnemyStats> ();
		myCollider = gameObject.GetComponent<Collider> ();

		isAttacking = false;

		enemiesAlerted = false;
	}

	public override void ReceiveDamage(float damage) {
		base.ReceiveDamage (damage);

		myShowEnemyStats.healthBar.UpdateAmount (base.GetHealthPoints ());

		if (!base.isAlive) {
			Debug.Log ("Enemy is Dead!");
			myAnimatorState.TriggerAnimation ("dead", 1.0f);
			myCollider.enabled = false;
			myShowEnemyStats.enabled = false;
		}

		AlertNearEnemies ();
	}

	private void AlertNearEnemies() {
		if (enemiesAlerted == false && alertOtherEnemiesRadius > 0.0f) {

			enemiesAlerted = true;

			int bitMask = 1 << LayerMask.NameToLayer ("Enemy");
			Collider[] enemiesToAlert = Physics.OverlapSphere (gameObject.transform.position, alertOtherEnemiesRadius, bitMask);

			foreach (Collider enemy in enemiesToAlert) {
				enemy.gameObject.GetComponent<EnemyMovement> ().SetPlayerAsDestination ();
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

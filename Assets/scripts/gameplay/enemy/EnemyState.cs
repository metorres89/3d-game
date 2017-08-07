using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Collider))]
public class EnemyState : HealthState {
	private EnemyMovement myEnemyMovement;
	private AnimatorState myAnimatorState;
	private ShowEnemyStats myShowEnemyStats;
	private Collider myCollider;
	private bool enemiesAlerted;

	private int alertBitMask;

	[SerializeField] private float alertOtherEnemiesRadius = 10.0f;

	public bool isAttacking;

	public void Start () {
		myEnemyMovement = gameObject.GetComponent<EnemyMovement> ();
		myAnimatorState = gameObject.GetComponent<AnimatorState> ();
		myShowEnemyStats = gameObject.GetComponent<ShowEnemyStats> ();
		myCollider = gameObject.GetComponent<Collider> ();

		isAttacking = false;

		enemiesAlerted = false;

		alertBitMask = 1 << LayerMask.NameToLayer ("Enemy");
	}

	public override void ReceiveDamage(float damage) {
		base.ReceiveDamage (damage);

		myShowEnemyStats.healthBar.UpdateAmount (base.GetHealthPoints ());

		if (isAlive) {
			myEnemyMovement.SetPlayerAsDestination ();
		}else{

			myAnimatorState.TriggerAnimation ("dead", 1.0f);
			myCollider.enabled = false;
			myShowEnemyStats.enabled = false;
		}

		AlertNearEnemies ();
	}

	private void AlertNearEnemies() {
		if (enemiesAlerted == false && alertOtherEnemiesRadius > 0.0f) {

			enemiesAlerted = true;

			Collider[] enemiesToAlert = Physics.OverlapSphere (gameObject.transform.position, alertOtherEnemiesRadius, alertBitMask);

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

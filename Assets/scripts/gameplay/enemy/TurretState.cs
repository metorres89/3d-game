using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretState : HealthState {

	private ShowEnemyStats myShowEnemyStats;
	private TurretShoot myTurretShoot;

	void Start () {
		myShowEnemyStats = gameObject.GetComponent<ShowEnemyStats> ();
		myTurretShoot = gameObject.GetComponent<TurretShoot> ();
	}

	public override void ReceiveDamage(float damage) {
		if (isAlive) {
			base.ReceiveDamage (damage);
			myTurretShoot.TargetHasRevealedHisPosition ();
			myShowEnemyStats.healthBar.UpdateAmount (base.GetHealthPoints ());
		}
	}

	public override void OnDead() {
		base.OnDead ();
		GameplayState.KilledEnemies++;
		myShowEnemyStats.enabled = false;
		myTurretShoot.enabled = false;
		DestroyImmediate (myShowEnemyStats);
		DestroyImmediate (myTurretShoot);
	}
}

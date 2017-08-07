﻿using System.Collections;
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
		base.ReceiveDamage (damage);
		myShowEnemyStats.healthBar.UpdateAmount (base.GetHealthPoints ());

		if (!isAlive) {
			myShowEnemyStats.enabled = false;
			myTurretShoot.enabled = false;
		}
	}
}

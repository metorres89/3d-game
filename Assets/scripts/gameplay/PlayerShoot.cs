using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (PlayerState))]
[RequireComponent(typeof (ParticleSystem))]

public class PlayerShoot : MonoBehaviour {

	[SerializeField] private float shootMaxDistance = 500.0f;
	[SerializeField] private float shootDamage = 1.0f;
	[SerializeField] private float shootRate = 0.25f;

	private float myShootTimer;
	private PlayerState myPlayerState;

	public void Start() {
		myPlayerState = gameObject.GetComponent<PlayerState> ();
		myShootTimer = shootRate;
	}

	public void FixedUpdate () {
		if (Input.GetAxis ("Fire1") != 0.0f && myPlayerState.isAlive) {

			myShootTimer -= Time.fixedDeltaTime;

			if (myShootTimer <= 0.0) {
				
				RaycastHit hitInfo;
				bool hasHit = Physics.Raycast (Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, shootMaxDistance);

				if (hasHit) {
					if (hitInfo.collider.gameObject.tag == "Enemy") {
						EnemyState enemyState = hitInfo.collider.gameObject.GetComponent<EnemyState> ();
						enemyState.ReceiveDamage (shootDamage);
					}
				}

				myShootTimer = shootRate;
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

	[SerializeField] private float shootMaxDistance = 500.0f;
	[SerializeField] private float shootDamage = 10.0f;

	private PlayerState myPlayerState;

	public void Start() {
		myPlayerState = gameObject.GetComponent<PlayerState> ();
	}

	public void FixedUpdate () {
		if (Input.GetAxis ("Fire1") != 0.0f && myPlayerState.isAlive) {
			RaycastHit hitInfo;
			bool hasHit = Physics.Raycast (Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, shootMaxDistance);

			if (hasHit) {
				Debug.LogFormat ("we hit this: {0}", hitInfo.collider.gameObject.name);

				if (hitInfo.collider.gameObject.tag == "Enemy") {
					EnemyState enemyState = hitInfo.collider.gameObject.GetComponent<EnemyState> ();

					enemyState.ReceiveDamage (shootDamage);
				}
			}
		}
	}
}

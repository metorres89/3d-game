using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (PlayerState))]
[RequireComponent(typeof (ParticleSystem))]

public class PlayerShoot : MonoBehaviour {

	[SerializeField] private float shootMaxDistance = 500.0f;
	[SerializeField] private float shootDamage = 1.0f;
	[SerializeField] [Range(0.02f, 2.0f)] private float shootRate = 0.02f;
	[SerializeField] int totalAmmoPerPack = 100;
	[SerializeField] int remainingAmmoPacks = 5;
	[SerializeField] float reloadGunDelay = 3.0f;

	private float myShootTimer;
	private PlayerState myPlayerState;
	private int currentAmmo;
	private bool reloadingGun;

	public int GetCurrentAmmo(){
		return currentAmmo;
	}

	public int GetTotalAmmoPerPack(){
		return totalAmmoPerPack;
	}

	public int GetRemainingAmmoPacks(){
		return remainingAmmoPacks;
	}

	public void Start() {
		myPlayerState = gameObject.GetComponent<PlayerState> ();
		myShootTimer = shootRate;
		currentAmmo = totalAmmoPerPack;
		reloadingGun = false;
	}

	public void FixedUpdate () {

		if (myPlayerState.isAlive) {
			
			if (Input.GetAxis ("Fire1") != 0.0f && currentAmmo > 0 && reloadingGun == false) {
				Shoot (Time.fixedDeltaTime);
			}

			if (Input.GetAxis ("Reload") != 0.0f && reloadingGun == false && remainingAmmoPacks > 0 && currentAmmo < totalAmmoPerPack) {
				StartCoroutine(ReloadGun(reloadGunDelay));
			}
		}
	}

	private void Shoot(float deltaTime) {
		if (myShootTimer <= 0.0 || myShootTimer == shootRate) {

			RaycastHit hitInfo;
			bool hasHit = Physics.Raycast (Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, shootMaxDistance);

			if (hasHit) {
				if (hitInfo.collider.gameObject.tag == "Enemy") {
					EnemyState enemyState = hitInfo.collider.gameObject.GetComponent<EnemyState> ();
					enemyState.ReceiveDamage (shootDamage);
				}
			}

			currentAmmo--;
			myShootTimer = shootRate;
		}

		myShootTimer -= deltaTime;
	}

	public IEnumerator ReloadGun(float delay) {
		currentAmmo += (totalAmmoPerPack - currentAmmo);
		remainingAmmoPacks--;
		reloadingGun = true;
		yield return new WaitForSeconds (delay);
		reloadingGun = false;
	}
}

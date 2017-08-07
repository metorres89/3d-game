using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (PlayerState))]
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

	private int shootBitMask;

	public LineRenderer shootLineRenderer;

	public int GetCurrentAmmo(){
		return currentAmmo;
	}

	public int GetTotalAmmoPerPack(){
		return totalAmmoPerPack;
	}

	public int GetRemainingAmmoPacks(){
		return remainingAmmoPacks;
	}

	public void AddAmmoPack(int amount) {
		remainingAmmoPacks += amount;
	}

	public void Start() {
		myPlayerState = gameObject.GetComponent<PlayerState> ();
		myShootTimer = shootRate;
		currentAmmo = totalAmmoPerPack;
		reloadingGun = false;

		int bmEnemy = 1 << LayerMask.NameToLayer ("Enemy");
		int bmDefault = 1 << LayerMask.NameToLayer ("Default");
		shootBitMask = bmEnemy | bmDefault;
	}

	public void FixedUpdate () {
		if (myPlayerState.GetHealthState().isAlive) {
			
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

			GameplayState.TotalShoots++;

			RaycastHit hitInfo;
			bool hasHit = Physics.Raycast (Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, shootMaxDistance, shootBitMask);

			Vector3 targetPosition;

			if (hasHit) {
				
				targetPosition = hitInfo.point;

				if (hitInfo.collider.gameObject.tag == "Enemy") {
					
					GameplayState.SuccessShoots++;

					HealthState enemyHealthState = hitInfo.collider.gameObject.GetComponent<HealthState> ();

					enemyHealthState.ReceiveDamage (shootDamage);

					if (!enemyHealthState.isAlive) {
						GameplayState.KilledEnemies++;
					}
				}

				ParticleSystem currentPS = ParticleSystemManager.GetParticleInstance ("PSShootImpact", GameplayState.TotalShoots);
				currentPS.transform.position = hitInfo.point;
				currentPS.Stop ();
				currentPS.Play ();

			} else {
				targetPosition = Camera.main.transform.forward * shootMaxDistance;
			}

			FXAudio.PlayClip ("fire");
			StartCoroutine(DrawShootLine (targetPosition, shootRate * 0.5f));

			currentAmmo--;
			myShootTimer = shootRate;
		}

		myShootTimer -= deltaTime;
	}

	public IEnumerator ReloadGun(float delay) {

		reloadingGun = true;

		FXAudio.PlayClip ("reload");

		remainingAmmoPacks--;

		int amountToReload = totalAmmoPerPack - currentAmmo;

		yield return new WaitForSecondsRealtime (delay);

		currentAmmo += amountToReload;

		reloadingGun = false;
	}

	public IEnumerator DrawShootLine(Vector3 target, float delay){
		shootLineRenderer.enabled = true;
		shootLineRenderer.SetPosition (0, shootLineRenderer.gameObject.transform.position);
		shootLineRenderer.SetPosition (1, target);
		yield return new WaitForSeconds (delay);
		shootLineRenderer.enabled = false;
	}
}

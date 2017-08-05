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
	/*[SerializeField] GameObject shootImpactPrototype;
	[SerializeField] int shootImpactMaxPoolSize = 10;*/

	private float myShootTimer;
	private PlayerState myPlayerState;
	private int currentAmmo;
	private bool reloadingGun;
	/*private ParticleSystem[] shootImpactPool;*/

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

		/*if(shootImpactPrototype != null) {
			shootImpactPool = new ParticleSystem[shootImpactMaxPoolSize];

			for (int index = 0; index < shootImpactMaxPoolSize; index++) {
				shootImpactPool [index] = Instantiate (shootImpactPrototype, null).GetComponent<ParticleSystem>();
			}	
		}*/
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

			GameplayState.TotalShoots++;

			RaycastHit hitInfo;
			bool hasHit = Physics.Raycast (Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, shootMaxDistance);

			Vector3 targetPosition;

			if (hasHit) {
				
				targetPosition = hitInfo.point;
				if (hitInfo.collider.gameObject.tag == "Enemy") {
					
					EnemyState enemyState = hitInfo.collider.gameObject.GetComponent<EnemyState> ();
					EnemyMovement enemyMovement = hitInfo.collider.gameObject.GetComponent<EnemyMovement> ();

					enemyState.ReceiveDamage (shootDamage);

					GameplayState.SuccessShoots++;

					if (enemyState.isAlive) {
						enemyMovement.SetPlayerAsDestination ();
					} else {
						GameplayState.KilledEnemies++;
					}
				}

				/*if (shootImpactPool.Length > 0) {
					ParticleSystem currentPS = shootImpactPool [GameplayState.TotalShoots % 10];
					currentPS.transform.position = hitInfo.point;
					currentPS.Stop ();
					currentPS.Play ();
				}*/

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

		float delayPerBullet = (delay / totalAmmoPerPack);

		while(currentAmmo < totalAmmoPerPack) {
			currentAmmo++; //+= (totalAmmoPerPack - currentAmmo);
			yield return new WaitForSecondsRealtime (delayPerBullet);
		}

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShoot : MonoBehaviour {
	
	[SerializeField] private float minDistanceToShoot = 10.0f;
	[SerializeField] private float rotationSpeed = 3.0f;
	[SerializeField] private float minDamage = 10.0f;
	[SerializeField] private float maxDamage = 10.0f;
	[SerializeField] private float shootSpeed = 1.0f;
	[SerializeField] private bool fixedDamage = false;

	private PlayerState myPlayerState;
	private Vector3 targetPosition;
	private float targetDistance;
	private GameObject shootOrigin;
	private LineRenderer shootLineRenderer;
	private float shootTimer;

	void Start () {
		myPlayerState = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerState> ();
		shootOrigin = gameObject.transform.Find ("ShootOrigin").gameObject;
		shootLineRenderer = shootOrigin.GetComponent<LineRenderer> ();
		shootTimer = shootSpeed;
	}
	
	void FixedUpdate () {

		targetPosition = myPlayerState.transform.position;

		targetDistance = Vector3.Distance (shootOrigin.transform.position, targetPosition);

		if (targetDistance <= minDistanceToShoot) {

			RotateToTarget (Time.fixedDeltaTime);

			shootTimer -= Time.fixedDeltaTime;

			if (shootTimer <= 0.0f) {
				ShootRayCast ();
				shootTimer = shootSpeed;
			}
		}
	}

	private void RotateToTarget(float deltaTime) {
		Quaternion targetRotation = Quaternion.LookRotation(targetPosition - shootOrigin.transform.position, Vector3.up);
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, deltaTime * rotationSpeed);
	}

	private void ShootRayCast(){

		RaycastHit hitInfo;

		bool hasHit = Physics.Raycast (shootOrigin.transform.position, shootOrigin.transform.forward, out hitInfo, targetDistance);

		//Debug.LogFormat ("ShootRayCast - hasHit:{0} - targetDistance:{1}", hasHit, targetDistance);

		if (hasHit) {

			Debug.LogFormat ("ShootRayCast - el disparo alcanzó el objeto con tag: {0}", hitInfo.collider.gameObject.tag);

			if (hitInfo.collider.gameObject.tag == "Player") {

				float currentDamage = 0.0f;

				if (fixedDamage)
					currentDamage = minDamage;
				else
					currentDamage = Random.Range (minDamage, maxDamage);

				Debug.LogFormat ("ShootRayCast - el player recibirá damage: {0}", currentDamage);

				myPlayerState.GetHealthState().ReceiveDamage (currentDamage);
			}
		}

		FXAudio.PlayClip ("fire");

		StartCoroutine(DrawShootLine (targetPosition, shootSpeed * 0.5f));
	}

	public IEnumerator DrawShootLine(Vector3 target, float delay){
		if (shootLineRenderer != null) {
			shootLineRenderer.enabled = true;
			shootLineRenderer.SetPosition (0, shootLineRenderer.gameObject.transform.position);
			shootLineRenderer.SetPosition (1, target);
			yield return new WaitForSeconds (delay);
			shootLineRenderer.enabled = false;
		}
	}
}

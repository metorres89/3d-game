using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour {

	[SerializeField] private float restoringHealthPoints = 10.0f;

	private PlayerState myPlayerState;

	public void Start () {
		GameObject go = GameObject.FindWithTag ("Player");

		myPlayerState = go.GetComponent<PlayerState> ();
	}

	public void OnTriggerEnter(Collider col) {
		if (col.tag == "Player") {
			myPlayerState.RestoreHealthPoints (restoringHealthPoints);
			this.enabled = false;
			Destroy (gameObject);
		}
	}
}

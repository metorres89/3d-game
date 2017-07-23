using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

	[SerializeField] private float shootMaxDistance = 500.0f;

	void FixedUpdate () {
		if (Input.GetAxis ("Fire1") != 0.0f) {
			RaycastHit hitInfo;
			bool hasHit = Physics.Raycast (Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, shootMaxDistance);

			if (hasHit) {
				Debug.LogFormat ("we hit this: {0}", hitInfo.collider.gameObject.name);
			}
		}
	}
}

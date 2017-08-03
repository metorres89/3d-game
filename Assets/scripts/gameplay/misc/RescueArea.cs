using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescueArea : MonoBehaviour {
	void OnTriggerEnter(Collider col) {
		if (col.tag == "Hostage") {
			GameplayState.RescuedHostages++;
		}
	}
}

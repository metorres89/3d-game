using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescueArea : MonoBehaviour {

	private int rescuedHostagesOnRescueArea;

	public void Start(){
		rescuedHostagesOnRescueArea = 0;
	}

	void OnTriggerEnter(Collider col) {

		Debug.Log (col.name);

		if(col.tag == "Hostage")
			rescuedHostagesOnRescueArea++;
	}

	public int GetRescuedHostagesCount() {
		return rescuedHostagesOnRescueArea;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : MonoBehaviour {

	[SerializeField] private int stock = 1;

	private PlayerShoot myPlayerShoot;

	public void Start () {
		GameObject go = GameObject.FindWithTag ("Player");

		myPlayerShoot = go.GetComponent<PlayerShoot> ();
	}

	public void OnTriggerEnter(Collider col) {
		if (col.tag == "Player") {
			myPlayerShoot.AddAmmoPack (stock);
			this.enabled = false;
			Destroy (gameObject);
		}
	}

}

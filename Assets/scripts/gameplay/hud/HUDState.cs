using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDState : MonoBehaviour {

	private PlayerState myPlayerState;
	private PlayerShoot myPlayerShoot;
	private Text lifeLabel;
	private Text ammoLabel;

	void Start () {
		GameObject goPlayer = GameObject.FindWithTag ("Player");
		myPlayerState = goPlayer.GetComponent<PlayerState> ();
		myPlayerShoot = goPlayer.GetComponent<PlayerShoot> ();
		lifeLabel = gameObject.transform.Find ("LifeLabel").GetComponent<Text> ();
		ammoLabel = gameObject.transform.Find ("AmmoLabel").GetComponent<Text> ();
	}

	void Update () {
		lifeLabel.text = string.Format ("Life: {0}", myPlayerState.GetHealthPoints ());
		ammoLabel.text = string.Format("Ammo: {0} / {1} - {2}", myPlayerShoot.GetCurrentAmmo(), myPlayerShoot.GetTotalAmmoPerPack(), myPlayerShoot.GetRemainingAmmoPacks());
	}
}

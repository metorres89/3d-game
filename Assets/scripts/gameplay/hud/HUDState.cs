using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDState : MonoBehaviour {

	private PlayerState myPlayerState;
	private PlayerShoot myPlayerShoot;
	private Text lifeLabel;
	private Text ammoLabel;
	private Text enemiesKilledLabel;
	private Text rescuedHostagesLabel;

	void Start () {
		GameObject goPlayer = GameObject.FindWithTag ("Player");
		myPlayerState = goPlayer.GetComponent<PlayerState> ();
		myPlayerShoot = goPlayer.GetComponent<PlayerShoot> ();
		lifeLabel = gameObject.transform.Find ("LifeLabel").GetComponent<Text> ();
		ammoLabel = gameObject.transform.Find ("AmmoLabel").GetComponent<Text> ();
		enemiesKilledLabel = gameObject.transform.Find ("EnemiesKilledLabel").GetComponent<Text> ();
		rescuedHostagesLabel = gameObject.transform.Find ("RescuedHostagesLabel").GetComponent<Text> ();
	}

	void Update () {
		
		lifeLabel.text = string.Format ("Life: {0}", myPlayerState.GetHealthPoints ().ToString("F"));
		ammoLabel.text = string.Format("{0} / {1} - {2}", myPlayerShoot.GetCurrentAmmo(), myPlayerShoot.GetTotalAmmoPerPack(), myPlayerShoot.GetRemainingAmmoPacks());
		enemiesKilledLabel.text = string.Format ("{0} / {1}", GameplayState.KilledEnemies , GameplayState.TotalEnemies );
		rescuedHostagesLabel.text = string.Format ("{0} / {1}", GameplayState.RecoveredHostages, GameplayState.TotalHostages);
	}
}

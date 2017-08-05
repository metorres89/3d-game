using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDState : MonoBehaviour {

	private PlayerState myPlayerState;
	private PlayerShoot myPlayerShoot;

	public AmountBar healthBar;
	public AmountBar staminaBar;

	public Text ammoLabel;
	public Text enemiesKilledLabel;
	public Text rescuedHostagesLabel;

	void Start () {
		GameObject goPlayer = GameObject.FindWithTag ("Player");
		myPlayerState = goPlayer.GetComponent<PlayerState> ();
		myPlayerShoot = goPlayer.GetComponent<PlayerShoot> ();

		healthBar.Init ("Life: %", myPlayerState.initialHealthPoints);
		staminaBar.Init ("Stamina: %", myPlayerState.initialStaminaPoints);
	}

	void Update () {

		healthBar.UpdateAmount (myPlayerState.GetHealthPoints ());
		staminaBar.UpdateAmount (myPlayerState.GetStaminaPoints ());

		ammoLabel.text = string.Format("{0} / {1} - {2}", myPlayerShoot.GetCurrentAmmo(), myPlayerShoot.GetTotalAmmoPerPack(), myPlayerShoot.GetRemainingAmmoPacks());
		enemiesKilledLabel.text = string.Format ("{0} / {1}", GameplayState.KilledEnemies , GameplayState.TotalEnemies );
		rescuedHostagesLabel.text = string.Format ("{0} / {1}", GameplayState.RecoveredHostages, GameplayState.TotalHostages);
	}
}

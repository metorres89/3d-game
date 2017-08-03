using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultPanel : MonoBehaviour {

	public Text rescuedHostagesLabel;
	public Text killedEnemiesLabel;
	public Text shootPrecisionLabel;
	public Text totalShootsLabel;

	public Button mainMenuButton;
	public Button replayButton;

	public void Start () {
	
		rescuedHostagesLabel.text = GameplayState.RescuedHostages.ToString();
		killedEnemiesLabel.text = GameplayState.KilledEnemies.ToString();
		shootPrecisionLabel.text = GameplayState.GetShootPrecision ().ToString("F");
		totalShootsLabel.text = GameplayState.TotalShoots.ToString();
	

		mainMenuButton.onClick.AddListener (MainMenu);
		replayButton.onClick.AddListener (Replay);
	}

	private void MainMenu() {
		GameplayState.Reset ();
		SceneManager.LoadScene ("main_menu", LoadSceneMode.Single);
	}

	private void Replay() {
		GameplayState.Reset ();
		SceneManager.LoadScene ("gameplay", LoadSceneMode.Single);
	}
}

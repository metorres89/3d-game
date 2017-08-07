using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultPanel : MonoBehaviour {
	public Text titleLabel;
	public Text rescuedHostagesLabel;
	public Text killedEnemiesLabel;
	public Text shootPrecisionLabel;
	public Text totalShootsLabel;

	public Button mainMenuButton;
	public Button replayButton;

	public AudioClip victoryMusic;
	public AudioClip gameOverMusic;
	public AudioSource musicAudioSource;

	public void Start () {

		if (GameplayState.CurrentState == GameplayState.StateType.GAME_OVER) {
			titleLabel.text = "GAME OVER";
			musicAudioSource.clip = gameOverMusic;
		} else {
			titleLabel.text = "VICTORY";
			musicAudioSource.clip = victoryMusic;
		}

		musicAudioSource.Play ();

		rescuedHostagesLabel.text = GameplayState.RescuedHostages.ToString();
		killedEnemiesLabel.text = GameplayState.KilledEnemies.ToString();

		float precisionAsPercent = GameplayState.GetShootPrecision () * 100.0f;
		shootPrecisionLabel.text = precisionAsPercent.ToString("F");
		totalShootsLabel.text = GameplayState.TotalShoots.ToString();
	

		mainMenuButton.onClick.AddListener (MainMenu);
		replayButton.onClick.AddListener (Replay);

		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
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

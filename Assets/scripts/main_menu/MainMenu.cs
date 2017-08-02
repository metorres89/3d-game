using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public Button startButton;
	public Button settingsButton;

	public void Start () {
		startButton.onClick.AddListener (StartGame);
		settingsButton.onClick.AddListener (Settings);
	}

	public void StartGame() {
		SceneManager.LoadScene ("gameplay", LoadSceneMode.Single);
	}

	public void Settings() {
		Debug.Log ("Settings not implemented yet!");
	}
}

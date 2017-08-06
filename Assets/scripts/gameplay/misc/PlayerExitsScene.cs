using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerExitsScene : MonoBehaviour {

	public bool mustRescueAllHostagesToExitScene = true;

	private GameObject myPlayer;

	[SerializeField] private float minDistanceFromPlayerToExitScene = 5.0f;

	public void Start () {
		myPlayer = GameObject.FindGameObjectWithTag ("Player");
	}
	
	public void Update () {

		float distanceFromPlayer = Vector3.Distance (gameObject.transform.position, myPlayer.transform.position);

		if (distanceFromPlayer <= minDistanceFromPlayerToExitScene && Input.GetAxis("ActiveObject") != 0.0f) {
			if ( (GameplayState.AllHostagesHasBeenRescued() && mustRescueAllHostagesToExitScene == true) || (mustRescueAllHostagesToExitScene == false) ) {
				GameplayState.CurrentState = GameplayState.StateType.WIN;
				GameResult ();
			}
		}
	}

	private void GameResult() {
		SceneManager.LoadScene ("game_result", LoadSceneMode.Single);
	}
}

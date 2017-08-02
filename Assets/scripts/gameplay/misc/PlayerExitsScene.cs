using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerExitsScene : MonoBehaviour {

	private PlayerState myPlayerState;
	private RescueArea myRescueArea;

	[SerializeField] private float minDistanceFromPlayerToExitScene = 5.0f;

	public void Start () {
		myPlayerState = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerState>();
		myRescueArea = GameObject.FindGameObjectWithTag ("RescueAreaTrigger").GetComponent<RescueArea> ();
	}
	
	public void Update () {

		float distanceFromPlayer = Vector3.Distance (gameObject.transform.position, myPlayerState.gameObject.transform.position);

		if (distanceFromPlayer <= minDistanceFromPlayerToExitScene && Input.GetAxis("ActiveObject") != 0.0f) {

			bool allHostagesHasBeenRescued = myPlayerState.GetScoreData ().totalHostages == myRescueArea.GetRescuedHostagesCount ();
			Debug.LogFormat ("allHostagesHasBeenRescued:{0}", allHostagesHasBeenRescued);

			if(allHostagesHasBeenRescued)
				GameResult ();
		}
	}

	private void GameResult() {
		SceneManager.LoadScene ("game_result", LoadSceneMode.Single);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof (HealthState))]
[RequireComponent(typeof (StaminaState))]
public class PlayerState : MonoBehaviour {

	private HealthState myHealthState;
	private StaminaState myStaminaState;
	public float gameOverDelay = 3.0f;

	public void Awake() {
		FXAudio.Init ();
		ParticleSystemManager.Init ();
	}

	public void Start() {

		myHealthState = gameObject.GetComponent<HealthState> ();
		myStaminaState = gameObject.GetComponent<StaminaState> ();

		GameplayState.CurrentState = GameplayState.StateType.PLAYING;
		GameplayState.TotalEnemies = GameObject.FindGameObjectsWithTag ("Enemy").Length;
		GameplayState.TotalHostages = GameObject.FindGameObjectsWithTag ("Hostage").Length;
	}

	public void Update() {
		if (!myHealthState.isAlive) {
			StartCoroutine (GameOver ());
		}
	}

	private IEnumerator GameOver(){
		GameplayState.CurrentState = GameplayState.StateType.GAME_OVER;
		yield return new WaitForSecondsRealtime(gameOverDelay);
		SceneManager.LoadScene("game_result", LoadSceneMode.Single);
	}

	public HealthState GetHealthState(){
		return myHealthState;
	}

	public StaminaState GetStaminaState(){
		return myStaminaState;
	}
}

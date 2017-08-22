using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof (StaminaState))]
public class PlayerState : HealthState {

	private StaminaState myStaminaState;
	public FadeImage bloodFadingImage;
	public float gameOverDelay = 3.0f;

	public override void Awake() {
		base.Awake ();
		FXAudio.Init ();
		ParticleSystemManager.Init ();
	}

	public void Start() {
		myStaminaState = gameObject.GetComponent<StaminaState> ();

		GameplayState.CurrentState = GameplayState.StateType.PLAYING;
		GameplayState.TotalEnemies = GameObject.FindGameObjectsWithTag ("Enemy").Length;
		GameplayState.TotalHostages = GameObject.FindGameObjectsWithTag ("Hostage").Length;
	}

	public void Update() {
		if(!isAlive){
			StartCoroutine (GameOver ());
		}
	}

	private IEnumerator GameOver(){
		GameplayState.CurrentState = GameplayState.StateType.GAME_OVER;
		yield return new WaitForSecondsRealtime(gameOverDelay);
		SceneManager.LoadScene("game_result", LoadSceneMode.Single);
	}

	public StaminaState GetStaminaState(){
		return myStaminaState;
	}

	public override void ReceiveDamage(float damage) {
		if (isAlive) {

			base.ReceiveDamage (damage);

			Debug.Log("we have to display damage effect on screen!");

			bloodFadingImage.FadeInThenFadeOut (0.5f);
		}
	}
}

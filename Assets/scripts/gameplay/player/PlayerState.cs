using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerState : MonoBehaviour {

	private float currentHealthPoints = 100.0f;
	private float currentStaminaPoints = 100.0f;

	public float initialHealthPoints = 100.0f;
	public float initialStaminaPoints = 100.0f;
	public bool isAlive = true;
	public bool isTired = false;
	public float gameOverDelay = 3.0f;

	public void Start() {

		currentHealthPoints = initialHealthPoints;
		currentStaminaPoints = initialStaminaPoints;

		GameplayState.CurrentState = GameplayState.StateType.PLAYING;
		GameplayState.TotalEnemies = GameObject.FindGameObjectsWithTag ("Enemy").Length;
		GameplayState.TotalHostages = GameObject.FindGameObjectsWithTag ("Hostage").Length;

		FXAudio.Init ();
		ParticleSystemManager.Init ();
	}

	public float GetHealthPoints(){
		return currentHealthPoints;
	}

	public float GetStaminaPoints(){
		return currentStaminaPoints;
	}

	public void RestoreHealthPoints(float amount) {
		if( amount > 0.0f)
			currentHealthPoints = Mathf.Clamp (currentHealthPoints + amount, 0, initialHealthPoints);
	}

	public void ReceiveDamage(float damage) {
		currentHealthPoints -= damage;

		currentHealthPoints = Mathf.Clamp (currentHealthPoints, 0.0f, initialHealthPoints);

		if (currentHealthPoints == 0.0f) {
			isAlive = false;
			StartCoroutine (GameOver ());
		}
	}

	public void UpdateStaminaPoints(float staminaPoint) {
		currentStaminaPoints += staminaPoint;

		currentStaminaPoints = Mathf.Clamp (currentStaminaPoints, 0.0f, initialStaminaPoints);

		if (currentStaminaPoints == 0.0f) {
			isTired = true;
		} 

		if (currentStaminaPoints >= 25.0f) {
			isTired = false;
		}
	}
	
	private IEnumerator GameOver(){
		GameplayState.CurrentState = GameplayState.StateType.GAME_OVER;
		yield return new WaitForSecondsRealtime(gameOverDelay);
		SceneManager.LoadScene("game_result", LoadSceneMode.Single);
	}
}

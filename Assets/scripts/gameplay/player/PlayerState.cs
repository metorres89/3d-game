﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerState : MonoBehaviour {

	[SerializeField] private float currentHealthPoints = 100.0f;

	public float initialHealthPoints = 100.0f;

	public bool isAlive = true;

	public float gameOverDelay = 3.0f;

	public float GetHealthPoints(){
		return currentHealthPoints;
	}

	public void RestoreHealthPoints(float amount) {
		if( amount > 0.0f)
			currentHealthPoints = Mathf.Clamp (currentHealthPoints + amount, 0, initialHealthPoints);
	}
		
	public void Start() {
		
		currentHealthPoints = initialHealthPoints;

		GameplayState.CurrentState = GameplayState.StateType.PLAYING;
		GameplayState.TotalEnemies = GameObject.FindGameObjectsWithTag ("Enemy").Length;
		GameplayState.TotalHostages = GameObject.FindGameObjectsWithTag ("Hostage").Length;

		FXAudio.Init ();
	}

	public void ReceiveDamage(float damage) {
		currentHealthPoints -= damage;

		currentHealthPoints = Mathf.Clamp (currentHealthPoints, 0.0f, initialHealthPoints);

		if (currentHealthPoints == 0.0f) {
			isAlive = false;
			StartCoroutine (GameOver ());
		}
	}
	
	private IEnumerator GameOver(){
		GameplayState.CurrentState = GameplayState.StateType.GAME_OVER;
		yield return new WaitForSecondsRealtime(gameOverDelay);
		SceneManager.LoadScene("game_result", LoadSceneMode.Single);
	}
}

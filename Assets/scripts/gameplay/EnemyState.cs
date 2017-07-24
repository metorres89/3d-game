using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour {

	public enum StateType
	{
		IDLE,
		WALK,
		ATTACK,
		DEAD
	}

	private float healthPoints = 100.0f;
	private Animator myAnimator;
	private ShowEnemyStats myShowEnemyStats;

	public float initialHealthPoints = 100.0f;
	public bool isAlive;
	private StateType currentState;

	public void Start () {
		healthPoints = initialHealthPoints;

		myShowEnemyStats = gameObject.GetComponent<ShowEnemyStats> ();

		myAnimator = gameObject.transform.Find ("z@walk").GetComponent<Animator> ();

		isAlive = true;

		SetState (StateType.IDLE);
	}

	public void ReceiveDamage(float damage) {
		healthPoints -= damage;

		healthPoints = Mathf.Clamp (healthPoints, 0.0f, initialHealthPoints);

		myShowEnemyStats.UpdateHealthBar (healthPoints, initialHealthPoints);

		if (healthPoints == 0.0f) {
			Debug.Log ("Enemy is Dead!");

			isAlive = false;

			//Destroy (gameObject);

			//TriggerAnimation("back_fall");
			SetState(StateType.DEAD);
		}
	}

	public float GetHealthPoints(){
		return healthPoints;
	}

	public void SetState (StateType newState)
	{
		if (newState != currentState) {

			currentState = newState;

			string triggerName = "";

			switch (currentState)
			{
			case StateType.IDLE:
				triggerName = "walk";
				break;
			case StateType.WALK:
				triggerName = "walk";
				break;
			case StateType.ATTACK:
				triggerName = "attack";
				break;
			case StateType.DEAD:
				//back_fall , left_fall, right_fall
				triggerName = "back_fall";
				break;
			}

			myAnimator.SetTrigger (triggerName);
		}
	}

	public StateType GetCurrentState () {
		return currentState;
	}
}

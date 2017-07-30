using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostageState : MonoBehaviour {

	public enum StateType {
		CAPTIVE,
		BEING_RESCUED_BY_PLAYER,
		SAFE
	};

	private float healthPoints;
	private StateType state;
	private PlayerState myPlayerState;

	public float initialHealthPoints = 100.0f;
	public bool isAlive;

	void Start () {

		myPlayerState = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerState> ();

		healthPoints = initialHealthPoints;

		isAlive = true;
		state = StateType.CAPTIVE;
	}

	public void ReceiveDamage(float damage) {
		healthPoints = Mathf.Clamp (healthPoints - damage, 0.0f, initialHealthPoints);

		if (healthPoints <= 0.0f) {
			isAlive = false;
		}
	}

	public void SetState(StateType newState) {
		state = newState;
	}

	public StateType GetState() {
		return state;
	}
		
	public void OnTriggerStay(Collider col) {

		Debug.Log ("OnTriggerStay HostageState!!!");

		if (col.tag == "Player" && myPlayerState.isAlive) {
			if (Input.GetAxis ("ActiveObject") != 0.0f) {
				Debug.Log ("hostage change state to bein rescued by player!!!");
				state = StateType.BEING_RESCUED_BY_PLAYER;
			}
		}
	}

}

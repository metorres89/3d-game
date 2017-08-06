using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostageState : MonoBehaviour {

	public enum StateType {
		CAPTIVE,
		BEING_RESCUED_BY_PLAYER,
		SAFE
	};

	public float initialHealthPoints = 100.0f;
	public bool isAlive = true;

	private float healthPoints;
	private StateType state = StateType.CAPTIVE;
	private PlayerState myPlayerState;

	void Start () {
		myPlayerState = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerState> ();
		healthPoints = initialHealthPoints;
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
		if (col.tag == "Player" && myPlayerState.GetHealthState().isAlive) {
			if (Input.GetAxis ("ActiveObject") != 0.0f && state == StateType.CAPTIVE) {
				state = StateType.BEING_RESCUED_BY_PLAYER;
				GameplayState.RecoveredHostages++;
			}
		}
	}
}

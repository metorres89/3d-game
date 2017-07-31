using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof (HostageState))]
[RequireComponent(typeof(NavMeshAgent))]
public class HostageMovement : MonoBehaviour {

	[SerializeField] private float walkSpeed = 3.0f;
	[SerializeField] private float runSpeed = 6.0f;
	[SerializeField] private float triggeringDistanceFromRescueArea = 5.0f;

	private HostageState myHostageState;
	private NavMeshAgent myNavMeshAgent;
	private PlayerState myPlayerState;
	private Transform rescueArea;

	public void Start () {
		myHostageState = gameObject.GetComponent<HostageState> ();
		myNavMeshAgent = gameObject.GetComponent<NavMeshAgent> ();
		myPlayerState = GameObject.Find ("Player").GetComponent<PlayerState>();

		//by default navmesh speed is walking speed
		myNavMeshAgent.speed = walkSpeed;
		rescueArea = GameObject.FindGameObjectWithTag ("RescueArea").transform;
	}
	
	public void FixedUpdate () {

		//while hostage is alive navigation will control movement
		myNavMeshAgent.enabled = myHostageState.isAlive;

		if (myNavMeshAgent.enabled && myHostageState.GetState() == HostageState.StateType.BEING_RESCUED_BY_PLAYER) {
			ProcessMovement (Time.fixedDeltaTime);
		}

	}

	private void ProcessMovement( float deltaTime ) {
		float distanceFromRescueArea = Vector3.Distance (gameObject.transform.position, rescueArea.transform.position);

		if (distanceFromRescueArea <= triggeringDistanceFromRescueArea) {
			if (myNavMeshAgent.speed != runSpeed)
				myNavMeshAgent.speed = runSpeed;
			
			myNavMeshAgent.SetDestination (rescueArea.transform.position);
		} else {
			if (myPlayerState.isAlive) {

				if (myNavMeshAgent.speed != walkSpeed)
					myNavMeshAgent.speed = walkSpeed;

				myNavMeshAgent.SetDestination (myPlayerState.gameObject.transform.position);
			}
		}
	}

	public Vector3 GetVelocity(){
		return myNavMeshAgent.velocity;
	}
}

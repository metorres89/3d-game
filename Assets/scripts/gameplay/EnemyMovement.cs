using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof (EnemyState))]
public class EnemyMovement : MonoBehaviour {

	[SerializeField]private GameObject[] patrolPoints;
	[SerializeField]private float playerDistanceTrigger = 10.0f;
	[SerializeField]private float followingPlayerTimeLength = 10.0f;

	private EnemyState myEnemyState;
	private NavMeshAgent myNavMeshAgent;
	private PlayerState myPlayerState;
	private int patrolPointIndex;
	private float myFollowingPlayerTimer;
	private bool isFollowingPlayer;
	private float walkAnimationSpeed;

	public void Start () {
		myEnemyState = gameObject.GetComponent<EnemyState> ();
		myNavMeshAgent = gameObject.GetComponent<NavMeshAgent> ();
		myPlayerState = GameObject.Find ("Player").GetComponent<PlayerState>();
		patrolPointIndex = 0;
		isFollowingPlayer = false;

		if (patrolPoints.Length == 0) {
			patrolPoints = GameObject.FindGameObjectsWithTag ("PatrolPoint");
		}

		walkAnimationSpeed = myNavMeshAgent.speed;
	}

	public void FixedUpdate () {

		//while enemy is alive navigation will control movement
		myNavMeshAgent.enabled = myEnemyState.isAlive;

		if (myNavMeshAgent.enabled && myEnemyState.isAttacking == false) {
			ProcessMovement (Time.fixedDeltaTime);
		}

	}

	private void ProcessMovement( float deltaTime ) {
		float distanceFromPlayer = Vector3.Distance (gameObject.transform.position, myPlayerState.gameObject.transform.position);

		//Debug.LogFormat ("distanceFromPlayer: {0}", distanceFromPlayer);

		if (distanceFromPlayer <= playerDistanceTrigger && myPlayerState.isAlive) {

			isFollowingPlayer = true;
			myFollowingPlayerTimer = followingPlayerTimeLength;

		}

		if (!isFollowingPlayer) {
			
				myNavMeshAgent.SetDestination (patrolPoints [patrolPointIndex].transform.position);
				
				if (myNavMeshAgent.remainingDistance < myNavMeshAgent.stoppingDistance) {

					patrolPointIndex++;

					if (patrolPointIndex >= patrolPoints.Length)
						patrolPointIndex = 0;
				}
		} else {
			
			if (myPlayerState.isAlive) {

				myNavMeshAgent.SetDestination (myPlayerState.gameObject.transform.position);
				myFollowingPlayerTimer -= deltaTime;
				if (myFollowingPlayerTimer <= 0.0f)
					isFollowingPlayer = false;
			
			} else {
				isFollowingPlayer = false;
			}
		}

		myEnemyState.TriggerAnimation (walkAnimationSpeed >= 5.0f ? "run" : "walk", walkAnimationSpeed);
		//myEnemyState.TriggerAnimation("run", 1.0f);
	}
}

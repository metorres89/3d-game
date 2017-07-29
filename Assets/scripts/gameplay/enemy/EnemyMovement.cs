using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof (EnemyState))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour {

	[SerializeField] private GameObject[] patrolPoints;
	[SerializeField] private float playerDistanceTrigger = 10.0f;
	[SerializeField] private float followingPlayerTimeLength = 10.0f;
	[SerializeField] private float walkSpeed = 1.0f;
	[SerializeField] private float runSpeed = 3.0f;
	[SerializeField] private bool syncNavMeshSpeedAndAnimationSpeed = false;

	private EnemyState myEnemyState;
	private NavMeshAgent myNavMeshAgent;
	private PlayerState myPlayerState;
	private int patrolPointIndex;
	private float myFollowingPlayerTimer;
	private bool isFollowingPlayer;

	public void Start () {
		myEnemyState = gameObject.GetComponent<EnemyState> ();
		myNavMeshAgent = gameObject.GetComponent<NavMeshAgent> ();
		myPlayerState = GameObject.Find ("Player").GetComponent<PlayerState>();
		patrolPointIndex = 0;
		isFollowingPlayer = false;

		if (patrolPoints.Length == 0) {
			patrolPoints = GameObject.FindGameObjectsWithTag ("PatrolPoint");
		}

		//by default navmesh speed is walking speed
		myNavMeshAgent.speed = walkSpeed;
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
			SetPlayerAsDestination ();
		} else {
			myEnemyState.SetEnemiesAlerted (false);
		}

		if (isFollowingPlayer) {
			FollowPlayer (deltaTime);
		} else {
			FollowPatrolPoints ();
		}

		float animationSpeed = syncNavMeshSpeedAndAnimationSpeed ? myNavMeshAgent.speed : 1.0f;
		string animationTrigger = myNavMeshAgent.speed == runSpeed ? "run" : "walk";
		myEnemyState.TriggerAnimation (animationTrigger, animationSpeed);
	}

	public void SetPlayerAsDestination() {
		isFollowingPlayer = true;
		myFollowingPlayerTimer = followingPlayerTimeLength;
	}

	private void FollowPatrolPoints() {
		myNavMeshAgent.speed = walkSpeed;
		myNavMeshAgent.SetDestination (patrolPoints [patrolPointIndex].transform.position);

		if (myNavMeshAgent.remainingDistance < myNavMeshAgent.stoppingDistance) {

			patrolPointIndex++;

			if (patrolPointIndex >= patrolPoints.Length)
				patrolPointIndex = 0;
		}
	}

	private void FollowPlayer(float deltaTime) {
		if (myPlayerState.isAlive) {
			myNavMeshAgent.speed = runSpeed;
			myNavMeshAgent.SetDestination (myPlayerState.gameObject.transform.position);
			myFollowingPlayerTimer -= deltaTime;
			if (myFollowingPlayerTimer <= 0.0f)
				isFollowingPlayer = false;

		} else {
			isFollowingPlayer = false;
		}
	}
}

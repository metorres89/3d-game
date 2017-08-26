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
	[SerializeField] private float patrolPointCheckRadius = 100.0f;

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
			SearchPatrolPoints ();
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

	private void SearchPatrolPoints() {
		
		if (patrolPointCheckRadius > 0.0f) {
			int patrolPointBitMask = 1 << LayerMask.NameToLayer ("PatrolPoint");

			Collider[] colPatrolPoints = Physics.OverlapSphere (gameObject.transform.position, patrolPointCheckRadius, patrolPointBitMask);

			patrolPoints = new GameObject [colPatrolPoints.Length];

			for (int index = 0; index < colPatrolPoints.Length; index++) {
				patrolPoints [index] = colPatrolPoints [index].gameObject;
			}
		} else {
			patrolPoints = GameObject.FindGameObjectsWithTag ("PatrolPoint");
		}


		patrolPointIndex = Random.Range (0, patrolPoints.Length - 1);
	}

	private void ProcessMovement( float deltaTime ) {
		float distanceFromPlayer = Vector3.Distance (gameObject.transform.position, myPlayerState.gameObject.transform.position);

		if (distanceFromPlayer <= playerDistanceTrigger && myPlayerState.isAlive) {
			StartFollowingPlayer ();
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
		myEnemyState.GetAnimatorState().TriggerAnimation (animationTrigger, animationSpeed);
	}

	public void StartFollowingPlayer() {
		isFollowingPlayer = true;
		myFollowingPlayerTimer = followingPlayerTimeLength;
	}

	private void FollowPatrolPoints() {

		if (patrolPoints.Length > 0) {
			
			myNavMeshAgent.speed = walkSpeed;

			myNavMeshAgent.TryToSetNewDestination (patrolPoints [patrolPointIndex].transform.position);

			if (!myNavMeshAgent.pathPending) {
				if (myNavMeshAgent.remainingDistance < myNavMeshAgent.stoppingDistance) {
					patrolPointIndex++;
					if (patrolPointIndex >= patrolPoints.Length) {
						patrolPointIndex = 0;
					}
				}
			}
		}
	}

	private void FollowPlayer(float deltaTime) {
		if (myPlayerState.isAlive) {
			myNavMeshAgent.speed = runSpeed;

			if (myFollowingPlayerTimer == followingPlayerTimeLength) {
				myNavMeshAgent.SetDestination (myPlayerState.gameObject.transform.position);
			} else {
				myNavMeshAgent.TryToSetNewDestination (myPlayerState.gameObject.transform.position);
			}

			myFollowingPlayerTimer -= deltaTime;
			if (myFollowingPlayerTimer <= 0.0f)
				isFollowingPlayer = false;

		} else {
			isFollowingPlayer = false;
		}
	}
}

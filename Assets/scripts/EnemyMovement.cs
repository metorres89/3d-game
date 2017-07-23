using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

	[SerializeField]private GameObject[] patrolPoints;
	[SerializeField]private float playerDistanceTrigger = 5.0f;
	[SerializeField]private float followingPlayerTimeLength = 5.0f;

	private NavMeshAgent myNavMeshAgent;
	private GameObject myPlayerRef;
	private int patrolPointIndex;
	private float myFollowingPlayerTimer;
	private bool isFollowingPlayer;

	void Start () {
		myNavMeshAgent = gameObject.GetComponent<NavMeshAgent> ();
		myPlayerRef = GameObject.Find ("Player");
		patrolPointIndex = 0;
		isFollowingPlayer = false;
	}
	

	void Update () {
		float distanceFromPlayer = Vector3.Distance (gameObject.transform.position, myPlayerRef.transform.position);
		if (distanceFromPlayer <= playerDistanceTrigger && isFollowingPlayer == false) {
			isFollowingPlayer = true;
			myFollowingPlayerTimer = followingPlayerTimeLength;

		} else {
			myNavMeshAgent.SetDestination (patrolPoints [patrolPointIndex].transform.position);

			if (myNavMeshAgent.remainingDistance < myNavMeshAgent.stoppingDistance) {

				patrolPointIndex++;

				if (patrolPointIndex >= patrolPoints.Length)
					patrolPointIndex = 0;
			}
		}


		if (isFollowingPlayer) {
			myNavMeshAgent.SetDestination (myPlayerRef.transform.position);

			myFollowingPlayerTimer -= Time.deltaTime;

			if (myFollowingPlayerTimer <= 0.0f)
				isFollowingPlayer = false;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public static class NavMeshAgentExtension
{

	public static bool TryToSetNewDestination (this NavMeshAgent target, Vector3 newDestination, float minDistance = 1.0f)
	{
		float destinationDifference = Vector3.Distance (target.destination, newDestination);

		if ( (target.pathPending == false && destinationDifference >= minDistance ) || (target.hasPath == false && target.pathPending == false)) {
			target.SetDestination (newDestination);
			return true;
		}

		return false;
	}
}


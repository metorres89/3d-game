using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShoot : MonoBehaviour {

	[SerializeField] private float targetMinDistanceToAttack = 10.0f;

	private PlayerState myPlayerState;

	// Use this for initialization
	void Start () {
		myPlayerState = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerState> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

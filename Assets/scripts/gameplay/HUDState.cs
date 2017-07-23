using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDState : MonoBehaviour {

	private PlayerState myPlayerState;
	private Text lifeLabel;

	// Use this for initialization
	void Start () {
		myPlayerState = GameObject.FindWithTag ("Player").GetComponent<PlayerState> ();
	
		lifeLabel = gameObject.transform.Find ("LifeLabel").GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {

		lifeLabel.text = string.Format ("Life: {0}", myPlayerState.GetHealthPoints ());

	}
}

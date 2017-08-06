using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowEnemyStats : MonoBehaviour {
	private Canvas myCanvas;
	private GameObject myPlayer;
	private EnemyState myEnemyState;
	public AmountBar healthBar;

	public void Start () {
		myCanvas = gameObject.transform.Find ("Canvas").GetComponent<Canvas>();	
		myPlayer = GameObject.Find ("Player");
		myEnemyState = gameObject.GetComponent<EnemyState> ();
		healthBar.Init ("", myEnemyState.initialHealthPoints);
	}

	public void Update () {
		myCanvas.transform.LookAt (myPlayer.transform);
	}

	public void OnDisable() {
		myCanvas.enabled = false;
	}
}

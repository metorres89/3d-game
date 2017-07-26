using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowEnemyStats : MonoBehaviour {
	private Canvas myCanvas;
	private GameObject myPlayer;
	private Image myCurrentHealth;
	private Vector2 originalHPBarSize;

	public void Start () {
		myCanvas = gameObject.transform.Find ("Canvas").GetComponent<Canvas>();	
		myPlayer = GameObject.Find ("Player");
		myCurrentHealth = myCanvas.transform.Find ("HealthBar").Find("CurrentHealth").GetComponent<Image> ();
		originalHPBarSize = myCurrentHealth.rectTransform.sizeDelta;

	}

	public void Update () {
		myCanvas.transform.LookAt (myPlayer.transform);
	}

	public void UpdateHealthBar(float currentHP, float initialHP){
		float newWidth = currentHP * originalHPBarSize.x / initialHP;
		myCurrentHealth.rectTransform.sizeDelta = new Vector2 (newWidth, originalHPBarSize.y);

		myCanvas.enabled = currentHP > 0.0f;
	}
}

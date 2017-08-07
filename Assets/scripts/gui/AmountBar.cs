using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmountBar : MonoBehaviour {

	public Text amountLabel;
	public Image currentAmountImage;
	private float initialValue;
	private string stringFormat;

	private Vector2 originalBarSize;

	public void Init(string format, float initial) {
		initialValue = initial;
		stringFormat = format;
	}

	public void UpdateAmount(float newValue) {

		if (amountLabel != null) {
			string amountText = newValue.ToString ("F");
			string labelText = string.Format ("{0}{1}", stringFormat, amountText);
			amountLabel.text = labelText;
		}

		float newFill = newValue / initialValue;
		currentAmountImage.fillAmount = newFill;
	}

}

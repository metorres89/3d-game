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
		originalBarSize = currentAmountImage.rectTransform.sizeDelta;
	}

	public void UpdateAmount(float newValue) {

		string amountText = newValue.ToString ("F");
		string labelText = string.Format ("{0}{1}", stringFormat, amountText);
		amountLabel.text = labelText;

		float newWidth = newValue * originalBarSize.x / initialValue;
		currentAmountImage.rectTransform.sizeDelta = new Vector2 (newWidth, originalBarSize.y);

	}

}

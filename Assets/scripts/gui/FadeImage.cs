using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour {
	
	[SerializeField] private float initialAlpha = 0.0f;

	private Image myImage;

	void Start () {
		myImage = GetComponent<Image>();

		if(myImage == null)
		{
			Debug.LogError("Error: No image on " + name);
		}

		Color initColor = myImage.canvasRenderer.GetColor ();
		initColor.a = initialAlpha;

		myImage.canvasRenderer.SetColor (initColor);
	}

	public void FadeOut(float delay)
	{
		myImage.CrossFadeAlpha (0.0f , delay, false);
	}

	public void FadeIn(float delay)
	{
		myImage.CrossFadeAlpha (1.0f, delay, false);
	}

	public void FadeInThenFadeOut(float delay) {
		StartCoroutine (ExecuteFadeInThenFadeOut (delay));
	}

	private IEnumerator ExecuteFadeInThenFadeOut(float delay){
		float crossFadeDelay = delay * 0.5f;
		myImage.CrossFadeAlpha (1.0f, crossFadeDelay , false);
		yield return new WaitForSecondsRealtime(crossFadeDelay);
		myImage.CrossFadeAlpha (0.0f, crossFadeDelay, false);
	}
}

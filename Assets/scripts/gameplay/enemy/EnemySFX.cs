using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (AudioSource))]
public class EnemySFX : MonoBehaviour {

	private AudioSource myAudioSource;

	[SerializeField] private AudioClip[] whileIdleClips;
	[SerializeField] private float sfxWhileIdleDelay = 5.0f;
	public bool sfxWhileIdleLoopActive = true;

	[SerializeField] private AudioClip[] hitPlayerClips;

	void Start () {
		myAudioSource = gameObject.GetComponent<AudioSource> ();
		StartCoroutine (StartSFXLoop ());
	}

	private IEnumerator StartSFXLoop(){

		while (sfxWhileIdleLoopActive) {
			if (!myAudioSource.isPlaying) {

				int randomIndex = Random.Range (0, whileIdleClips.Length);

				myAudioSource.clip = whileIdleClips [randomIndex];

				myAudioSource.Play ();
			}

			yield return new WaitForSeconds (sfxWhileIdleDelay);
		}
	}

	public AudioSource GetAudioSource(){
		return myAudioSource;
	}

	public void PlayHitPlayerSFX() {
		int randomIndex = Random.Range (0, hitPlayerClips.Length);
		myAudioSource.PlayOneShot (hitPlayerClips [randomIndex]);
	}

	public void OnDisable(){
		sfxWhileIdleLoopActive = false;
		StopAllCoroutines ();
		myAudioSource.Stop ();
	}
}

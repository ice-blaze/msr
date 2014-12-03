using UnityEngine;
using System.Collections;

public class SoundScript : MonoBehaviour {

	public AudioSource audioSource;
	public float minPitch = 1.8f;
	public float maxPitch = 2.5f;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
		audioSource.pitch = minPitch;
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetAxis("Vertical")>0){
			if(audioSource.pitch >= maxPitch) {return;}
			audioSource.pitch += 0.1f;
		} else if(audioSource.pitch>minPitch){
			audioSource.pitch -= 0.1f;
			if(audioSource.pitch < minPitch){
				audioSource.pitch = minPitch;
			}
		}
	}
}

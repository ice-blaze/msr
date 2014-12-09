using UnityEngine;
using System.Collections;

public class SoundScript : MonoBehaviour {

	AudioSource motorSource;
	AudioSource voiceSource;
	public float minMotorPitch = 0.0f;
	public float maxMotorPitch = 4.5f;
	public AudioClip motor;
	public AudioClip hello;
	public AudioClip alarm;
	public AudioClip bonus;

	// Use this for initialization
	void Start () 
	{
//		audioSource = GetComponents<AudioSource>();
		motorSource = gameObject.AddComponent<AudioSource>();
		voiceSource = gameObject.AddComponent<AudioSource>();
		motorSource.clip = motor;
		motorSource.pitch = minMotorPitch;
		motorSource.loop = true;
		motorSource.playOnAwake = true;
		motorSource.Play();
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetAxis("Vertical")>0||Input.GetAxis("Vertical")<0)
		{
			if(motorSource.pitch >= maxMotorPitch) 
			{
				return;
			}
			if(motorSource.pitch <= minMotorPitch)
			{
				motorSource.pitch = minMotorPitch;
			}
			motorSource.pitch += 0.1f;
		} 
		else if(motorSource.pitch>=minMotorPitch)
		{
			motorSource.pitch -= 0.1f;
			if(motorSource.pitch <= minMotorPitch)
			{
				motorSource.pitch = 0.0f;
			}
		}
	}
}

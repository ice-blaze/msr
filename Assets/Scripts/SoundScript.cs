using UnityEngine;
using System.Collections;

public class SoundScript : MonoBehaviour {

	AudioSource motorSource;
	AudioSource voiceSource;
	AudioSource alarmSource;
	AudioSource bonusSource;
	AudioSource boostSource;

	public float minMotorPitch = 0.0f;
	public float maxMotorPitch = 4.5f;
	public AudioClip motor;
	public AudioClip hello;
	public AudioClip alarm;
	public AudioClip critical;
	public AudioClip bonus;
	public AudioClip boost;

	PlayerController pc;

	// Use this for initialization
	void Start () 
	{
		pc = GetComponent<PlayerController>();
//		audioSource = GetComponents<AudioSource>();
		motorSource = gameObject.AddComponent<AudioSource>();
		voiceSource = gameObject.AddComponent<AudioSource>();
		alarmSource = gameObject.AddComponent<AudioSource>();
		bonusSource = gameObject.AddComponent<AudioSource>();
		boostSource = gameObject.AddComponent<AudioSource>();

		motorSource.clip = motor;
		motorSource.pitch = minMotorPitch;
		motorSource.loop = true;
		motorSource.playOnAwake = true;
		motorSource.Play();

		alarmSource.clip = alarm;
		alarmSource.loop = true;
		alarmSource.pitch = 0.9f;
		alarmSource.volume = 0.25f;

		bonusSource.clip = bonus;

		boostSource.clip = boost;
		boostSource.pitch = 3.0f;
		boostSource.loop = true;

		voiceSource.pitch = 0.6f;
		voiceSource.playOnAwake = false;
	}

	
	// Update is called once per frame
	void Update () 
	{
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

		if (pc.oxygen <= 0.3f)
		{
			if (!alarmSource.isPlaying)
			{
				alarmSource.Play();
				SayCriticalLevel();
			}
		}
		else
		{
			alarmSource.Stop();
		}

		if (Input.GetKeyUp("space"))
		{
			boostSource.Stop();
		}
	}

	public void PlayBoost()
	{
		if(!boostSource.isPlaying)
		{
			boostSource.Play();
		}
	}

	void SayCriticalLevel()
	{
		voiceSource.pitch = 1.0f;
		voiceSource.clip = critical;
		voiceSource.Play();
	}

	void SayHello()
	{
		voiceSource.clip = hello;
		voiceSource.Play();
	}

	public void PlayBonus()
	{
		bonusSource.Play();
	}
}

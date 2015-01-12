using UnityEngine;
using System.Collections;

public class SoundScript : MonoBehaviour {

	AudioSource motorSource;
	AudioSource voiceSource;
	AudioSource alarmSource;
	AudioSource bonusSource;
	AudioSource boostSource;
	AudioSource brakeSource;

	public float minMotorPitch = 0.0f;
	public float maxMotorPitch = 4.5f;
	public AudioClip motor;
	public AudioClip hello;
	public AudioClip alarm;
	public AudioClip critical;
	public AudioClip bonus;
	public AudioClip boost;
	public AudioClip brake;


	PlayerController pc;

	// Use this for initialization
	void Start () 
	{
		pc = GetComponent<PlayerController>();
		motorSource = gameObject.AddComponent<AudioSource>();
		voiceSource = gameObject.AddComponent<AudioSource>();
		alarmSource = gameObject.AddComponent<AudioSource>();
		bonusSource = gameObject.AddComponent<AudioSource>();
		boostSource = gameObject.AddComponent<AudioSource>();
		brakeSource = gameObject.AddComponent<AudioSource>();

		motorSource.clip = motor;
		motorSource.priority = 256;
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
		boostSource.priority = 0;

		brakeSource.clip = brake;
		brakeSource.pitch = 1.0f;
		brakeSource.loop = false;
		brakeSource.priority = 0;

		voiceSource.pitch = 0.6f;
		voiceSource.playOnAwake = false;
	}

	
	// Update is called once per frame
	void Update () 
	{
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
		
		if (Input.GetKeyDown("space"))
		{
			boostSource.Play();
		}
		
		if (Input.GetKeyUp("space"))
		{
			boostSource.Stop();
		}
	}

   public void PlayMotor(float speed)
   {    
      motorSource.pitch = Mathf.Min(speed/150.0f,1) * maxMotorPitch;
   }

	public void PlayBoost()
	{
		if(!boostSource.isPlaying)
		{
			boostSource.Play();
		}
	}

	public void PlayBrake(float speed)
	{
      if(!brakeSource.isPlaying)
      {
         brakeSource.Play();
      }
      brakeSource.volume = 0.2f + Mathf.Min (speed / 120.0f, 0.8f);
   }

   public void StopBrake()
   {
      if(brakeSource.isPlaying)
      {
         brakeSource.Stop();
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

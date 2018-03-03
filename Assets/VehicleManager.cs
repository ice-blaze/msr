using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleManager : MonoBehaviour {

	public float oxygen = 1f; // Between 0 and 1;
	public float oxygenDecPerSec = 0.01f;
	public float oxygenDecPerSecBoost = 0.02f;

	public float oxygenGainedPerCapsule = 0.3f;
	public bool isBoosted = false;
	ArrowManager arrowManager;
	EndUIScript endUIScript;
	TimerManager timerScript;
	SoundScript soundScript;
	VehicleMove vehicleMove;
	bool endLevel = false;

	void Start()
	{
		arrowManager = GetComponentInChildren<ArrowManager>();
		endUIScript = GetComponentInChildren<EndUIScript>();
		timerScript = GetComponentInChildren<TimerManager>();
		vehicleMove = GetComponentInChildren<VehicleMove>();
		soundScript = GetComponent<SoundScript>();

		GameObject respawn = (GameObject)GameObject.FindGameObjectsWithTag("Respawn").GetValue(0);

		transform.position = respawn.transform.position;
		transform.rotation = respawn.transform.rotation;

		timerScript.LaunchTimer();
		endUIScript.ResetTime();
	}

	void OnTriggerEnter(Collider other)
	{
		switch (other.gameObject.tag)
		{
		case "Finish":
			if (endUIScript.Activate())
				this.endLevel = true;
			break;

		case "capsule":
			this.ModifyOxygenByDelta(oxygenGainedPerCapsule);
			other.gameObject.SetActive (false);
			this.soundScript.PlayBonus();
			break;

		case "checkpoint":
			this.arrowManager.RemoveCheckPoint(other);
			break;
		}
	}

	void ModifyOxygenByDelta(float d)
	{
		this.oxygen += d;
		if (this.oxygen > 1f)
			this.oxygen = 1f;
		else if (this.oxygen < 0f)
			this.oxygen = 0f;
	}

	void FixedUpdate()
	{
		if (endLevel)
		{
			return;
		}

		//Play sound of the motor according to the motor speed
		this.soundScript.PlayMotor(80 + vehicleMove.wheelColFR.rpm / 500.0f);

		if (Input.GetButton("Jump"))
		{
			this.isBoosted = true;
			this.ModifyOxygenByDelta (-this.oxygenDecPerSecBoost * Time.deltaTime);
		}
		else
		{
			this.isBoosted = false;
		}

		if(Input.GetButtonDown("LastCheckpoint"))
		{
			transform.position = arrowManager.getLastCheckpoint().position;
			transform.rotation = arrowManager.getLastCheckpoint().rotation;
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		}

	}
	void Update()
	{
		this.ModifyOxygenByDelta(-this.oxygenDecPerSec * Time.deltaTime);

		//If player ran out of oxygen, the game is over
		if(this.oxygen<=0f)
		{
			endLevel=true;
			endUIScript.Oxygenfail();
		}
	}
}

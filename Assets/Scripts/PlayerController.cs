﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public WheelCollider WheelLF;
	public WheelCollider WheelLB;
	public WheelCollider WheelRF;
	public WheelCollider WheelRB;
	public Transform WheelLFTransform;
	public Transform WheelLBTransform;
	public Transform WheelRFTransform;
	public Transform WheelRBTransform;
	public float currentSpeed;
	public float horsePower = 120;
	private float HorsePowerApplied;
	public float brakeFriction = 10;
	public float frictionCoeff = 0;
	public float lowSpeedSteerAngle;
	public float highSpeedSteerAngle;
	private int horseToWatt = 1356;
	float dv = 1f;
	float dh = 1f;

	public ParticleRenderer prLF;
	public ParticleRenderer prLB;
	public ParticleRenderer prRF;
	public ParticleRenderer prRB;

	public float particleSize;

	public Material skidMaterial;
	/*void Update()
   {
   }*/
	
	void Start()
	{
		HorsePowerApplied = horsePower;
		rigidbody.centerOfMass = Vector3.down * 1f;
		currentSpeed = 0.0f;

		dv = Mathf.Abs(WheelLF.transform.position.x - WheelRB.transform.position.x);
		dh = Mathf.Abs(WheelLF.transform.position.z - WheelRB.transform.position.z);
		if (dv == 0)
						dv = 6;
		if (dh == 0)
						dh = 3;


   }


   
   void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.tag == "Finish")
      {
         Application.LoadLevel("menu");
      }
   }
	
	void FixedUpdate()
	{
		currentSpeed = 2*Mathf.PI*WheelLF.radius*Mathf.Abs(WheelLF.rpm*60/1000);
		WheelLF.motorTorque = horseToWatt * HorsePowerApplied / Mathf.Max(currentSpeed, 10f) * Input.GetAxis ("Vertical");
		WheelRF.motorTorque = horseToWatt * HorsePowerApplied / Mathf.Max(currentSpeed, 10f) * Input.GetAxis ("Vertical");
		if (Input.GetKey ("space") == true) {
			HorsePowerApplied = horsePower * 4;
		}
		else 
		{
			HorsePowerApplied = horsePower;		
		}
		
		WheelLF.brakeTorque = currentSpeed * brakeFriction + frictionCoeff;
		WheelRF.brakeTorque = currentSpeed * brakeFriction + frictionCoeff;
		
		float speedFactor = Mathf.Min(rigidbody.velocity.magnitude / 50);
		float currentSteerAngle = Mathf.Lerp(lowSpeedSteerAngle, highSpeedSteerAngle, speedFactor);


		//Steer angle of interior wheel
		currentSteerAngle *= Input.GetAxis("Horizontal");
		if (currentSteerAngle > 0) 
		{
			WheelRF.steerAngle = currentSteerAngle;
			WheelLF.steerAngle = getExternalWheelAngle(currentSteerAngle, dv, dh);
		} 
		else if(currentSteerAngle < 0)
		{
			WheelLF.steerAngle = currentSteerAngle;
			WheelRF.steerAngle = getExternalWheelAngle(currentSteerAngle, dv, dh);
		}
		else
		{
			WheelLF.steerAngle = currentSteerAngle;
			WheelRF.steerAngle = currentSteerAngle;
		}
	}
	void Update()
	{
		WheelLFTransform.Rotate (0,0,WheelLF.rpm / 60 * -360 * Time.deltaTime);
		WheelRFTransform.Rotate (0,0,WheelRF.rpm / 60 * -360 * Time.deltaTime);
		WheelLBTransform.Rotate (0,0,WheelLB.rpm / 60 * -360 * Time.deltaTime);
		WheelRBTransform.Rotate (0,0,WheelRB.rpm / 60 * -360 * Time.deltaTime);
		


		float newLFYAngle = WheelLF.steerAngle;
		float newRFYAngle = WheelRF.steerAngle;

		WheelLFTransform.localEulerAngles = new Vector3(WheelLFTransform.localEulerAngles.x, newLFYAngle, WheelLFTransform.localEulerAngles.z);
		WheelRFTransform.localEulerAngles = new Vector3(WheelRFTransform.localEulerAngles.x, newRFYAngle, WheelRFTransform.localEulerAngles.z);

		WheelPosition ();
	}

	void WheelPosition()
	{
		RaycastHit hit;
		Vector3 wheelPos = new Vector3();
		
		//FL

		if (Physics.Raycast(WheelLF.transform.position, -WheelLF.transform.up, out hit, WheelLF.radius+WheelLF.suspensionDistance) ){
			wheelPos = hit.point+WheelLF.transform.up * WheelLF.radius;
			prLF.maxParticleSize = particleSize*currentSpeed/100*particleSize;
			prLF.particleEmitter.emit = true;
		}
		else {
			wheelPos = WheelLF.transform.position -WheelLF.transform.up* WheelLF.suspensionDistance;
			prLF.particleEmitter.emit = false;
		}
		WheelLFTransform.position = wheelPos;

		if (Physics.Raycast(WheelRF.transform.position, -WheelRF.transform.up, out hit, WheelRF.radius+WheelRF.suspensionDistance) ){
			wheelPos = hit.point+WheelRF.transform.up * WheelRF.radius;
			prRF.maxParticleSize = particleSize*currentSpeed/100*particleSize;
			prRF.particleEmitter.emit = true;
		}
		else {
			wheelPos = WheelRF.transform.position -WheelRF.transform.up* WheelRF.suspensionDistance;
			prRF.particleEmitter.emit = false;
		}
		WheelRFTransform.position = wheelPos;

		if (Physics.Raycast(WheelLB.transform.position, -WheelLB.transform.up, out hit, WheelLB.radius+WheelLB.suspensionDistance) ){
			wheelPos = hit.point+WheelLB.transform.up * WheelLB.radius;
			prLB.maxParticleSize = particleSize*currentSpeed/100*particleSize;
			prLB.particleEmitter.emit = true;
		}
		else {
			wheelPos = WheelLB.transform.position -WheelLB.transform.up* WheelLB.suspensionDistance;
			prLB.particleEmitter.emit = false;
		}
		WheelLBTransform.position = wheelPos;

		if (Physics.Raycast(WheelRB.transform.position, -WheelRB.transform.up, out hit, WheelRB.radius+WheelRB.suspensionDistance) ){
			wheelPos = hit.point+WheelRB.transform.up * WheelRB.radius;
			prRB.maxParticleSize = particleSize*currentSpeed/100*particleSize;
			prRB.particleEmitter.emit = true;
		}
		else {
			wheelPos = WheelRB.transform.position -WheelRB.transform.up* WheelRB.suspensionDistance;
			prRB.particleEmitter.emit = false;
		}
		WheelRBTransform.position = wheelPos;
	
	}

	//Calculate the external wheel angle corresponding to the internal in order to get a perfect direction for a vehicle
	float getExternalWheelAngle(float internalAngle, float verticalWheelBase, float horizontalWheelBase)
	{
		return Mathf.Rad2Deg*Mathf.Atan(verticalWheelBase/((verticalWheelBase/Mathf.Tan(Mathf.Deg2Rad*internalAngle))+horizontalWheelBase));
	}

}


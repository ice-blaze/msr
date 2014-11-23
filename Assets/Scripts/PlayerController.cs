using UnityEngine;
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
	private float currentSpeed;
	public float horsePower = 120;
	public float brakeFriction = 10;
	public float frictionCoeff = 0;
	public float lowSpeedSteerAngle;
	public float highSpeedSteerAngle;
	private float currentSteerAngle;
	private int horseToWatt = 1356;

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
		
		rigidbody.centerOfMass = Vector3.down * 1f;
		currentSpeed = 0.0f;
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
		WheelLF.motorTorque = horseToWatt * horsePower / Mathf.Max(currentSpeed, 10f) * Input.GetAxis ("Vertical");
		WheelRF.motorTorque = horseToWatt * horsePower / Mathf.Max(currentSpeed, 10f) * Input.GetAxis ("Vertical");
		
		WheelLF.brakeTorque = currentSpeed * brakeFriction + frictionCoeff;
		WheelRF.brakeTorque = currentSpeed * brakeFriction + frictionCoeff;
		
		float speedFactor = Mathf.Min(rigidbody.velocity.magnitude / 50);
		float currentSteerAngle = Mathf.Lerp(lowSpeedSteerAngle, highSpeedSteerAngle, speedFactor);
		currentSteerAngle *= Input.GetAxis("Horizontal");
		WheelLF.steerAngle = currentSteerAngle;
		WheelRF.steerAngle = currentSteerAngle;
		
	}
	void Update()
	{
		WheelLFTransform.Rotate (0,0,WheelLF.rpm / 60 * -360 * Time.deltaTime);
		WheelRFTransform.Rotate (0,0,WheelRF.rpm / 60 * -360 * Time.deltaTime);
		WheelLBTransform.Rotate (0,0,WheelLB.rpm / 60 * -360 * Time.deltaTime);
		WheelRBTransform.Rotate (0,0,WheelRB.rpm / 60 * -360 * Time.deltaTime);
		
		
		float newYAngle = WheelLF.steerAngle/* - WheelLFTransform.localEulerAngles.z */;
		
		WheelLFTransform.localEulerAngles = new Vector3(WheelLFTransform.localEulerAngles.x, newYAngle, WheelLFTransform.localEulerAngles.z);
		
		WheelRFTransform.localEulerAngles = new Vector3(WheelRFTransform.localEulerAngles.x, newYAngle, WheelRFTransform.localEulerAngles.z);

		WheelPosition ();
	}

	void WheelPosition()
	{
		RaycastHit hit;
		Vector3 wheelPos = new Vector3();
		
		//FL

		if (Physics.Raycast(WheelLF.transform.position, -WheelLF.transform.up, out hit, WheelLF.radius+WheelLF.suspensionDistance) ){
			wheelPos = hit.point+WheelLF.transform.up * WheelLF.radius;
			prLF.maxParticleSize = particleSize*currentSpeed/100;
		}
		else {
			wheelPos = WheelLF.transform.position -WheelLF.transform.up* WheelLF.suspensionDistance;
			prLF.maxParticleSize = 0;
		}
		WheelLFTransform.position = wheelPos;

		if (Physics.Raycast(WheelRF.transform.position, -WheelRF.transform.up, out hit, WheelRF.radius+WheelRF.suspensionDistance) ){
			wheelPos = hit.point+WheelRF.transform.up * WheelRF.radius;
			prRF.maxParticleSize = particleSize*currentSpeed/100;
		}
		else {
			wheelPos = WheelRF.transform.position -WheelRF.transform.up* WheelRF.suspensionDistance;
			prRF.maxParticleSize = 0;
		}
		WheelRFTransform.position = wheelPos;

		if (Physics.Raycast(WheelLB.transform.position, -WheelLB.transform.up, out hit, WheelLB.radius+WheelLB.suspensionDistance) ){
			wheelPos = hit.point+WheelLB.transform.up * WheelLB.radius;
			prLB.maxParticleSize = particleSize*currentSpeed/100;
		}
		else {
			wheelPos = WheelLB.transform.position -WheelLB.transform.up* WheelLB.suspensionDistance;
			prLB.maxParticleSize = 0;
		}
		WheelLBTransform.position = wheelPos;

		if (Physics.Raycast(WheelRB.transform.position, -WheelRB.transform.up, out hit, WheelRB.radius+WheelRB.suspensionDistance) ){
			wheelPos = hit.point+WheelRB.transform.up * WheelRB.radius;
			prRB.maxParticleSize = particleSize*currentSpeed/100;
		}
		else {
			wheelPos = WheelRB.transform.position -WheelRB.transform.up* WheelRB.suspensionDistance;
			prRB.maxParticleSize = 0;
		}
		WheelRBTransform.position = wheelPos;
	
	}



}


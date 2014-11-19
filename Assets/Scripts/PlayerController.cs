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
	/*void Update()
   {
   }*/
	
	void Start()
	{
		
		rigidbody.centerOfMass = Vector3.down * 2f;
		currentSpeed = 0.0f;
	}
	
	void FixedUpdate()
	{
		currentSpeed = Mathf.Max(2*Mathf.PI*WheelLF.radius*Mathf.Abs(WheelLF.rpm*60/1000), 10f);
		WheelLF.motorTorque = horseToWatt * horsePower / currentSpeed * Input.GetAxis ("Vertical");
		WheelRF.motorTorque = horseToWatt * horsePower / currentSpeed * Input.GetAxis ("Vertical");
		
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
		
	}
}


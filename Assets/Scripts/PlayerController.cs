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
	private int horseToWatt = 1356;
   /*void Update()
   {
   }*/
   
   void Start()
   {

		rigidbody.centerOfMass = Vector3.down * 1f;
		currentSpeed = 0.0f;
   }

   void FixedUpdate()
   {
		currentSpeed = Mathf.Max(2*Mathf.PI*WheelLF.radius*Mathf.Abs(WheelLF.rpm*60/1000), 1f);
		WheelLF.motorTorque = horseToWatt * horsePower / currentSpeed * Input.GetAxis ("Vertical");
		WheelRF.motorTorque = horseToWatt * horsePower / currentSpeed * Input.GetAxis ("Vertical");

		WheelLF.brakeTorque = currentSpeed * brakeFriction + frictionCoeff;
		WheelRF.brakeTorque = currentSpeed * brakeFriction + frictionCoeff;


		WheelLF.steerAngle = 20 * Input.GetAxis("Horizontal");
		WheelRF.steerAngle = 20 * Input.GetAxis("Horizontal");

	
       
//      var movement = new Vector3(moveVertical, 0, -moveHorizontal);
//      this.rigidbody.AddForce(movement * this.speed * Time.deltaTime);
   }
	void Update()
	{
		WheelLFTransform.Rotate (0,WheelLF.rpm / 60 * -360 * Time.deltaTime,0);
		WheelRFTransform.Rotate (0,WheelLF.rpm / 60 * -360 * Time.deltaTime,0);
		WheelLBTransform.Rotate (0,WheelLB.rpm / 60 * -360 * Time.deltaTime,0);
		WheelRBTransform.Rotate (0,WheelLB.rpm / 60 * -360 * Time.deltaTime,0);

		WheelLFTransform.localEulerAngles += Vector3.up * (WheelLF.steerAngle - WheelLFTransform.localEulerAngles.z - 180 - WheelLFTransform.localEulerAngles.y);
		WheelRFTransform.localEulerAngles += Vector3.up * (WheelRF.steerAngle - WheelRFTransform.localEulerAngles.z - 180 - WheelRFTransform.localEulerAngles.y);

	}
}


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
	int maxTorque = 300;
   /*void Update()
   {
   }*/
   
   void Start()
   {

		rigidbody.centerOfMass = Vector3.down * 1f;
   }

   void FixedUpdate()
   {
		WheelLF.motorTorque = maxTorque * Input.GetAxis("Vertical");
		WheelRF.motorTorque = maxTorque * Input.GetAxis("Vertical");
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


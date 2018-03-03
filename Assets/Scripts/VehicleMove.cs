using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMove : MonoBehaviour {

	public float motorForce = 1000.0f;
	public float boostForce = 100.0f;
	private float currentBoostForce = 1.0f;
	public float steerForce = 10.0f;
	public float speed = 0.0f;
	public WheelCollider wheelColFR;
	WheelCollider wheelColBR;
	WheelCollider wheelColFL;
	WheelCollider wheelColBL;
	void Start () {
		this.wheelColFL = GameObject.Find("WheelFL").GetComponent<WheelCollider>();
		this.wheelColFR = GameObject.Find("WheelFR").GetComponent<WheelCollider>();
		this.wheelColBL = GameObject.Find("WheelBL").GetComponent<WheelCollider>();
		this.wheelColBR = GameObject.Find("WheelBR").GetComponent<WheelCollider>();
	}

	void FixedUpdate () {
		float verticalAxis = Input.GetAxis("Vertical");
		speed = verticalAxis * motorForce * this.currentBoostForce;
		if (verticalAxis < 0) {
			speed = -100000;
		}

		wheelColFL.motorTorque = speed;
		wheelColFR.motorTorque = speed;


		float torque = Input.GetAxis("Horizontal") * steerForce;

		wheelColFL.steerAngle = torque;
		wheelColFR.steerAngle = torque;

		if (Input.GetButton("Jump"))
		{
			this.currentBoostForce = this.boostForce;
		}
		else
		{
			this.currentBoostForce = 1.0f;
		}
	}
}

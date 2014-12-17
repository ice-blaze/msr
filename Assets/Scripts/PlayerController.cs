using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
   public float oxygen = 1f; // Between 0 and 1;
   public float oxygenDecreasePerSec = 0.01f;
   public float oxygenDecreasePerSecBoost = 0.02f;

   public float oxygenGainedPerCapsule = 0.3f;
   public bool isBoosted = false;
   public float horsePowerBoostMultiply = 4f;

	public WheelCollider WheelLF;
	public WheelCollider WheelLB;
	public WheelCollider WheelRF;
	public WheelCollider WheelRB;
	public Transform WheelLFTransform;
	public Transform WheelLBTransform;
	public Transform WheelRFTransform;
	public Transform WheelRBTransform;
	public Transform AxleBack;
	public Transform AxleFront;
	public float currentSpeed;
	public float horsePower = 120;
	private float HorsePowerApplied;
	public float brakeFriction = 10;
	public float frictionCoeff = 0;
	public float lowSpeedSteerAngle;
	public float highSpeedSteerAngle;
	private int horseToWatt = 1356;
	private float ElevationLF = 0;
	private float ElevationLB = 0;
	private float ElevationRF = 0;
	private float ElevationRB = 0;
	private Vector3 offsetAxleB;
	private Vector3 offsetAxleF;
	public float BackAltitudeDifference;
	public float BackLatitudeDifference;
	public float FrontAltitudeDifference;
	public float FrontLatitudeDifference;
	public float BackAngle;
	public float FrontAngle;
	float dv = 1f;
	float dh = 1f;

	public ParticleRenderer prLF;
	public ParticleRenderer prLB;
	public ParticleRenderer prRF;
	public ParticleRenderer prRB;

	public float particleSize;

	public Material skidMaterial;

    ArrowManager arrowScript;
    EndUIScript endUIScript;
    TimerManager timerScript;
    bool endLevel = false;
	/*void Update()
   {
   }*/

	void Start()
	{
        arrowScript = GetComponentInChildren<ArrowManager>();
        endUIScript = GetComponentInChildren<EndUIScript>();
        timerScript = GetComponentInChildren<TimerManager>();

		HorsePowerApplied = horsePower;
		rigidbody.centerOfMass = Vector3.down * 1.5f;
		currentSpeed = 0.0f;

		//Compute the vertical and horizontal distance between the wheels in order to make some trigonometry for 
		//wheels steer angles
		dv = Mathf.Abs(WheelLF.transform.position.x - WheelRB.transform.position.x);
		dh = Mathf.Abs (WheelLF.transform.position.z - WheelRB.transform.position.z);
		//Save the base position of both axle in order to modifiy it according to the suspension
		offsetAxleB = AxleBack.localPosition;
		offsetAxleF = AxleFront.localPosition;

   }

    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag) 
        {
        case "Finish":
            if(endUIScript.Activate())
            {
                endLevel=true;
            }
            break;

        case "capsule":
            this.ModifyOxygenByDelta(oxygenGainedPerCapsule);
            other.gameObject.SetActive (false);
            break;

        case "checkpoint":
            this.arrowScript.RemoveCheckPoint(other);
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

        currentSpeed = 2*Mathf.PI*WheelLF.radius*Mathf.Abs(WheelLF.rpm*60/1000);
        WheelLF.motorTorque = horseToWatt * HorsePowerApplied / Mathf.Max(currentSpeed, 10f) * Input.GetAxis ("Vertical");
        WheelRF.motorTorque = horseToWatt * HorsePowerApplied / Mathf.Max(currentSpeed, 10f) * Input.GetAxis ("Vertical");


		if (Input.GetKey ("space")) 
      {
         this.isBoosted = true;
         this.ModifyOxygenByDelta (-this.oxygenDecreasePerSecBoost * Time.deltaTime);
         HorsePowerApplied = horsePower * this.horsePowerBoostMultiply;
		}
		else 
      {
         this.isBoosted = false;
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
      this.ModifyOxygenByDelta (-this.oxygenDecreasePerSec * Time.deltaTime);

		WheelLFTransform.Rotate (0,0,WheelLF.rpm / 60 * -360 * Time.deltaTime);
		WheelRFTransform.Rotate (0,0,WheelRF.rpm / 60 * -360 * Time.deltaTime);
		WheelLBTransform.Rotate (0,0,WheelLB.rpm / 60 * -360 * Time.deltaTime);
		WheelRBTransform.Rotate (0,0,WheelRB.rpm / 60 * -360 * Time.deltaTime);
		
		float newLFYAngle = WheelLF.steerAngle;
		float newRFYAngle = WheelRF.steerAngle;

		WheelLFTransform.localEulerAngles = new Vector3(WheelLFTransform.localEulerAngles.x, newLFYAngle, WheelLFTransform.localEulerAngles.z);
		WheelRFTransform.localEulerAngles = new Vector3(WheelRFTransform.localEulerAngles.x, newRFYAngle, WheelRFTransform.localEulerAngles.z);

		WheelPosition ();
		AxlePosition ();
	}

	void WheelPosition()
	{
		RaycastHit hit;
		Vector3 wheelPos = new Vector3();
		
		//FL

		if (Physics.Raycast(WheelLF.transform.position, -WheelLF.transform.up, out hit, WheelLF.radius+WheelLF.suspensionDistance) ){
			wheelPos = hit.point+WheelLF.transform.up * WheelLF.radius;
			ElevationLF = hit.distance;
			prLF.maxParticleSize = particleSize*currentSpeed/100*particleSize;
			prLF.particleEmitter.emit = true;
		}
		else 
		{
			wheelPos = WheelLF.transform.position -WheelLF.transform.up* WheelLF.suspensionDistance;
			ElevationLF = WheelLF.suspensionDistance+WheelLF.radius;
			prLF.particleEmitter.emit = false;
		}
		WheelLFTransform.position = wheelPos;

		if (Physics.Raycast(WheelRF.transform.position, -WheelRF.transform.up, out hit, WheelRF.radius+WheelRF.suspensionDistance) ){
			wheelPos = hit.point+WheelRF.transform.up * WheelRF.radius;
			ElevationRF = hit.distance;
			prRF.maxParticleSize = particleSize*currentSpeed/100*particleSize;
			prRF.particleEmitter.emit = true;
		}
		else 
		{
			wheelPos = WheelRF.transform.position -WheelRF.transform.up* WheelRF.suspensionDistance;
			ElevationRF = WheelRF.suspensionDistance+WheelRF.radius;
			prRF.particleEmitter.emit = false;
		}
		WheelRFTransform.position = wheelPos;

		if (Physics.Raycast(WheelLB.transform.position, -WheelLB.transform.up, out hit, WheelLB.radius+WheelLB.suspensionDistance) ){
			wheelPos = hit.point+WheelLB.transform.up * WheelLB.radius;
			ElevationLB = hit.distance;
			prLB.maxParticleSize = particleSize*currentSpeed/100*particleSize;
			prLB.particleEmitter.emit = true;
		}
		else 
		{
			wheelPos = WheelLB.transform.position -WheelLB.transform.up* WheelLB.suspensionDistance;
			ElevationLB = WheelLB.suspensionDistance+WheelLB.radius;
			prLB.particleEmitter.emit = false;
		}
		WheelLBTransform.position = wheelPos;

		if (Physics.Raycast(WheelRB.transform.position, -WheelRB.transform.up, out hit, WheelRB.radius+WheelRB.suspensionDistance) ){
			wheelPos = hit.point+WheelRB.transform.up * WheelRB.radius;
			ElevationRB = hit.distance;
			prRB.maxParticleSize = particleSize*currentSpeed/100*particleSize;
			prRB.particleEmitter.emit = true;
		}
		else 
		{
			wheelPos = WheelRB.transform.position -WheelRB.transform.up* WheelRB.suspensionDistance;
			ElevationRB = WheelRB.suspensionDistance+WheelRB.radius;
			prRB.particleEmitter.emit = false;
		}
		WheelRBTransform.position = wheelPos;
	
	}
	//Put the axle with the correct angle, following the wheels
	void AxlePosition()
	{
		//Back axle
		BackAltitudeDifference = ElevationLB-ElevationRB;
		BackLatitudeDifference = Mathf.Abs(WheelRBTransform.transform.localPosition.z-WheelLBTransform.transform.localPosition.z);
		BackAngle = Mathf.Atan (BackAltitudeDifference / BackLatitudeDifference) * Mathf.Rad2Deg;
		AxleBack.localEulerAngles = new Vector3(0,0,BackAngle);
	//	AxleBack.localPosition = new Vector3 (offsetAxleB.x, offsetAxleB.y - (ElevationLB - WheelLB.radius)/2 - BackAltitudeDifference/2, offsetAxleB.z);
		AxleBack.localPosition = new Vector3 (offsetAxleB.x, offsetAxleB.y - ElevationLB + WheelLB.radius, offsetAxleB.z);

		//Front axle
		FrontAltitudeDifference = ElevationLF-ElevationRF;
		FrontLatitudeDifference = Mathf.Abs(WheelRFTransform.transform.localPosition.z-WheelLFTransform.transform.localPosition.z);
		FrontAngle = Mathf.Atan (FrontAltitudeDifference / FrontLatitudeDifference) * Mathf.Rad2Deg;
		AxleFront.localEulerAngles = new Vector3(0,0,FrontAngle);
		AxleFront.localPosition = new Vector3 (offsetAxleF.x, offsetAxleF.y - ElevationLF + WheelLF.radius, offsetAxleF.z);

	}

	//Calculate the external wheel angle corresponding to the internal in order to get a perfect direction for a vehicle
	float getExternalWheelAngle(float internalAngle, float verticalWheelBase, float horizontalWheelBase)
	{
		return Mathf.Rad2Deg*Mathf.Atan(verticalWheelBase/((verticalWheelBase/Mathf.Tan(Mathf.Deg2Rad*internalAngle))+horizontalWheelBase));
	}

}


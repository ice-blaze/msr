using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public Transform car;
	public float distance;
	public float height;
	public float rotationDamping;
	public float heightDamping;
	public float zoomRatio;
	public float defaultFOV;
	private Vector3 rotationVector;
   //	public GameObject player;
   	//Vector3 offset;
	//Quaternion offsetangle;
   
   	// Use this for initialization
   	void Start()
   	{
   	}
   
   	// Update is called once per frame
   	void LateUpdate()
   	{
		float wantedAngle = rotationVector.y;
		float wantedHeight = car.position.y + height;
		float myAngle = transform.eulerAngles.y;
		float myHeight = transform.position.y;
		myAngle = Mathf.LerpAngle(myAngle,wantedAngle,rotationDamping*Time.deltaTime);
		myHeight = Mathf.Lerp(myHeight,wantedHeight,heightDamping*Time.deltaTime);
		Quaternion currentRotation = Quaternion.Euler(0,myAngle,0);
		transform.position = car.position;
		transform.position -= currentRotation*Vector3.forward*distance;
		transform.position += Vector3.up * (myHeight-transform.position.y);
		transform.LookAt(car);


     // 	this.transform.position = this.player.transform.position + this.offset;
	//	this.transform.rotation.Set(this.transform.rotation.x,this.transform.rotation.y,this.offsetangle.z,this.transform.rotation.w);
   	}

	void FixedUpdate ()
	{
		Vector3 localVilocity = car.InverseTransformDirection(car.rigidbody.velocity);
		if (localVilocity.z < -0.1){
			rotationVector.y = car.eulerAngles.y + 180;
		}
		else {
			rotationVector.y = car.eulerAngles.y;
		}
		var acc = car.rigidbody.velocity.magnitude;
		camera.fieldOfView = defaultFOV + acc*zoomRatio;
	}
}


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
	public float sensitivityX;
	public float sensitivityY;
	private Vector3 rotationVector;
	private float cameraXAngleOffset;
	private float cameraYAngleOffset;
	public float cameraXMaxAngle;
	public float cameraYMaxAngle;
	bool isRemotePlayer = false;
   //	public GameObject player;
   	//Vector3 offset;
	//Quaternion offsetangle;
   
   	// Use this for initialization
   	void Awake()
	{
		cameraXAngleOffset = 0;
		cameraYAngleOffset = 0;
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
		cameraXAngleOffset += Input.GetAxis ("Mouse X") * sensitivityX;
		cameraYAngleOffset += Input.GetAxis ("Mouse Y") * sensitivityY;
		if (cameraXAngleOffset > cameraXMaxAngle) 
			cameraXAngleOffset = cameraXMaxAngle;
		if (cameraXAngleOffset < -cameraXMaxAngle) 
			cameraXAngleOffset = -cameraXMaxAngle;
		if (cameraYAngleOffset > cameraYMaxAngle) 
			cameraYAngleOffset = cameraYMaxAngle;
		if (cameraYAngleOffset < -cameraYMaxAngle) 
			cameraYAngleOffset = -cameraYMaxAngle;
		transform.localEulerAngles += cameraXAngleOffset * Vector3.up + cameraYAngleOffset * Vector3.left;



     // 	this.transform.position = this.player.transform.position + this.offset;
	//	this.transform.rotation.Set(this.transform.rotation.x,this.transform.rotation.y,this.offsetangle.z,this.transform.rotation.w);
   	}

	void FixedUpdate ()
	{
		Vector3 localVilocity = car.InverseTransformDirection(car.GetComponent<Rigidbody>().velocity);
		if (localVilocity.z < -0.1){
			rotationVector.y = car.eulerAngles.y + 180;
		}
		else {
			rotationVector.y = car.eulerAngles.y;
		}
		var acc = car.GetComponent<Rigidbody>().velocity.magnitude;
		GetComponent<Camera>().fieldOfView = defaultFOV + acc*zoomRatio;
	}

	public void setCar(Transform g){
		cameraXAngleOffset = 0;
		cameraYAngleOffset = 0;
		car = g;
	}

	public void SetIsRemotePlayer(bool val)
	{
		isRemotePlayer = val;
	}

}


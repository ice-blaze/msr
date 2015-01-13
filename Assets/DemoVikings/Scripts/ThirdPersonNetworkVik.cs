using UnityEngine;
using System.Collections;


public class ThirdPersonNetworkVik : Photon.MonoBehaviour
{
    ThirdPersonCameraNET cameraScript;
    PlayerController controllerScript;

    private bool appliedInitialUpdate;
	Camera cameraVehicle;

    void Awake()
    {
		controllerScript = GetComponent<PlayerController>();
    }
    void Start()
    {
        //TODO: Bugfix to allow .isMine and .owner from AWAKE!
        if (photonView.isMine)
        {
            //MINE: local player, simply enable the local scripts
            controllerScript.enabled = true;
			Camera.main.transform.parent = transform;
			Camera.main.transform.localPosition = new Vector3(0, 2, -10);
			Camera.main.transform.localEulerAngles = new Vector3(10, 0, 0);

			var cam = Camera.main.GetComponent<CameraController>();
			var oxy = Camera.main.GetComponentInChildren<OxygenManager>();
			var arrow = Camera.main.GetComponentInChildren<ArrowManager>();
			var endui = Camera.main.GetComponentInChildren<EndUIScript>();

			oxy.setPlayercontrol(controllerScript);
			cam.setCar(transform);

			cam.enabled = true;
			oxy.enabled = true;
			arrow.enabled = true;
			endui.enabled = true;
        }
        else
        {           
            controllerScript.enabled = true;
        }
        controllerScript.SetIsRemotePlayer(!photonView.isMine);

        gameObject.name = gameObject.name + photonView.viewID;
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //We own this player: send the others our data
           // stream.SendNext((int)controllerScript._characterState);
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(rigidbody.velocity); 

        }
        else
        {
            //Network player, receive data
            //controllerScript._characterState = (CharacterState)(int)stream.ReceiveNext();
            correctPlayerPos = (Vector3)stream.ReceiveNext();
            correctPlayerRot = (Quaternion)stream.ReceiveNext();
            rigidbody.velocity = (Vector3)stream.ReceiveNext();

            if (!appliedInitialUpdate)
            {
                appliedInitialUpdate = true;
                transform.position = correctPlayerPos;
                transform.rotation = correctPlayerRot;
                rigidbody.velocity = Vector3.zero;
            }
        }
    }

    private Vector3 correctPlayerPos = Vector3.zero; //We lerp towards this
    private Quaternion correctPlayerRot = Quaternion.identity; //We lerp towards this

	//TODO TO OPTIMIZE INTERPOLATION LINEAIRE MA GUEULE
//    void Update()
//    {
//        if (!photonView.isMine)
//        {
//            //Update remote player (smooth this, this looks good, at the cost of some accuracy)
//            transform.position = Vector3.Lerp(transform.position, correctPlayerPos, Time.deltaTime * 5);
//            transform.rotation = Quaternion.Lerp(transform.rotation, correctPlayerRot, Time.deltaTime * 5);
//        }
//    }
}
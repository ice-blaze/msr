using UnityEngine;
using System.Collections;

public class StartLevel : MonoBehaviour {

    public GameObject vehicle;

	// Use this for initialization
	void Start () {
        this.vehicle.transform.position = this.transform.position;
        this.vehicle.transform.eulerAngles = this.transform.eulerAngles;
	}
	
	// Update is called once per frame
	/*void Update () {
	
	}*/
}

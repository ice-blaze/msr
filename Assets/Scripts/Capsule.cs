using UnityEngine;
using System.Collections;

public class Capsule : MonoBehaviour
{    
    public float speed = 100; 
    Vector3 rotation = new Vector3(15, 30, 45);

	// Use this for initialization
	void Start() 
    {
        this.transform.Rotate (new Vector3 (30, 0, 0));
	}
	
	// Update is called once per frame
	void Update()
    {	
        //this.transform.Rotate(Vector3.up, this.speed * Time.deltaTime);
        this.transform.Rotate (Vector3.up, Time.deltaTime * this.speed, Space.World);
	}
}

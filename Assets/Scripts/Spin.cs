using UnityEngine;
using System.Collections;

public class Spin : MonoBehaviour{
	public float speed = 4;	
	
	void Update () {
		transform.Rotate(Vector3.up, speed * Time.deltaTime);
	}
}
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OxygenManager : MonoBehaviour {

	public GameObject rover;
	PlayerController pc;

	// Use this for initialization
	void Start () {
		pc = rover.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
<<<<<<< HEAD
		Slider slider = gameObject.GetComponentInChildren<Slider>();
		slider.value = pc.oxygen;
=======
//		Slider slider = gameObject.GetComponent<Slider>();
	//	slider.value = pc.oxygen;
>>>>>>> 302c717474a67f77288ea1416020f948c19e2df7
	}
}

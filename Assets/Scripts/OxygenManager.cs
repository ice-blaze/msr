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
		Slider slider = gameObject.GetComponent<Slider>();
		slider.value = pc.oxygen;
	}
}

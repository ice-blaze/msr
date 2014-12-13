using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OxygenManager : MonoBehaviour {

	GameObject rover;
	PlayerController pc;

	// Use this for initialization
	void Start () {
		pc = GetComponentInParent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		Slider slider = gameObject.GetComponentInChildren<Slider>();
		slider.value = pc.oxygen;
	}
}

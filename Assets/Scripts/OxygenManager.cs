using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OxygenManager : MonoBehaviour {

	public GameObject rover;
	PlayerController pc;

	// Use this for initialization
	void Start () {
		pc = GetComponentInParent<PlayerController>();
        Debug.Log(pc.name);
	}
	
	// Update is called once per frame
	void Update () {
		Slider slider = gameObject.GetComponentInChildren<Slider>();
		slider.value = pc.oxygen;

	}
}

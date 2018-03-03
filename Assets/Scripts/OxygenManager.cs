using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OxygenManager : MonoBehaviour {

	VehicleManager vehicleManager;
	Slider slider;

	void Start() {
		slider = gameObject.GetComponentInChildren<Slider>();
		vehicleManager = gameObject.GetComponentInParent<VehicleManager>();
	}

	void Update () {
		if(vehicleManager) {
			slider.value = vehicleManager.oxygen;
		}
	}
}

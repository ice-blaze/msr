using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OxygenManager : MonoBehaviour {

	PlayerController pc;

	// Update is called once per frame
	void Update () {
		Slider slider = gameObject.GetComponentInChildren<Slider>();
		slider.value = pc.oxygen;
	}

	public void setPlayercontrol(PlayerController g){
		pc=g;
	}
}

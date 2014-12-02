using UnityEngine;
using System.Collections;

public class StartMenuScript : MonoBehaviour {
	InputControllerScript inputController;

	void Start(){
		inputController = GameObject.Find("InputController").GetComponent<InputControllerScript>();
	}

	void animationFinished()
   {
		inputController.isAnimated = false;
	}

	void endStartMenu()
   {
		inputController.QuittedStartMenu();
	}
}

using UnityEngine;
using System.Collections;

public class AboutMenuScript : MonoBehaviour {

	InputControllerScript inputController;
	Animator anima;
	
	void Start(){
		inputController = GameObject.Find("InputController").GetComponent<InputControllerScript>();
		anima = GetComponentInParent<Animator>();
	}
	
	void animationFinished(){
		inputController.isAnimated = false;
	}

	bool ignoreFirstTime = true;
	void endQuitedAnimation(){
		if(ignoreFirstTime){ignoreFirstTime=false;return;}
		inputController.quitedAboutMenu();
	}

	public void quit(){
		anima.SetTrigger("aboutout");
	}
}

using UnityEngine;
using System.Collections;

public class AboutMenuScript : MonoBehaviour
{
	InputControllerScript inputController;
   Animator anima;
   bool ignoreFirstTime = true;
	
	void Start()
   {
		this.inputController = GameObject.Find("InputController").GetComponent<InputControllerScript>();
		this.anima = this.GetComponentInParent<Animator>();
	}
	
	void animationFinished()
   {
		inputController.isAnimated = false;
	}

	void endQuitedAnimation()
   {
		if(ignoreFirstTime){ignoreFirstTime=false;return;}
		inputController.QuittedAboutMenu();
	}

	public void Quit()
   {
		anima.SetTrigger("aboutout");
	}
}

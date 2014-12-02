using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Change color of the button, select/deselect and do the execute part
 */
[RequireComponent(typeof(Renderer))]
public class ButtonScript : MonoBehaviour
{
	public enum Role
   {
      newgame,
      about,
      exit
   };

	public Role role;
	public GameObject selectors;
	public bool isSelected;
	static public GameObject selected = null; 
	public GameObject nextButton;
	public GameObject prevButton;
	public bool isExit = false;
	InputControllerScript inputController;

	void Start()
   {
		// next button et prev can't be empty
		if (nextButton == null || prevButton == null || role == null)
      {
			Debug.LogError("member nextButton or prevButton can't be null");
		}
		
      if (isSelected == true && selected == null)
      {
			selected = gameObject;
			Select ();
		}
		inputController = GameObject.Find("InputController").GetComponent<InputControllerScript>();
	}

	public static void selectActual()
   {
		selected.GetComponent<ButtonScript>().Select();
	}
	
	public void Select()
   {
		isSelected = true;
		selectors.renderer.enabled = true;
	}
	
	public void deselect()
   {
		isSelected = false;
		selectors.renderer.enabled = false;
	}
	
	public void execute()
   {
		switch (role)
      {
		case Role.newgame:
			inputController.QuitStartMenu();
			InputControllerScript.actualMenu = InputControllerScript.Menu.LevelSelection;
//			inputController.startLevelMenu();
			break;
		case Role.about:
			inputController.QuitStartMenu();
			InputControllerScript.actualMenu = InputControllerScript.Menu.About;
			break;
		case Role.exit:
			InputControllerScript.ApplicationQuit();
			break;
		default:
			break;
		}			
	}
}

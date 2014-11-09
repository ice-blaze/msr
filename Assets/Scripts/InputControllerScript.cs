// Manage the inputs from the user.

using UnityEngine;
using System.Collections;

public class InputControllerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			selectNextButton ();
		} else if (Input.GetKeyDown (KeyCode.UpArrow)) {
			selectPrevButton ();
		} else if (Input.GetKeyDown (KeyCode.Return)) {
			getSelected ().execute ();
		} else if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit();
		//	UnityEditor.EditorApplication.isPlaying = false;
		}
	}

	private ButtonScript getSelected(){
		return ButtonScript.selected.GetComponent<ButtonScript>();
	}

	private void selectNextButton(){
		getSelected().deselect();
		ButtonScript.selected = getSelected().nextButton;
		getSelected().select ();
	}

	private void selectPrevButton(){
		getSelected().deselect();
		ButtonScript.selected = getSelected().prevButton;
		getSelected().select ();
	}
}
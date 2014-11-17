// Manage the inputs from the user.

using UnityEngine;
using System.Collections;

public class InputControllerScript : MonoBehaviour {

	public enum Menu{Start,LevelSelection,About};
	public static Menu actualMenu = Menu.Start;

	GameObject startMenu;
	Animator startAnim;
	GameObject levelMenu;
	Animator levelAnim;
	GameObject aboutMenu;
	Animator aboutAnim;

	public bool isAnimated = true;

	LevelMenuScript levelScript;
	AboutMenuScript aboutScript;

	void Start () {
		startMenu = GameObject.Find("StartMenu");
		levelMenu = GameObject.Find("LevelMenu");
		aboutMenu = GameObject.Find("AboutMenu");

		startAnim = startMenu.GetComponent<Animator>();
		levelAnim = levelMenu.GetComponent<Animator>();
		aboutAnim = aboutMenu.GetComponent<Animator>();

		levelScript = levelMenu.GetComponent<LevelMenuScript>();
		aboutScript = aboutMenu.GetComponent<AboutMenuScript>();
	}

	void Update () {
		if(isAnimated){return;}


		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			selectNextButton ();
		} else if (Input.GetKeyDown (KeyCode.UpArrow)) {
			selectPrevButton ();
		} else if (Input.GetKeyDown (KeyCode.Return)) {
			isAnimated = true;
			switch(actualMenu){
			case Menu.Start:
				getSelected().execute();
				break;
			case Menu.LevelSelection:
				levelScript.execute();
				break;
			case Menu.About:
				aboutScript.quit();
				break;
			default:
				break;
			}
		} else if (Input.GetKeyDown (KeyCode.Escape)) {
			isAnimated = true;
			switch(actualMenu){
			case Menu.LevelSelection:
				levelScript.quit();
				break;
			case Menu.About:
				aboutScript.quit();
				break;
			case Menu.Start:
				applicationQuit();
				break;
			default:
				break;
			}
		}
	}

	private ButtonScript getSelected(){
		return ButtonScript.selected.GetComponent<ButtonScript>();
	}

	private void selectNextButton(){
		switch(actualMenu){
			case Menu.Start:
				getSelected().deselect();
				ButtonScript.selected = getSelected().nextButton;
				getSelected().select ();
				break;
			case Menu.LevelSelection:
				isAnimated=true;
				levelScript.nextButton();
				break;
			default:
				break;
		}
	}

	private void selectPrevButton(){
		switch(actualMenu){
			case Menu.Start:
				getSelected().deselect();
				ButtonScript.selected = getSelected().prevButton;
				getSelected().select ();
				break;
			case Menu.LevelSelection:
				isAnimated=true;
				levelScript.previousButton();
				break;
			default:
				break;
		}
	}

	public void startLevelMenu(){
		levelAnim.SetTrigger("levelin");
		renderChild(levelMenu,true);
	}

	public void startStartMenu(){
		startAnim.SetTrigger("menuin");
		renderChild(startMenu,true);
		foreach(GameObject cube in GameObject.FindGameObjectsWithTag("selector")){
			cube.renderer.enabled = false;
		}
		ButtonScript.selectActual();
	}

	public void startAboutMenu(){
		aboutAnim.SetTrigger("aboutin");
		renderChild(aboutMenu,true);
	}

	public void quitLevelMenu(){
		isAnimated=true;
		levelAnim.SetTrigger("levelout");
	}

	public void quitedLevelMenu(){
		renderChild(levelMenu,false);
		actualMenu = Menu.Start;
		launchNextMenu();
	}

	public void quitStartMenu(){
		isAnimated=true;
		startAnim.SetTrigger("menuout");
	}

	public void quitedStartMenu(){
		renderChild(startMenu,false);
		launchNextMenu();
	}

	public void quitAboutMenu(){
		isAnimated=true;
		startAnim.SetTrigger("aboutout");
	}
	
	public void quitedAboutMenu(){
		renderChild(aboutMenu,false);
		actualMenu = Menu.Start;
		launchNextMenu();
	}

	public void launchNextMenu(){
		switch(actualMenu){
			case Menu.Start:
				startStartMenu();
				break;
			case Menu.LevelSelection:
				startLevelMenu();
				break;
			case Menu.About:
				startAboutMenu();
				break;
			default:
				break;
		}
	}

	public static void applicationQuit(){
//		Application.Quit();
		UnityEditor.EditorApplication.isPlaying = false;
	}

	public static void renderChild(GameObject parent,bool isEnabled){
		foreach(Renderer child in parent.GetComponentsInChildren<Renderer>()){
			child.renderer.enabled=isEnabled;
		}
	}
}
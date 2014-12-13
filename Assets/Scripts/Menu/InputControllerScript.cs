using UnityEngine;
using System.Collections;

/**
 * Manage the input into the main menu.
 */
public class InputControllerScript : MonoBehaviour
{
	public enum Menu
   {
      Start,
      LevelSelection,
      About,
   };

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

	void Start()
   {
		this.startMenu = GameObject.Find("StartMenu");
		this.levelMenu = GameObject.Find("LevelMenu");
      this.aboutMenu = GameObject.Find("AboutMenu");

      this.startAnim = this.startMenu.GetComponent<Animator>();
      this.levelAnim = this.levelMenu.GetComponent<Animator>();
      this.aboutAnim = this.aboutMenu.GetComponent<Animator>();

      this.levelScript = this.levelMenu.GetComponent<LevelMenuScript>();
      this.aboutScript = this.aboutMenu.GetComponent<AboutMenuScript>();
	}

	void Update()
   {
      if (this.isAnimated)
         return;

		if (Input.GetKeyDown(KeyCode.DownArrow))
      {
			this.SelectNextButton();
		} 
      else if (Input.GetKeyDown(KeyCode.UpArrow))
      {
			this.SelectPrevButton();
		}
      else if (Input.GetKeyDown(KeyCode.Return))
      {
			this.isAnimated = true;
			switch (actualMenu)
         {
			case Menu.Start:
				this.GetSelected().execute();
				break;
			case Menu.LevelSelection:
            this.levelScript.Execute();
                actualMenu = Menu.Start;
				break;
			case Menu.About:
            this.aboutScript.Quit();
				break;
			default:
				break;
			}
		} 
      else if (Input.GetKeyDown(KeyCode.Escape))
      {
         this.isAnimated = true;
			switch(actualMenu)
         {
			case Menu.LevelSelection:
            this.levelScript.Quit();
				break;
			case Menu.About:
            this.aboutScript.Quit();
				break;
			case Menu.Start:
            this.ApplicationQuit();
				break;
			default:
				break;
			}
		}
	}

	ButtonScript GetSelected()
   {
		return ButtonScript.selected.GetComponent<ButtonScript>();
	}

	void SelectNextButton()
   {
      switch (actualMenu)
      {
		case Menu.Start:
         this.GetSelected().deselect();
         ButtonScript.selected = this.GetSelected().nextButton;
         this.GetSelected().Select();
			break;
		case Menu.LevelSelection:
         this.isAnimated = true;
         this.levelScript.NextButton();
			break;
		default:
			break;
		}
	}

	void SelectPrevButton()
   {
      switch (actualMenu)
      {
		case Menu.Start:
			this.GetSelected().deselect();
			ButtonScript.selected = GetSelected().prevButton;
         this.GetSelected().Select();
			break;
		case Menu.LevelSelection:
         this.isAnimated = true;
         this.levelScript.previousButton();
			break;
		default:
			break;
		}
	}

	public void StartLevelMenu()
   {
      this.levelAnim.SetTrigger("levelin");
      this.RenderChild(levelMenu, true);
	}

	public void StartStartMenu()
   {
      this.startAnim.SetTrigger("menuin");
      this.RenderChild(startMenu, true);
		
      foreach(GameObject cube in GameObject.FindGameObjectsWithTag("selector"))
			cube.renderer.enabled = false;

		ButtonScript.selectActual();
	}

	public void StartAboutMenu()
   {
		this.aboutAnim.SetTrigger("aboutin");
		RenderChild(aboutMenu, true);
	}

	public void quitLevelMenu()
   {
      this.isAnimated = true;
		levelAnim.SetTrigger("levelout");
	}

	public void QuittedLevelMenu()
   {
		RenderChild(levelMenu,false);
		actualMenu = Menu.Start;
		LaunchNextMenu();
	}

	public void QuitStartMenu()
   {
		isAnimated=true;
		startAnim.SetTrigger("menuout");
	}

	public void QuittedStartMenu()
   {
		RenderChild(startMenu,false);
		LaunchNextMenu();
	}

	public void QuitAboutMenu()
   {
		this.isAnimated = true;
		startAnim.SetTrigger("aboutout");
	}
	
	public void QuittedAboutMenu()
   {
		this.RenderChild(aboutMenu, false);
		actualMenu = Menu.Start;
		this.LaunchNextMenu();
	}

	public void LaunchNextMenu()
   {
		switch(actualMenu)      
      {
		case Menu.Start:
         this.StartStartMenu();
			break;
		case Menu.LevelSelection:
         this.StartLevelMenu();
			break;
		case Menu.About:
			this.StartAboutMenu();
			break;
		default:
			break;
		}
	}

	public static void ApplicationQuit()
   {
//		Application.Quit();
		UnityEditor.EditorApplication.isPlaying = false;
	}

	public static void RenderChild(GameObject parent,bool isEnabled)
   {
		foreach(Renderer child in parent.GetComponentsInChildren<Renderer>())
      {
			child.renderer.enabled = isEnabled;
		}
	}
}
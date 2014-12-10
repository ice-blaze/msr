using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Manage the input into the main menu.
 */
public class LevelMenuScript : MonoBehaviour
{
	InputControllerScript inputController;
	Animator anima;
	
	List<string> levels = new List<string>();
	List<Texture2D> images = new List<Texture2D>();
	int actualLevel = 0;
	int numberLevel = 2;

	GameObject image;
	TextMesh levelName;
	TextMesh highscore;
	
	void Start()
   {
		inputController = GameObject.Find("InputController").GetComponent<InputControllerScript>();
		anima = GetComponentInParent<Animator>();

		image = GameObject.Find("level image");
		levelName = GameObject.Find("level name").GetComponent<TextMesh>();
		highscore = GameObject.Find("highscore").GetComponent<TextMesh>();

		levels.Add("KATASTROV");
		images.Add( Resources.Load("Levels/levelimage", typeof(Texture2D)) as Texture2D	);
		levels.Add("BIG JUMP");
		images.Add( Resources.Load("Levels/levelimage", typeof(Texture2D)) as Texture2D);

		RefreshText();
	}

	public void NextButton(){
		anima.SetTrigger("levelout");
		actualLevel = (actualLevel+1)%numberLevel;
	}

	public void Quit()
   {
		anima.SetBool("quit",true);
		anima.SetTrigger("levelout");
	}

	public void previousButton()
   {
		//set all speed -1
//		Debug.Log(animationClips.Length);
//		anima.get
		anima.SetTrigger("levelinmirror");
		actualLevel = (actualLevel)%numberLevel;
	}

	public void endLevelOut()
   {
		Debug.Log("asidasd");
		if(anima.GetBool("quit")){
			anima.SetBool("quit",false);
			inputController.QuittedLevelMenu();
		}
		//change name of froms
//		Debug.Log(highscore);
		RefreshText();
	}

	public void RenderChild(int boolean)
   {
		RenderChild(gameObject,(boolean==0)?true:false);
	}

	public void RefreshText()
   {
		levelName.text = levels[actualLevel];

		image.renderer.material.mainTexture = images[actualLevel];
		highscore.text = "99:99.999";
	}

	void AnimationFinished()
    {
		inputController.isAnimated = false;
	}

	public void Execute()
    {
		Application.LoadLevel(actualLevel);
	}

	public static void RenderChild(GameObject parent,bool isEnabled)
    {
		foreach(Renderer child in parent.GetComponentsInChildren<Renderer>()){
			child.renderer.enabled=isEnabled;
		}
	}
}

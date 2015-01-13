using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Manage the input into the main menu.
 */
//using UnityEditor;


public class LevelMenuScript : MonoBehaviour
{
	public class Level
	{
		public string title;
		public string highscore;
		public Texture2D image;

		public Level(int id){
			string levelFilePath = "Levels/level"+id.ToString()+"/";
			string[] lines = ((TextAsset) Resources.Load(levelFilePath+"highscore")).text.Split('\n');
			image = Resources.Load(levelFilePath+"levelImage", typeof(Texture2D)) as Texture2D;
			title = lines[0];
			HighscoreManager.SetPath(Application.dataPath+"/");
			highscore = HighscoreManager.getHighscoreString();
		}
	}
	
	InputControllerScript inputController;
	Animator anima;
	
	List<Level> levels = new List<Level>();
	int actualLevel = 1;
	int numberLevel = 2;

	GameObject image;
	TextMesh levelName;
	TextMesh highscore;
	
	void Start()
   {
//        AssetDatabase.Refresh();
		inputController = GameObject.Find("InputController").GetComponent<InputControllerScript>();
		anima = GetComponentInParent<Animator>();

		image = GameObject.Find("level image");
		levelName = GameObject.Find("level name").GetComponent<TextMesh>();
		highscore = GameObject.Find("highscore").GetComponent<TextMesh>();

		levels.Add(new Level(0));
		levels.Add(new Level(1));

		RefreshText();
	}

	public void NextButton(){
		anima.SetTrigger("levelout");
		actualLevel++;
		if(actualLevel==numberLevel+1){
			actualLevel=1;
		}
		Debug.Log(actualLevel);
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
		actualLevel--;
		if(actualLevel==-1)
		{
			actualLevel=numberLevel;
		}
		actualLevel = actualLevel;
		anima.SetTrigger("levelinmirror");
	}

	public void endLevelOut()
   {
		if(anima.GetBool("quit")){
			anima.SetBool("quit",false);
			inputController.QuittedLevelMenu();
		}
		//change name of froms

		RefreshText();

	}

	public void RenderChild(int boolean)
   {
		RenderChild(gameObject,(boolean==0)?true:false);
	}

	public void RefreshText()
   {
		levelName.text = levels[actualLevel-1].title;
		image.renderer.material.mainTexture = levels[actualLevel-1].image;
		highscore.text = levels[actualLevel-1].highscore;
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

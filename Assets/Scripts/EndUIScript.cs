using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System.Text.RegularExpressions;

public class EndUIScript : MonoBehaviour {

    bool isActivated = false;
    Image background;
    Text text;
    float time;
    Animator animator;
    ArrowManager arrowScript;
    TimerManager timerManager;
    float startTime;

	void Start () {
        this.background = GetComponentInChildren<Image>();
        this.text = GetComponentInChildren<Text>();
        this.animator = GetComponent<Animator>();
        this.arrowScript = transform.parent.GetComponentInChildren<ArrowManager>();
        this.timerManager = transform.parent.GetComponentInChildren<TimerManager>();
        startTime = Time.time;
	}

    float ActualTime()
    {
        return Time.time-startTime;
    }
	
	void Update () {
        if (this.isActivated && ActualTime()-this.time>2.0f 
            && Input.anyKey )
        {
            Application.LoadLevel("menu");
        }
	}

    public bool Activate()
    {
        if (!arrowScript.PassTroughAllCheckPoints())
        {
            return false;
        }

        this.time = timerManager.Finish();
        this.isActivated = true;

        this.animator.SetTrigger("startend");

        //save in file
        string levelFilePath = "Levels/level"+Application.loadedLevel.ToString()+"/highscore";
        string[] lines = Regex.Split(((TextAsset) Resources.Load(levelFilePath)).text, "\r\n|\r|\n");
        string title = lines[0];
        string highscore = lines[1];

        float bestTime = TimerManager.ConvertStringToTime(highscore);

        Debug.Log(bestTime+" "+this.time);
        if (bestTime>this.time)
        {
            this.text.text = "You beat the highscore !! press any key to continue.";
            //save new highscore
            StreamWriter sw = new StreamWriter("Assets/Resources/"+levelFilePath+".txt");
            sw.WriteLine(title);
            sw.Write(TimerManager.ConvertTimeToString(this.time));
            sw.Close(); 
        }
        else
        {
            this.text.text = "Highscore is still better. press any key to continue.";
        }
        return true;
    }

}

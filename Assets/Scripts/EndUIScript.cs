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
	bool oxygenFail = false;

	void Start () {

		this.background = GetComponentInChildren<Image>();
        this.text = GetComponentInChildren<Text>();
        this.animator = GetComponent<Animator>();
        this.arrowScript = transform.parent.GetComponentInChildren<ArrowManager>();
        this.timerManager = transform.parent.GetComponentInChildren<TimerManager>();
		ResetTime();
	}

	public void ResetTime()
	{
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
		if(isActivated){
			return true;
		}
        if (!arrowScript.PassTroughAllCheckPoints())
        {
            return false;
        }

        this.time = timerManager.Finish();
        this.isActivated = true;

        this.animator.SetTrigger("startend");

		string levelID = Application.loadedLevel.ToString();
		float bestTime = HighscoreManager.getHighscoreFloat(levelID);

        Debug.Log(bestTime+" "+this.time);
        if (bestTime>this.time)
        {
            this.text.text = "You beat the highscore !! press any key to continue.";

			HighscoreManager.setHighscore(this.time, levelID);
        }
        else
        {
            this.text.text = "Highscore is still better. press any key to continue.";
        }
        return true;
    }

	public void Oxygenfail()
	{
		this.animator.SetTrigger("startend");
		this.text.text = "No more oxygen... But try again !!";
		isActivated = true;
	}
}

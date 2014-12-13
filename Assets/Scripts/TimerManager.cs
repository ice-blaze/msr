using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerManager : MonoBehaviour {

	float startTime;
    float endTime;
	Text text;
    bool isFinish;

	// Use this for initialization
	void Start () {
        isFinish = false;
		startTime = Time.time;
		text = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!isFinish)
        {
    		float timer = Time.time-startTime;
    		text.text = string.Format("{0:00}:{1:00.000}",(int)timer / 60,timer%60);
        }
	}

    public void Finish()
    {
        endTime = Time.time;
        isFinish = true;
    }


}

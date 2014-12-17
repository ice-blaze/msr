using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text.RegularExpressions;
using System.Globalization;

public class TimerManager : MonoBehaviour {

	float startTime;
    float endTime;
	Text text;
    bool isFinish;

	// Use this for initialization
	void Start ()
   {
        isFinish = false;
		startTime = Time.time;
		text = gameObject.GetComponent<Text>();
	}

    float ActualTime()
    {
        return Time.time-startTime;
    }
	
	// Update is called once per frame
	void Update () {
        if (!isFinish)
        {
            RefreshText(ActualTime());
        }
	}

    void RefreshText(float timer)
    {
        text.text = ConvertTimeToString(timer);
    }

    public float Finish()
    {
        isFinish = true;
        endTime = ActualTime();
        RefreshText(endTime);
        return endTime;
    }

    public static string ConvertTimeToString(float time)
    {
        return string.Format("{0:00}:{1:00.000}",(int)time / 60,time%60);
    }

    public static float ConvertStringToTime(string time)
    {
        string[] times = Regex.Split(time, @"[:]");
        float result =  float.Parse(times[1], CultureInfo.InvariantCulture.NumberFormat);
        result += int.Parse(times[0], CultureInfo.InvariantCulture.NumberFormat)*60;
        return result;
    }


}

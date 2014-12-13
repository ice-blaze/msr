using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerManager : MonoBehaviour {

	float startTime;
	Text text;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
		text = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		float timer = Time.time-startTime;
		text.text = string.Format("{0:00}:{1:00.000}",(int)timer / 60,timer%60);
	}
}

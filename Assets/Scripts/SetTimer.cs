using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetTimer : MonoBehaviour {

	//public GUIText ytext;
	public float timerMax;
	private int timerInt;
	public bool decreases;
	Text txt;
	void Start () {
		txt = gameObject.GetComponent<Text>(); 
		txt.text = "" + System.Convert.ToString(timerMax);
		if (decreases) { timerMax++;}
	}
	void Update()
	{
		if (decreases) 
		{
						if (timerMax > 0.1) {
								
								timerMax -= Time.deltaTime;
//								Debug.Log (timerMax);
								timerInt = (int)timerMax;
								txt.text = "" + System.Convert.ToString (timerInt);
						}
						//GameOver();
		} 
		else 
		{
			timerMax += Time.deltaTime;
		//	Debug.Log (timerMax);
			txt.text = "" + System.Convert.ToString (timerMax);
		}
	}
}

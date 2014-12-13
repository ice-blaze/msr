using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndUIScript : MonoBehaviour {

    bool isActivated = false;
    Image background;
    Text text;
    float time;
    Animator asdsasd2;


	void Start () {
        background = GetComponentInChildren<Image>();
        text = GetComponentInChildren<Text>();
        asdsasd2 = GetComponent<Animator>();
        Debug.Log(asdsasd2.animation);
	}
	
	void Update () {
        if (isActivated && Time.time-time>2 && Input.anyKey)
        {
            Application.LoadLevel("menu");
        }
	}

    public void Activate()
    {
        time = Time.time;
        isActivated = true;

        asdsasd2.SetTrigger("startend");
//        animation.playAutomatically = true;
        animation.Play("endui");
//        text.color = new Color(text.color.r, text.color.g, text.color.b, 1.0f);
//        background.color = new Color(background.color.r, background.color.g, background.color.b, 0.5f);
    }
}

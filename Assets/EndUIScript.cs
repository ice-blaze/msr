using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndUIScript : MonoBehaviour {

    bool isActivated = false;
    Image background;
    Text text;
    float time;
    Animator animator;


	void Start () {
        this.background = GetComponentInChildren<Image>();
        this.text = GetComponentInChildren<Text>();
        this.animator = GetComponent<Animator>();
	}
	
	void Update () {
        if (this.isActivated && Time.time-this.time>2 && Input.anyKey)
        {
            Application.LoadLevel("menu");
        }
	}

    public void Activate()
    {
        this.time = Time.time;
        this.isActivated = true;

        this.animator.SetTrigger("startend");
    }
}

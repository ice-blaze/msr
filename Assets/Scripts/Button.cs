﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Button : MonoBehaviour {
	
	public GameObject selectors;
	public bool isSelected;
	static public GameObject selected = null; 
	public GameObject nextButton;
	public GameObject prevButton;
	public int levelIndex = 0;

	private void Start(){
		// next button et prev can't be empty
		if (nextButton == null || prevButton == null) {
			Debug.LogError("member nextButton or prevButton can't be null");
		}

		if (isSelected == true && selected == null) {
			selected = gameObject;
			select ();
		}
	}
	
	public void select(){
		isSelected = true;
		renderer.material.color = Color.yellow;
		selectors.renderer.enabled = true;
	}
	
	public void deselect(){
		isSelected = false;
		renderer.material.color = Color.white;
		selectors.renderer.enabled = false;
	}

	public void execute(){
		Application.LoadLevel(levelIndex);
	}
	
//	void OnMouseEnter() {
//		select ();
//	}
//	
//	void OnMouseExit(){
//		deselect ();
//	}
}

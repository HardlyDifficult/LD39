using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLevelNumber : MonoBehaviour {

	protected void Start () {
    Text text = GetComponent<Text>();
    LevelManager levelManager = GameObject.FindObjectOfType<LevelManager>();
    text.text = "Level " + levelManager.thisLevelNumber;
	}
	
}

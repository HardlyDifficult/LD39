using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextHarvestGoal : MonoBehaviour
{
  protected void Start()
  {
    Text text = GetComponent<Text>();
    LevelManager levelManager = GameObject.FindObjectOfType<LevelManager>();
    text.text = "Goal: Harvest at least " + (levelManager.percentHarvestToProceed * 100).ToString("N0") + "%";
  }
}

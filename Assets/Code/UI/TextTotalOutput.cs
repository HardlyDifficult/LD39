using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTotalOutput : MonoBehaviour
{
  LevelManager levelManager;
  Text text;

  protected void Awake()
  {
    text = GetComponent<Text>();
    levelManager = GameObject.FindObjectOfType<LevelManager>();
  }

  protected void Update()
  {
    float percentOfNetwork = (float)GameController.instance.totalOutput / Machine.initialNetworkPotential * 100;
    text.text = "Harvested: " 
      + percentOfNetwork.ToString("N2") + "%"
      + " / "
      + (levelManager.percentHarvestToProceed * 100).ToString("N0") + "%"
      + " ("
      + GameController.instance.totalOutput.ToString("N0") 
      + " kW)";
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTotalOutput : MonoBehaviour
{
  Text text;

  protected void Awake()
  {
    text = GetComponent<Text>();
  }

  protected void Update()
  {
    float percentOfNetwork = (float)GameController.instance.totalOutput / Machine.initialNetworkPotential * 100;
    text.text = "Harvested: " + GameController.instance.totalOutput.ToString("N0") + " kW ("
      + percentOfNetwork.ToString("N2") + "%)";
  }
}

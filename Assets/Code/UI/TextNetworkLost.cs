using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextNetworkLost : MonoBehaviour {

  Text text;

  protected void Awake()
  {
    text = GetComponent<Text>();
  }

  protected void Update()
  {
    float amountLost = (float)(Machine.initialNetworkPotential - Machine.totalNetworkPotential - GameController.instance.totalOutput);
  
    float percentOfNetwork = (float)amountLost / Machine.initialNetworkPotential * 100;
    text.text = "Lost: " + amountLost.ToString("N0") + " kW ("
      + percentOfNetwork.ToString("N2") + "%)";
  }
}

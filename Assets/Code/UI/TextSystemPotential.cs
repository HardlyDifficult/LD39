using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSystemPotential : MonoBehaviour
{

  Text text;

  protected void Awake()
  {
    text = GetComponent<Text>();
  }



  protected void Update()
  {
    text.text = "Potential: " + Machine.totalNetworkPotential.ToString("N0") + " kW ("
      + (Machine.percentNetworkPotential * 100).ToString("N2") + "%)";
  }
}

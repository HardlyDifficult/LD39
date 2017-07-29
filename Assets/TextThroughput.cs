using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextThroughput : MonoBehaviour
{
  Text text;

  protected void Awake()
  {
    text = GetComponent<Text>();
  }

  protected void FixedUpdate()
  {
    text.text = "Throughput: "
      + GameController.instance.throughputLastUpdate.ToString("N0")
      + " kWh";
  }
}

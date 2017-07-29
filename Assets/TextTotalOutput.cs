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
    text.text = GameController.instance.totalOutput.ToString("N0") + " kW";
  }
}

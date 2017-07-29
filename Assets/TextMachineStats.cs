using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextMachineStats : MonoBehaviour
{
  Machine machine;
  Text text;

  protected void Awake()
  {
    machine = GetComponentInParent<Machine>();
    machine.onValueChange += Machine_onValueChange;

    text = GetComponent<Text>();
  }

  void Machine_onValueChange()
  {
    text.text = (machine.currentEffeciencyPercent * 100).ToString("N0") + "%";
  }
}

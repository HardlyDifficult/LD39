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

    text = GetComponent<Text>();
  }

  protected void FixedUpdate()
  {
    text.text = (machine.percentPotential * 100).ToString("N0") + "%";
  }
}

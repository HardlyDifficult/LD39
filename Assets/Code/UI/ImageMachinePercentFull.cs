using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageMachinePercentFull : MonoBehaviour {
  Image image;
  Machine machine;

  protected void Awake()
  {
    image = GetComponent<Image>();
    machine = GetComponentInParent<Machine>();
  }

  protected void Update()
  {
    image.fillAmount = machine.percentPotential;
  }
}

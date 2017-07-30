using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashyText : MonoBehaviour
{
  Text text;

  protected void Awake()
  {
    text = GetComponent<Text>();
  }

  // Update is called once per frame
  void Update()
  {
    text.color = new Color(1, 1, 1, Mathf.Abs(Mathf.Sin(Time.timeSinceLevelLoad)));
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropIt : MonoBehaviour
{
  float velocity;

  [SerializeField]
  float multiple = 100;

  RectTransform rect;

  private void Awake()
  {
    rect = GetComponent<RectTransform>();
  }

  protected void Update()
  {
    float temp = Time.timeSinceLevelLoad;
    temp *= 10;

    velocity = 9.8f *  temp * temp * temp * multiple;
    rect.anchoredPosition -= new Vector2(0, velocity * Time.deltaTime);
    if(rect.anchoredPosition.y < 0)
    {
      rect.anchoredPosition = new Vector2(0, 0);
      Destroy(this);
    }
  }
}

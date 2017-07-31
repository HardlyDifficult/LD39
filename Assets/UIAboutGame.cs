using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAboutGame : MonoBehaviour
{
  protected void Awake()
  {
    Time.timeScale = 0;
  }

  protected void Update()
  {
    if(Input.anyKeyDown)
    {
      Destroy(gameObject);
      Time.timeScale = 1;
    }
  }
}

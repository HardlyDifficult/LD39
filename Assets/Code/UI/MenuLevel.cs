using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLevel : MonoBehaviour
{
  protected void Update()
  {
    if(Input.anyKey)
    {
      SceneManager.LoadScene("Level1");
    }
  }
}

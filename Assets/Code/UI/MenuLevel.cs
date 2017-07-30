using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MenuLevel : MonoBehaviour
{
  protected void Update()
  {
    if(Input.anyKeyDown 
        && EventSystem.current.IsPointerOverGameObject() == false)
    {
      SceneManager.LoadScene("Level1");
    }
  }
}

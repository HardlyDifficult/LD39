using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
  public static GameController instance;

  public int totalOutput;
  public int throughputLastUpdate;
  int totalLastUpdate;


  public GameObject explosionPrefab;

  protected void Awake()
  {
    if(instance != null)
    {
      Destroy(gameObject);
      return;
    }

    instance = this;
    SceneManager.sceneLoaded += SceneManager_sceneLoaded;
  }

  private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode arg1)
  {
    if(scene.name == "Menu")
    {
      totalOutput = 0;
    }
  }

  protected void Update()
  {
    throughputLastUpdate = totalOutput - totalLastUpdate;
    totalLastUpdate = totalOutput;
  }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
  [SerializeField]
  GameObject yellowLighteningPrefab;
  [SerializeField]
  GameObject redLighteningPrefab;

  public static GameController instance;

  public int totalOutput;
  public int throughputLastUpdate;
  int totalLastUpdate;

  int iYellow, iRed;
  List<GameObject> yellowLighteningList = new List<GameObject>();
  List<GameObject> redLighteningList = new List<GameObject>();


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
    if(scene.name == "Level1")
    {
      totalOutput = 0;
    }
  }

  protected void Update()
  {
    throughputLastUpdate = totalOutput - totalLastUpdate;
    totalLastUpdate = totalOutput;
    iYellow = -1;
    iRed = -1;
  }


  public void CreateLightning(bool isRed, Vector2 from, Vector2 to, float width)
  {
    Vector2 delta = to - from;
    float zRotation = (float)Math.Atan2(delta.y, delta.x);
    Vector3 scale = new Vector3(delta.magnitude * .81f, width, 1);

    CreateLightning(isRed, from, scale, zRotation);
  }

  void CreateLightning(bool isRed, Vector2 position, Vector3 scale, float zRotationInRad)
  {
    GameObject lightening = GetLightening(isRed);
    lightening.transform.position = position;
    lightening.transform.localScale = scale;
    lightening.transform.rotation = Quaternion.Euler(0, 0, zRotationInRad * Mathf.Rad2Deg);
  }

  protected void LateUpdate()
  {
    Clean(yellowLighteningList, iYellow);
    Clean(redLighteningList, iRed);
  }

  void Clean(List<GameObject> list, int i)
  {
    while(i < list.Count - 1)
    {
      int index = list.Count - 1;
      GameObject lightening = list[index];
      Destroy(lightening);
      list.RemoveAt(index);
    }
  }

  GameObject GetLightening(bool isRed)
  {
    List<GameObject> list;
    GameObject prefab;
    int i;
    if(isRed)
    {
      list = redLighteningList;
      iRed++;
      i = iRed;
      prefab = redLighteningPrefab;
    }
    else
    {
      list = yellowLighteningList;
      iYellow++;
      i = iYellow;
      prefab = yellowLighteningPrefab;
    }

    GameObject lightening;
    if(i >= list.Count)
    {
      lightening = Instantiate(prefab);
      list.Add(lightening);
    }
    else
    {
      lightening = list[i];
    }

    return lightening;
  }
}

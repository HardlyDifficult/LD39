using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
  public static GameController instance;

  public int totalOutput;
  public int throughputLastUpdate;
  int totalLastUpdate;

  protected void Awake()
  {
    instance = this;
  }

  protected void FixedUpdate()
  {
    throughputLastUpdate = totalOutput - totalLastUpdate;
    totalLastUpdate = totalOutput;
  }
}

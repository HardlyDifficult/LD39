using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour
{
  public static int initialNetworkPotential;
  public static int totalNetworkPotential;

  public static float percentNetworkPotential
  {
    get
    {
      return (float)totalNetworkPotential / initialNetworkPotential;
    }
  }

  int initialPotential;
  [SerializeField]
  int _totalPotential = 1000000;
  public int totalPotential
  {
    get
    {
      return _totalPotential;
    }
    set
    {
      totalNetworkPotential += value - totalPotential;
      _totalPotential = value;
    }
  }

  public int maxPonetialPerFixed = 100;

  public float percentPotential
  {
    get
    {
      return (float)totalPotential / initialPotential;
    }
  }

  protected void Awake()
  {
    initialPotential = totalPotential;
    totalNetworkPotential += totalPotential;
  }

  protected void Start()
  {
    initialNetworkPotential = totalNetworkPotential;
  }
}

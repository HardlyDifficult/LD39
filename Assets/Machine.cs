using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour
{
  [SerializeField]
  float _currentValue = 1;

  [SerializeField]
  int kwPer = 1000;

  public event Action onValueChange;

  /// <summary>
  /// Capped 0 -> 1
  /// </summary>
  public float currentEffeciencyPercent
  {
    get
    {
      return _currentValue;
    }
    set
    {
      _currentValue = value;

      if(currentEffeciencyPercent < 0)
      {
        _currentValue = 0;
      } else if(currentEffeciencyPercent > 1)
      {
        _currentValue = 1;
      }

      if(onValueChange != null)
      {
        onValueChange();
      }
    }
  }

  protected void FixedUpdate()
  {
    GameController.instance.totalOutput 
      += (int)(currentEffeciencyPercent * kwPer);
  }
}

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public static class Instructions
{
  public enum InstructionType
  {
    None, Repair,
  }

  static InstructionType _current;
  public static InstructionType current
  {
    get
    {
      return _current;
    }
    set
    {
      if(current == value)
      {
        return;
      }

      _current = value;
      if(onChange != null)
      {
        onChange();
      }
    }
  }

  public static Action onChange;
}

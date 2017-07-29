using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakdownOvertime : MonoBehaviour
{
  [SerializeField]
  float breakdownRate = .001f;

  Machine machine;

  protected void Awake()
  {
    machine = GetComponent<Machine>();
  }

  protected void FixedUpdate()
  {
    machine.totalPotential = (int)(machine.totalPotential * (1 - breakdownRate));
  }
}

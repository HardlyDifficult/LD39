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
    breakdownRate *= UnityEngine.Random.Range(.5f, 1.5f);
  }

  protected void FixedUpdate()
  {
    machine.totalPotential -= (int)(breakdownRate * Time.timeSinceLevelLoad * Time.timeSinceLevelLoad * Time.timeSinceLevelLoad);
  }
}

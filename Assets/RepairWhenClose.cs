using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairWhenClose : MonoBehaviour
{
  [SerializeField]
  float repairPercent = 1.01f;

  Machine machine;
  bool isRepairing;

  protected void Awake()
  {
    machine = GetComponent<Machine>();
  }

  protected void OnTriggerEnter2D(
    Collider2D collision)
  {
    Instructions.current = Instructions.InstructionType.Repair;
    isRepairing = true;
  }

  protected void OnTriggerExit2D(
    Collider2D collision)
  {
    Instructions.current = Instructions.InstructionType.None;
    isRepairing = false;
  }

  protected void FixedUpdate()
  {
    if(isRepairing)
    {
      machine.currentEffeciencyPercent *= repairPercent;
    }
  }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
  public int amountHarvestedThisFixed;

  [SerializeField]
  int maxEnergyLevel = 10000;
  public int energyLevel = 10000;

  [SerializeField]
  int energyPerTick = 1;
  [SerializeField]
  float internalAbsorbRate = .9f;

  public float currentEnergyPercent
  {
    get
    {
      return (float)energyLevel / maxEnergyLevel;
    }
  }

  public void Harvest(
    Machine machine,
    int amountToHarvest)
  {
    GameController.instance.totalOutput += amountToHarvest;
    if(machine != null)
    {
      machine.totalPotential -= amountToHarvest;
    }
    amountHarvestedThisFixed += amountToHarvest;
  }

  protected void Update()
  {
    energyLevel -= energyPerTick;
    if(amountHarvestedThisFixed > 0)
    {
      energyLevel += (int)(amountHarvestedThisFixed * internalAbsorbRate);
    }
    if(energyLevel <= 0)
    {

      Instantiate(GameController.instance.explosionPrefab, transform.position, Quaternion.identity);
      Destroy(gameObject);
    }
    else
    {

      energyLevel = Math.Min(energyLevel, maxEnergyLevel);

      amountHarvestedThisFixed = 0;
    }
  }
}

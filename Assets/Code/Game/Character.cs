using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
  public static int count;
  public int amountHarvestedThisFixed;

  RotateYFacing rotateYFacing;

  [SerializeField]
  int maxEnergyLevel = 10000;
  public int energyLevel = 10000;

  public float overload = 0;

  [SerializeField]
  int energyPerTick = 1;
  [SerializeField]
  float internalAbsorbRate = .9f;
  [SerializeField]
  Collider2D boltCollider;
  [SerializeField]
  Collider2D boltColliderWhenHorizontal;

  public Collider2D currentBoltCollider
  {
    get
    {
      if(rotateYFacing.isFacingHorizontal)
      {
        return boltColliderWhenHorizontal;
      }
      else
      {
        return boltCollider;
      }
    }
  }

  public float currentEnergyPercent
  {
    get
    {
      return (float)energyLevel / maxEnergyLevel;
    }
  }

  protected void Awake()
  {
    rotateYFacing = GetComponentInChildren<RotateYFacing>();
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
    if(Time.timeScale < .1)
    {
      return;
    }

    if(overload > 0)
    {
      overload -= .01f;
      energyLevel = maxEnergyLevel;
    } else
    {
      energyLevel -= energyPerTick;
    }

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

  protected void OnEnable()
  {
    count++;
  }
  protected void OnDisable()
  {
    count--;
  }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairWhenClose : MonoBehaviour
{
  [SerializeField]
  float lighteningWidthMultiple = .01f;

  [SerializeField]
  Collider2D myLighteningCollider;

  Machine machine;

  Collider2D myCollider;


  protected void Awake()
  {
    machine = GetComponent<Machine>();
    myCollider = GetComponent<Collider2D>();
  }
  
  protected void Update()
  {
    Collider2D[] resultList = new Collider2D[20];
    var filter = new ContactFilter2D();
    filter.useLayerMask = true;
    filter.useTriggers = true;
    filter.layerMask = LayerMask.GetMask(new[] { "Bots" });
    int count = myCollider.OverlapCollider(filter, resultList);

    for(int i = 0; i < count; i++)
    {
      Collider2D collider = resultList[i];

     



      Character character = collider.GetComponentInParent<Character>();
      Vector2 deltaPosition = transform.position - collider.transform.position;
      float deltaMag = deltaPosition.sqrMagnitude;
      float maxDelta = collider.GetComponentInChildren<CircleCollider2D>().radius;
      maxDelta *= maxDelta;

      float percentDistance = deltaMag / maxDelta;
      percentDistance = 1 - percentDistance;
      percentDistance *= percentDistance;
      percentDistance = Mathf.Clamp(percentDistance, .1f, 1);
      float multiple = 1 + (count - 1) * .75f;
      int amountToHarvest = (int)((machine.maxPonetialPerFixed / multiple) * percentDistance * (.1 + machine.percentPotential));
      amountToHarvest = Mathf.Clamp(amountToHarvest, 0, machine.totalPotential);

      character.Harvest(machine, amountToHarvest);

      collider = character.currentBoltCollider;
      var d = collider.Distance(myLighteningCollider);
      float width = amountToHarvest * lighteningWidthMultiple;
      width *= width;
      width += .001f;

      GameController.instance.CreateLightning(
        isRed: false,
        from: d.pointB,
        to: d.pointA,
        width: width);        
    }
  }
}

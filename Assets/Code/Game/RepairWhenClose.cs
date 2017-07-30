using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairWhenClose : MonoBehaviour
{
  [SerializeField]
  GameObject lighteningPrefab;
  [SerializeField]
  float lighteningWidthMultiple = .01f;
  [SerializeField]
  float lighteningLengthMultiple = 1.05f;

  [SerializeField]
  Collider2D myLighteningCollider;

  Machine machine;

  Collider2D myCollider;

  List<GameObject> lighteningList = new List<GameObject>();

  protected void Awake()
  {
    machine = GetComponent<Machine>();
    myCollider = GetComponent<Collider2D>();
  }

  protected void OnDestroy()
  {
    for(int i = 0; i < lighteningList.Count; i++)
    {
      Destroy(lighteningList[i]);
    }
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



      collider = collider.GetComponentInParent<CapsuleCollider2D>();
      var d = collider.Distance(myLighteningCollider);
      GameObject lightening = GetLightening(i);
      lightening.transform.position = d.pointB;
      float width = amountToHarvest * lighteningWidthMultiple;
      width *= width;
      width += .001f;
      lightening.transform.localScale = new Vector3(d.distance * lighteningLengthMultiple, width, 1);
      var delta = d.pointA - d.pointB;
      float zRotation = (float)Math.Atan2(delta.y, delta.x);
      lightening.transform.rotation = Quaternion.Euler(0, 0, zRotation * Mathf.Rad2Deg);





      
    }

    while(count < lighteningList.Count)
    {
      int index = lighteningList.Count - 1;
      GameObject lightening = lighteningList[index];
      Destroy(lightening);
      lighteningList.RemoveAt(index);
    }
  }

  private GameObject GetLightening(int i)
  {
    GameObject lightening;
    if(i >= lighteningList.Count)
    {
      lightening = Instantiate(lighteningPrefab);
      lighteningList.Add(lightening);
    } else
    {
      lightening = lighteningList[i];
    }

    return lightening;
  }
}

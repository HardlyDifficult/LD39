using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairWhenClose : MonoBehaviour
{
  [SerializeField]
  float repairPercent = .01f;
  [SerializeField]
  GameObject lighteningPrefab;
  [SerializeField]
  float lighteningWidthMultiple = .01f;

  [SerializeField]
  Collider2D myLighteningCollider;

  Machine machine;

  Collider2D myCollider;

  List<Collider2D> connectionList = new List<Collider2D>();
  List<GameObject> lighteningList = new List<GameObject>();

  protected void Awake()
  {
    machine = GetComponent<Machine>();
    myCollider = GetComponent<Collider2D>();
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    connectionList.Add(collision);
  }

  private void OnTriggerExit2D(Collider2D collision)
  {
    connectionList.Remove(collision);
  }

  protected void OnTriggerStay2D(
    Collider2D collision)
  {
    Vector2 deltaPosition = transform.position - collision.transform.position;
    float deltaMag = deltaPosition.sqrMagnitude;
    float maxDelta = collision.GetComponent<CircleCollider2D>().radius;
    maxDelta *= maxDelta;
    if(deltaMag > maxDelta)
    {
      return;
    }
    float percentDistance = deltaMag / maxDelta;
    percentDistance = 1 - percentDistance;
    //machine.currentEffeciencyPercent *= 1 + (repairPercent * percentDistance);

    var distance = collision.GetComponent<CapsuleCollider2D>().Distance(myCollider);
    Debug.DrawLine(transform.position, distance.pointA, Color.white * percentDistance);

  }

  protected void Update()
  {
    for(int i = 0; i < connectionList.Count; i++)
    {
      Collider2D collider = connectionList[i];
      Vector2 deltaPosition = transform.position - collider.transform.position;
      float deltaMag = deltaPosition.sqrMagnitude;
      float maxDelta = collider.GetComponent<CircleCollider2D>().radius;
      maxDelta *= maxDelta;

      float percentDistance = deltaMag / maxDelta;
      percentDistance = 1 - percentDistance;
      int amountToHarvest = (int)(machine.maxPonetialPerFixed * percentDistance * machine.percentPotential);
      amountToHarvest = Mathf.Clamp(amountToHarvest, 0, machine.totalPotential);
      GameController.instance.totalOutput += amountToHarvest;
      machine.totalPotential -= amountToHarvest;



      collider = collider.GetComponent<CapsuleCollider2D>();
      var d = collider.Distance(myLighteningCollider);
      GameObject lightening = GetLightening(i);
      lightening.transform.position = d.pointB;
      lightening.transform.localScale = new Vector3(amountToHarvest * lighteningWidthMultiple, d.distance, 1);
      var delta = d.pointA - d.pointB;
      float zRotation = (float)Math.Atan2(delta.y, delta.x);
      lightening.transform.rotation = Quaternion.Euler(0, 0, zRotation * Mathf.Rad2Deg - 90);





      
    }

    while(connectionList.Count < lighteningList.Count)
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

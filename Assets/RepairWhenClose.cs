using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairWhenClose : MonoBehaviour
{
  [SerializeField]
  float repairPercent = .01f;

  Machine machine;

  Collider2D myCollider;

  protected void Awake()
  {
    machine = GetComponent<Machine>();
    myCollider = GetComponent<Collider2D>();
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
    machine.currentEffeciencyPercent *= 1 + (repairPercent * percentDistance);

    var distance = collision.GetComponent<CapsuleCollider2D>().Distance(myCollider);
    Debug.DrawLine(distance.pointB, distance.pointA, Color.white * percentDistance);

    GameController.instance.totalOutput += (int) (machine.kwPer * machine.currentEffeciencyPercent * percentDistance);
  }
}

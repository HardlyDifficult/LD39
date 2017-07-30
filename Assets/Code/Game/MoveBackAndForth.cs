using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackAndForth : MonoBehaviour
{
  [SerializeField]
  bool xVsY;
  [SerializeField]
  float distance;
  [SerializeField]
  bool shouldTeleportBack;

  bool goingToEnd = true;
  [SerializeField]
  float speed;

  Vector2 start, end;

  protected void Awake()
  {
    Vector2 delta;
    if(xVsY)
    {
      delta = new Vector2(1, 0);
    }
    else
    {
      delta = new Vector2(0, 1);
    }

    start = (Vector2)transform.position;
    end = (Vector2)transform.position + delta * distance;
  }

  protected void Update()
  {
    Vector2 target;
    if(goingToEnd)
    {
      target = end;
    }
    else
    {
      target = start;
    }
    transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    if((target - (Vector2)transform.position).sqrMagnitude < .01)
    {
      if(shouldTeleportBack)
      {
        transform.position = start;
      }
      else
      {
        goingToEnd = !goingToEnd;
      }
    }
  }
}

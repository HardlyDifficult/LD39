using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateYFacing : MonoBehaviour
{
  public bool isFacingHorizontal;

  Rigidbody2D myBody;

  protected void Awake()
  {
    myBody = GetComponentInParent<Rigidbody2D>();
  }

  protected void Update()
  {
    Vector2 velocity = myBody.velocity;
    if(velocity.sqrMagnitude > .01)
    {
      float yRotation;
      if(Mathf.Abs(velocity.x) > Mathf.Abs(velocity.y))
      {
        isFacingHorizontal = true;
        if(velocity.x > 0)
        { // Right
          yRotation = 90;
        }
        else
        { // Left
          yRotation = -90;
        }
      }
      else
      {
        isFacingHorizontal = false;
        if(velocity.y > 0)
        { // Up
          yRotation = 180;
        }
        else
        { // Down
          yRotation = 0;
        }
      }
      transform.localRotation = Quaternion.Euler(0, yRotation, 0);
    }
  }
}

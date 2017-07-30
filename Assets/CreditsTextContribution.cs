using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsTextContribution : MonoBehaviour
{
  [SerializeField]
  float initialWaitTime;
  [SerializeField]
  float speed;

  protected void Start()
  {
    StartCoroutine(Go());
  }

  IEnumerator Go()
  {
    yield return new WaitForSeconds(initialWaitTime);

    Bounds screenBounds = new Bounds(Vector3.zero, Vector3.one * 12);
    Collider2D myCollider = GetComponent<Collider2D>();


    while(screenBounds.Contains(myCollider.bounds.min) == false 
      || screenBounds.Contains(myCollider.bounds.max) == false)
    {
      transform.position += new Vector3(0, speed, 0);
      yield return 0;
    }
  }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsCloud : MonoBehaviour
{
  [SerializeField]
  GameObject target;

  [SerializeField]
  float width = .1f;

  [SerializeField]
  float initialWaitTime;

  protected void Start()
  {
    StartCoroutine(Go());
  }

  private IEnumerator Go()
  {
    yield return new WaitForSeconds(initialWaitTime);


    bool isDone = false;
    while(isDone == false)
    {
      bool blew = false;
      Vector3? yourPosition = RandomPosition();
      if(yourPosition == null)
      {
        isDone = true;
        break;
      }
      for(int i = 0; i < 20; i++)
      {
        Vector3 myPosition = MyRandomPosition();
        if(Mathf.Abs(transform.position.x) < 5)
        {
          if(blew == false)
          {
            Transform[] letters = target.GetComponentsInChildren<Transform>();
            GameObject closestLetter = null;
            for(int j = 0; j < letters.Length; j++)
            {
              GameObject letter = letters[j].gameObject;
              if(letter == target)
              {
                continue;
              }
              if(closestLetter == null || (closestLetter.transform.position - yourPosition.Value).sqrMagnitude 
                > (letter.transform.position - yourPosition.Value).sqrMagnitude)
              {
                closestLetter = letter;
              }
            }

            if(closestLetter == null)
            {
              isDone = true;
              break;
            }
            else if((closestLetter.transform.position - yourPosition.Value).sqrMagnitude < .2)
            {
              Destroy(closestLetter);
              Instantiate(GameController.instance.explosionPrefab, yourPosition.Value, Quaternion.identity);
            }
            blew = true;
          }
          GameController.instance.CreateLightning(false, myPosition, yourPosition.Value, width);
        }
        yield return 0;
      }
    }

    GameObject.Find("Contribution").GetComponent<Rigidbody2D>().gravityScale = 1;
  }

  Vector3? RandomPosition()
  {
    Collider2D[] targetColliderList = target.GetComponentsInChildren<Collider2D>();
    if(targetColliderList.Length < 1)
    {
      return null;
    }
    Collider2D targetCollider = targetColliderList[UnityEngine.Random.Range(0, targetColliderList.Length)];
    Bounds targetBounds = targetCollider.bounds;
    return (Vector3)new Vector2(UnityEngine.Random.value * targetBounds.extents.x * 2 - targetBounds.extents.x,
      UnityEngine.Random.value * targetBounds.extents.y * 2 - targetBounds.extents.y) + targetBounds.center;
  }
  Vector3 MyRandomPosition()
  {
    Collider2D targetCollider = GetComponent<Collider2D>();
    Bounds targetBounds = targetCollider.bounds;
    return (Vector3)new Vector2(UnityEngine.Random.value * targetBounds.extents.x * 2 - targetBounds.extents.x,
      UnityEngine.Random.value * targetBounds.extents.y * 2 - targetBounds.extents.y) + targetBounds.center;
  }
}

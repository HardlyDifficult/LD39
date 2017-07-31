using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCloud : MonoBehaviour
{

  [SerializeField]
  float width = .1f;

  [SerializeField]
  float initialWaitTime;

  Camera camera;

  protected void Start()
  {
    camera = Camera.main;
    StartCoroutine(Go());
  }

  private IEnumerator Go()
  {
    while(true)
    {
      yield return new WaitForSeconds(initialWaitTime * UnityEngine.Random.value);

      bool isDone = false;
        Machine[] targetList = GameObject.FindObjectsOfType<Machine>();
        Machine target = targetList[UnityEngine.Random.Range(0, targetList.Length)];
      while(isDone == false)
      {
        bool blew = false;
        Vector3? yourPosition = RandomPosition(target);
        if(yourPosition == null)
        {
          isDone = true;
          break;
        }
        for(int i = 0; i < 100; i++)
        {
          if(target == null)
          {
            isDone = true;
            break;
          }
          if(target.isActiveAndEnabled == false)
          {
            isDone = true;
            break;
          }

          Vector3 myPosition = MyRandomPosition();
          //if(Mathf.Abs(transform.position.x) < 5)
          {
            if(blew == false && i > 60)
            {

              if(target == null)
              {
                isDone = true;
                break;
              }
              else if((target.transform.position - yourPosition.Value).sqrMagnitude < .2)
              {
                target.gameObject.SetActive(false);
                GameObject explosion = Instantiate(GameController.instance.explosionPrefab, yourPosition.Value, Quaternion.identity);
                explosion.transform.localScale *= 2;
                Collider2D[] hitList = Physics2D.OverlapCircleAll(yourPosition.Value, 3);

                for(int j = 0; j < hitList.Length; j++)
                {
                  Collider2D hit = hitList[j];
                  Character character = hit.GetComponent<Character>();
                  if(character == null)
                  {
                    continue;
                  }
                  float distance = (transform.position - yourPosition.Value).magnitude;
                  float percentDistance = distance / 3;
                  character.overload = percentDistance;
                }

              }
              blew = true;
            }
            GameController.instance.CreateLightning(false, myPosition, yourPosition.Value, width);
          }
          yield return 0;
        }
      }


      yield return new WaitForSeconds(20f * UnityEngine.Random.value);
    }
  }

  Vector3? RandomPosition(Machine target)
  {
    Collider2D targetCollider= target.GetComponent<RepairWhenClose>().myLighteningCollider;
    Bounds targetBounds = targetCollider.bounds;
    return (Vector3)new Vector2(UnityEngine.Random.value * targetBounds.extents.x * 2 - targetBounds.extents.x,
      UnityEngine.Random.value * targetBounds.extents.y * 2 - targetBounds.extents.y) + targetBounds.center;
  }
  Vector3 MyRandomPosition()
  {
    return camera.transform.position + new Vector3(UnityEngine.Random.Range(-3, 3) + transform.position.x, camera.orthographicSize + 1, 0);
    //Collider2D targetCollider = GetComponent<Collider2D>();
    //Bounds targetBounds = targetCollider.bounds;
    //return (Vector3)new Vector2(UnityEngine.Random.value * targetBounds.extents.x * 2 - targetBounds.extents.x,
    //  UnityEngine.Random.value * targetBounds.extents.y * 2 - targetBounds.extents.y) + targetBounds.center;
  }

}

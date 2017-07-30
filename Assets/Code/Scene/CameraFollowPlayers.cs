using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayers : MonoBehaviour
{
  Camera camera;

  [SerializeField]
  float minSize = 3f;
  Vector3 targetPosition;
  float targetSize;

  [SerializeField]
  float positionLerpSpeed = .01f;
  [SerializeField]
  float sizeLerpSpeed = .01f;

  protected void Awake()
  {
    camera = GetComponent<Camera>();
  }

  protected void FixedUpdate()
  {
    if(PlayerController.playerList.Count == 0)
    {
      targetPosition = new Vector3(0, 0, -10);
      targetSize = minSize;
      return;
    }

    Vector2 averagePosition = Vector2.zero;
    Vector2 minPosition = new Vector2(float.MaxValue, float.MaxValue);
    Vector2 maxPosition = new Vector2(float.MinValue, float.MinValue);

    for(int i = 0; i < PlayerController.playerList.Count; i++)
    {
      Vector2 position = (Vector2)PlayerController.playerList[i].transform.position;
      averagePosition += position;
      minPosition.x = Mathf.Min(minPosition.x, position.x);
      minPosition.y = Mathf.Min(minPosition.y, position.y);
      maxPosition.x = Mathf.Max(maxPosition.x, position.x);
      maxPosition.y = Mathf.Max(maxPosition.y, position.y);
    }
    averagePosition /= PlayerController.playerList.Count;

    Vector2 deltaPosition = maxPosition - minPosition;
    float maxDelta = Mathf.Max(deltaPosition.x, deltaPosition.y);

    targetSize = Mathf.Max(minSize, maxDelta);
    targetPosition = (Vector3)averagePosition + new Vector3(0, 0, -10);
  }

  protected void Update()
  {
    transform.position = Vector3.Lerp(transform.position, targetPosition, positionLerpSpeed * Time.deltaTime);
    camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, targetSize, sizeLerpSpeed * Time.deltaTime);
  }
}

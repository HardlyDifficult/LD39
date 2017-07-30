using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{

  protected void Update()
  {
    Bounds bounds = new Bounds();
    Machine[] machines = GameObject.FindObjectsOfType<Machine>();
    for(int i = 0; i < machines.Length; i++)
    {
      Collider2D[] colliders = machines[i].GetComponents<Collider2D>();
      for(int j = 0; j < colliders.Length; j++)
      {
        bounds.Encapsulate(colliders[j].bounds);
      }
    }

    Character[] characters = GameObject.FindObjectsOfType<Character>();
    for(int i = 0; i < characters.Length; i++)
    {
      Character character = characters[i];
      bounds.Encapsulate(character.GetComponent<CapsuleCollider2D>().bounds);
    }

    float maxSize = Mathf.Max(bounds.extents.x, bounds.extents.y);
    Camera camera = GetComponent<Camera>();
    camera.transform.position = new Vector3(bounds.center.x, bounds.center.y, -10);
    camera.orthographicSize = maxSize + 1;
  }
}

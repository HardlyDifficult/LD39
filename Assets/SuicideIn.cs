using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideIn : MonoBehaviour
{

  [SerializeField]
  float time = 3;

  void Start()
  {
    Destroy(gameObject, time);
  }

}

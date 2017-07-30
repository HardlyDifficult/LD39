using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShader : MonoBehaviour
{
  [SerializeField]
  float sizeScale = .01f;

  Character character;
  Material material;

  float progress;

  protected void Awake()
  {
    character = GetComponentInParent<Character>();
    material = GetComponent<MeshRenderer>().material;
    material.SetFloat("_RngSeed", UnityEngine.Random.value * 999);
  }

  protected void Update()
  {
    float delta = character.currentEnergyPercent; // 1 -> 0
    delta = 1 - delta;
    delta *= delta;
    delta = 1 - delta;
    // Goal is to fast then slow

    float temp = character.currentEnergyPercent;
    temp *= temp;
    //temp = 1 - temp;

    progress += Time.deltaTime * temp;
   
    //delta *= delta;
    material.SetFloat("_Size",  delta);
    material.SetFloat("_Progress",  progress);
  }
}

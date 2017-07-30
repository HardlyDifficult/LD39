using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShader : MonoBehaviour
{
  [SerializeField]
  float sizeScale = .01f;

  Character character;
  Material material;

  protected void Awake()
  {
    character = GetComponentInParent<Character>();
    material = GetComponent<MeshRenderer>().material;
    material.SetFloat("_RngSeed", UnityEngine.Random.value * 999);
  }

  protected void Update()
  {
    float delta = character.currentEnergyPercent;
    //delta *= delta;
    material.SetFloat("_Size",  delta);
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneMaterial : MonoBehaviour
{
  protected void Awake()
  {
    MeshRenderer renderer = GetComponent<MeshRenderer>();
    Material mat = renderer.material;
    renderer.material = new Material(mat);
    renderer.material.SetFloat("_RngSeed", UnityEngine.Random.value * 999);
  }
}

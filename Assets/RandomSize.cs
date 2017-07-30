using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSize : MonoBehaviour {

	protected void OnEnable ()
  {
    Resize();
  }

  public void Resize()
  {
    transform.localScale = Vector3.one * UnityEngine.Random.Range(.5f, 1.5f);
  }
}

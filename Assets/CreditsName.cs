using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsName : MonoBehaviour
{

  public GameObject contribution;

  int viewCount = 0;

  void OnEnable()
  {
    contribution.SetActive(true);
    for(int i = 0; i < transform.childCount; i++)
    {
      transform.GetChild(i).gameObject.SetActive(true);
    }

    if(viewCount > 0)
    {
      transform.rotation = Quaternion.Euler(0, 0, (UnityEngine.Random.value * 2 - 1) * viewCount * 20);
      contribution.transform.rotation = Quaternion.Euler(0, 0, (UnityEngine.Random.value * 2 - 1) * viewCount * 5);
    }

    viewCount++;
  }

  private void OnDisable()
  {
    contribution.SetActive(false);
  }
}

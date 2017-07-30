using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsManager : MonoBehaviour
{
  public CreditsName currentName;

  [SerializeField]
  CreditsName[] nameList;

  void Start()
  {
    Go(null);
  }

  private void Go(CreditsName notMe)
  {
    do
    {
      currentName = nameList[UnityEngine.Random.Range(0, nameList.Length)];
    } while(currentName == notMe);
    currentName.gameObject.SetActive(true);
    currentName.transform.position = new Vector3(0, 10, 0);
    currentName.contribution.GetComponent<Rigidbody2D>().gravityScale = 0;
    currentName.contribution.transform.position = new Vector3(0, -10, 0);
    StopAllCoroutines();
  }

  public void Restart()
  {
    StopAllCoroutines();
    StartCoroutine(StartOver());
  }

  private IEnumerator StartOver()
  {
    yield return new WaitForSeconds(.5f);
    print("Restart");
    currentName.gameObject.SetActive(false);
    Go(currentName);
  }
}

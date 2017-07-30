using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
  public float percentHarvestToProceed;

  [SerializeField]
  int thisLevelNumber;

  protected void Update()
  {
    if(Machine.count == 0
      || Character.count == 0)
    { // Game over
      float percentOfNetwork = (float)GameController.instance.totalOutput / Machine.initialNetworkPotential;
      if(percentOfNetwork >= percentHarvestToProceed)
      {
        if(thisLevelNumber > 1)
        { // win
          SceneManager.LoadScene("Win");
        }
        else
        { // Next!
          SceneManager.LoadScene("Level" + (thisLevelNumber + 1));
        }
      }
      else
      { // lose
        SceneManager.LoadScene("Level" + thisLevelNumber);
      }
    }
  }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseLevel : MonoBehaviour
{


  void Update()
  {
    if(Input.anyKeyDown)
    {
      SceneManager.LoadScene("Menu");
    }
  }
}

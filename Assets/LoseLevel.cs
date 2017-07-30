using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseLevel : MonoBehaviour
{


  void Update()
  {
    if(Input.anyKey)
    {
      SceneManager.LoadScene("Menu");
    }
  }
}

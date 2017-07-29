using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public class AutoSave
{
  static AutoSave()
  {
    EditorApplication.playmodeStateChanged
      += OnPlaymodeStateChanged;
  }

  static void OnPlaymodeStateChanged()
  {
    if(EditorApplication.isPlaying)
    {
      return;
    }

    EditorSceneManager.SaveOpenScenes();
  }
}
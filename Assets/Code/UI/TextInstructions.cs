using UnityEngine;
using UnityEngine.UI;

public class TextInstructions : MonoBehaviour
{
  Text text;

  protected void Awake()
  {
    text = GetComponent<Text>();
    text.text = "";
    Instructions.onChange += OnInstructionChanges;
  }

  void OnInstructionChanges()
  {
    switch(Instructions.current)
    {
      case Instructions.InstructionType.None:
        text.text = "";
        break;
      case Instructions.InstructionType.Repair:
        text.text = "Hit space to start repairs";
        break;
      default:
        break;
    }
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public static List<PlayerController> playerList = new List<PlayerController>();

  [SerializeField]
  float speed = 1;
  [SerializeField]
  float costPerMag = 100;

  Rigidbody2D myBody;
  Character character;

  protected void Awake()
  {
    myBody = GetComponent<Rigidbody2D>();
    character = GetComponent<Character>();
  }

  protected void OnEnable()
  {
    playerList.Add(this);
  }

  protected void OnDisable()
  {
    playerList.Remove(this);
  }

  protected void FixedUpdate()
  {
    float xDirection = Input.GetAxis("Horizontal");
    float yDirection = Input.GetAxis("Vertical");

    myBody.velocity = new Vector2(xDirection, yDirection) * speed * character.currentEnergyPercent;
    float moveMag = myBody.velocity.magnitude;

    character.energyLevel -= (int)(moveMag * costPerMag);
  }
}

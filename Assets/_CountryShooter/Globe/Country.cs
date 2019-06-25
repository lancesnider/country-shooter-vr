using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Country : MonoBehaviour
{
  public string countryName;
  public Constants.Region region;

  void OnCollisionEnter(Collision collision)
  {
    // Disable the bullet collider so it doesn't bounce onto something else.
    Collider bulletCollider = collision.collider.GetComponent<Collider>();
    bulletCollider.enabled = false;

    // Only check answer if Playing
    if (GameController.instance.gameStatus == GameController.GameStatus.Playing)
    {
      GameController.instance.CheckAnswer(countryName);
    }
  }
}

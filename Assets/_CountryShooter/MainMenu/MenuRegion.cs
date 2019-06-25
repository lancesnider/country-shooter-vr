using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRegion : MonoBehaviour
{
  public Constants.Region region;

  void OnCollisionEnter(Collision collision)
  {
    // Only start a game from the main menu
    if (GameController.instance.gameStatus == GameController.GameStatus.MainMenu)
    {
      GameController.instance.StartGame(region);
    }
  }
}

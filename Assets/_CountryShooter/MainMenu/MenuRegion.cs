using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRegion : MonoBehaviour
{
  public delegate void MenuClickedAction(Constants.Region region);
  public static event MenuClickedAction OnMenuClicked;

  private bool isActive = true;
  public Constants.Region region;

  void Awake()
  {
    GameController.OnGameStart += SetInactive;
    GameController.OnGameOver += SetActive;
  }

  void onDisable()
  {
    GameController.OnGameStart -= SetInactive;
    GameController.OnGameOver -= SetActive;
  }

  void SetActive()
  {
    isActive = true;
  }

  void SetInactive()
  {
    isActive = false;
  }

  public void MenuClicked(Constants.Region currentRegion)
  {
    // Only start a game from the main menu
    if (isActive)
    {
      if (OnMenuClicked != null)
        OnMenuClicked(region);
    }
  }


  void OnCollisionEnter(Collision collision)
  {
    MenuClicked(region);
  }
}

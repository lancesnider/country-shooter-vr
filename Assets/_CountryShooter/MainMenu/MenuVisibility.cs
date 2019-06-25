using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuVisibility : MonoBehaviour
{
  public GameObject[] regionMenuItems;

  void Awake()
  {
    GameController.OnGameStart += HideMenu;
    GameController.OnGameOver += ShowMenu;
  }

  void onDisable()
  {
    GameController.OnGameStart -= HideMenu;
    GameController.OnGameOver -= ShowMenu;
  }

  void HideMenu()
  {
    foreach (GameObject menuItem in regionMenuItems)
    {
      menuItem.SetActive(false);
    }
  }

  void ShowMenu()
  {
    foreach (GameObject menuItem in regionMenuItems)
    {
      menuItem.SetActive(true);
    }
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Store : MonoBehaviour
{
  private int gameStatus;

  public int GameStatus
  {
    set
    {
      Debug.Log(value);
      gameStatus = value;
    }
  }
}

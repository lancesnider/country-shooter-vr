using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameActions : MonoBehaviour
{
  public static GameActions instance;

  public enum ActionTypes
  {
    MENU_CLICKED,
    COUNTRY_HIT,
    TIMER_ENDED,
    PAUSE_CLICKED
  }

  void Awake()
  {
    // Make sure this is the only GameActions
    if (instance == null)
      instance = this;
    else if (instance != this)
      Destroy(gameObject);
  }

  void OnDestroy()
  {

  }

  public void Call(ActionTypes actionType)
  {
    switch (actionType)
    {
      case ActionTypes.TIMER_ENDED:
        Debug.Log("Timer ended");
        break;
      case ActionTypes.PAUSE_CLICKED:
        Debug.Log("Pause clicked");
        break;

        Debug.LogError(actionType + "is not a valid action.");
    }
  }


  public void Call<T>(ActionTypes actionType, T data)
  {
    switch (actionType)
    {
      case ActionTypes.MENU_CLICKED:
        if (data is GameObject)
        {
          Debug.Log("is");
        }
        else
        {
          Debug.LogError("The" + actionType + " action type requires a Region");
        }
        break;
      case ActionTypes.COUNTRY_HIT:
        if (data is GameObject)
        {
          Debug.Log("is");
        }
        else
        {
          Debug.LogError("The" + actionType + " action type requires a GameObject");
        }
        break;

        Debug.LogError(actionType + "is not a valid action.");
    }
  }
}

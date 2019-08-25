using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAgain : MonoBehaviour
{
  public Constants.Region region = Constants.Region.All;

  public delegate void MenuClickedAction(Constants.Region region);
  public static event MenuClickedAction OnMenuClicked;

  void OnCollisionEnter(Collision collision)
  {
    if (OnMenuClicked != null)
      OnMenuClicked(region);
  }
}

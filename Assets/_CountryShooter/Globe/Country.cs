using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;

public class Country : MonoBehaviour
{
  public delegate bool CountryHitAction(string countryName);
  public static event CountryHitAction OnCountryHit;

  private bool isActive = true;
  public string countryID;
  public string countryName;
  public Constants.Region region;
  public string countryNameOverride;

  void Awake()
  {
    GameController.OnGameStart += SetActive;
    GameController.OnGameOver += SetInactive;
    countryID = gameObject.name;

    string countryIDWithoutUnderscore = countryName.Replace("_", " ");
    TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
    countryName = textInfo.ToTitleCase(countryIDWithoutUnderscore);
  }

  void onDisable()
  {
    GameController.OnGameStart -= SetActive;
    GameController.OnGameOver -= SetInactive;
  }

  void SetActive()
  {
    isActive = true;
  }

  void SetInactive()
  {
    isActive = false;
  }

  void OnCollisionEnter(Collision collision)
  {
    // Disable the bullet collider so it doesn't bounce onto something else.
    Collider bulletCollider = collision.collider.GetComponent<Collider>();
    bulletCollider.enabled = false;

    // Only check answer if Playing
    if (isActive)
    {
      if (OnCountryHit != null)
        OnCountryHit(countryID);
    }
  }
}

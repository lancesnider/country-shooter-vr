using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;

public enum CountryState { Right, Wrong, Default };

public class Country : MonoBehaviour
{
  private CountryState countryState = CountryState.Default;

  public delegate bool CountryHitAction(string countryName);
  public static event CountryHitAction OnCountryHit;

  private Color defaultColor = new Color(0.08627451f, 0.7254902f, 0.3607843f);
  private Color wrongColor = new Color(0.9490197f, 0.3764706f, 0.3333333f);
  private Color rightColor = new Color(0.9176471f, 0.8509805f, 0.1882353f);

  private bool isActive = true;
  public string countryID;
  public string countryName;
  public Constants.Region region;
  public string countryNameOverride;
  public CountryTitle countryTitle;

  private Material countryMaterial;

  void Awake()
  {
    GameController.OnGameStart += SetActive;
    GameController.OnGameOver += SetInactive;
    GameController.OnSetQuestion += ResetIncorrect;
    countryID = gameObject.name;

    countryMaterial = GetComponent<Renderer>().material;
    TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
    string countryIDWithoutUnderscore = countryID.Replace("_", " ");
    countryName = textInfo.ToTitleCase(countryIDWithoutUnderscore);
  }

  void onDisable()
  {
    GameController.OnGameStart -= SetActive;
    GameController.OnGameOver -= SetInactive;
    GameController.OnSetQuestion -= ResetIncorrect;
  }

  void SetActive()
  {
    SetCountryState(CountryState.Default);
    isActive = true;
  }

  void SetInactive()
  {
    isActive = false;
  }

  void ResetIncorrect(Constants.Region region)
  {
    if (countryState == CountryState.Wrong)
    {
      SetCountryState(CountryState.Default);
    }
  }


  void SetCountryState(CountryState newState)
  {
    if (newState != countryState)
    {
      switch (newState)
      {
        case CountryState.Default:
          countryMaterial.color = defaultColor;
          break;
        case CountryState.Wrong:
          countryTitle.setTitle(gameObject);
          countryMaterial.color = wrongColor;
          break;
        case CountryState.Right:
          countryMaterial.color = rightColor;
          break;
      }


      countryState = newState;
    }
  }

  void OnCollisionEnter(Collision collision)
  {
    // Disable the bullet collider so it doesn't bounce onto something else.
    Collider bulletCollider = collision.collider.GetComponent<Collider>();
    bulletCollider.enabled = false;

    // Only check answer if Playing && it's in a default state
    if (isActive && countryState == CountryState.Default)
    {
      if (OnCountryHit != null)
      {
        bool correct = OnCountryHit(countryID);
        if (correct)
        {
          SetCountryState(CountryState.Right);
        }
        else if (countryState == CountryState.Default)
        {
          SetCountryState(CountryState.Wrong);
        }
      }
    }
  }
}

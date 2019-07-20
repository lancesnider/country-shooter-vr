using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountryTitle : MonoBehaviour
{
  private Transform thisTransform;
  public TextMeshPro titleText;

  void Start()
  {
    thisTransform = transform;
  }

  void Awake()
  {
    GameController.OnSetQuestion += ResetTitle;
  }

  void onDisable()
  {
    GameController.OnSetQuestion -= ResetTitle;
  }

  public void ResetTitle(Constants.Region region)
  {
    titleText.text = "";
  }

  public void setTitle(GameObject country)
  {
    // set title position
    thisTransform.position = country.transform.position;
    thisTransform.LookAt(Vector3.zero);
    // set title
    titleText.text = country.GetComponent<Country>().countryName;
  }
}

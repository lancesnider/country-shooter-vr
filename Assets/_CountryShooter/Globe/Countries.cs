using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countries : MonoBehaviour
{
  public GameObject[] allCountries;
  public GameObject[] africanCountries;
  public GameObject[] asianCountries;
  public GameObject[] australianCountries;
  public GameObject[] europeanCountries;
  public GameObject[] northAmericanCountries;
  public GameObject[] southAmericanCountries;

  public string[] getRandomizedCountries(Constants.Region region)
  {
    GameObject[] countriesOfARegion = getCountriesByRegion(region);
    string[] countryNames = getCountryNames(countriesOfARegion);

    return Shuffle(countryNames);
  }

  private GameObject[] getCountriesByRegion(Constants.Region region)
  {
    switch (region)
    {
      case Constants.Region.Africa:
        return africanCountries;
      case Constants.Region.Asia:
        return asianCountries;
      case Constants.Region.Australia:
        return australianCountries;
      case Constants.Region.Europe:
        return europeanCountries;
      case Constants.Region.NorthAmerica:
        return northAmericanCountries;
      case Constants.Region.SouthAmerica:
        return southAmericanCountries;
      default:
        return allCountries;
    }
  }

  private string[] getCountryNames(GameObject[] countryGameObjects)
  {
    string[] countryNames;
    countryNames = new string[countryGameObjects.Length];
    for (int i = 0; i < countryGameObjects.Length; i++)
    {
      Country countryScript = countryGameObjects[i].GetComponent<Country>();
      countryNames[i] = countryScript.countryName;
    }

    return Shuffle(countryNames);
  }

  private string[] Shuffle(string[] countriesToShuffle)
  {
    int countriesLength = countriesToShuffle.Length;
    string tempCountry;

    // For each item in the array:
    // 1. Pick a random index
    // 2. Swap the current string with the random one
    for (int i = 0; i < countriesLength; i++)
    {
      int randomIndex = Random.Range(0, countriesLength);
      tempCountry = countriesToShuffle[randomIndex];
      countriesToShuffle[randomIndex] = countriesToShuffle[i];
      countriesToShuffle[i] = tempCountry;
    }

    return countriesToShuffle;
  }
}

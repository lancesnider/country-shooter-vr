using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
  This is the place to store each of the country game objects, and get a
  randomized list of countries.
*/

public class Countries : MonoBehaviour
{
  public GameObject[] allCountries;
  public GameObject[] africanCountries;
  public GameObject[] asianCountries;
  public GameObject[] australianCountries;
  public GameObject[] europeanCountries;
  public GameObject[] northAmericanCountries;
  public GameObject[] southAmericanCountries;

  public GameObject[] getRandomizedCountries(Constants.Region region)
  {
    GameObject[] countriesOfARegion = getCountriesByRegion(region);

    return Shuffle(countriesOfARegion);
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

  private GameObject[] Shuffle(GameObject[] countriesToShuffle)
  {
    int countriesLength = countriesToShuffle.Length;
    GameObject tempCountry;

    // For each item in the array:
    // 1. Pick a random index
    // 2. Swap the current GameObject with the random one
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

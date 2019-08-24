using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
  public delegate void GameOverAction();
  public static event GameOverAction OnGameOver;

  public delegate void GameStartAction();
  public static event GameStartAction OnGameStart;

  public delegate void GameSetDifficultyAction(int difficulty);
  public static event GameSetDifficultyAction OnGameSetDifficulty;

  public delegate void SetQuestionAction(Constants.Region region);
  public static event SetQuestionAction OnSetQuestion;

  public GameObject globe;
  private Countries globeCountriesScript;
  private GlobeMovement globeMovementScript;

  public int difficulty = 0;
  public int score = 0;
  private int incorrectAnswers;
  private List<GameObject> incorrectCountries;
  private GameObject currentCountry;
  public string currentCountryID;
  private int currentCountryIndex;
  private GameObject[] randomCountries;

  public TextMeshPro instructionText;

  void Awake()
  {
    MenuRegion.OnMenuClicked += StartGame;
    Country.OnCountryHit += CheckAnswer;

    globeCountriesScript = globe.GetComponent<Countries>();
  }

  void onDisable()
  {
    MenuRegion.OnMenuClicked -= StartGame;
    Country.OnCountryHit -= CheckAnswer;
  }

  // Start is called before the first frame update
  public void StartGame(Constants.Region region)
  {
    Debug.Log("Start Game");
    randomCountries = globeCountriesScript.getRandomizedCountries(region, difficulty);

    // Reset variables
    score = 0;
    incorrectAnswers = 0;
    currentCountryIndex = 0;
    if (OnGameStart != null)
    {
      OnGameStart();
    }

    if (OnGameSetDifficulty != null)
    {
      OnGameSetDifficulty(difficulty);
    }

    SetQuestion();
  }

  public void SetQuestion()
  {
    if (currentCountryIndex == randomCountries.Length)
    {
      EndGame();
      return;
    }

    currentCountry = randomCountries[currentCountryIndex];
    Country currentCountryScript = currentCountry.GetComponent<Country>();
    currentCountryID = currentCountryScript.countryID;

    instructionText.text = "Find " + currentCountryScript.countryName;

    if (OnSetQuestion != null)
      OnSetQuestion(currentCountryScript.region);
  }

  public bool CheckAnswer(string answer)
  {
    if (answer == currentCountryID)
    {
      Debug.Log("Correct!");
      currentCountryIndex++;
      SetQuestion();
      return true;
    }

    Debug.Log("Incorrect.");
    incorrectAnswers++;
    return false;

  }

  public void EndGame()
  {
    Debug.Log("Game over!");
    instructionText.text = "";
    if (OnGameOver != null)
      OnGameOver();
  }

  public void PauseGame()
  {
  }

  public void ResumeGame()
  {
  }
}

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

  public delegate void GameSetRegionAction(Constants.Region region);
  public static event GameSetRegionAction OnSetRegion;

  public delegate void CorrectAnswerAction();
  public static event CorrectAnswerAction OnCorrectAnswer;

  public delegate void IncorrectAnswerAction();
  public static event IncorrectAnswerAction OnIncorrectAnswer;

  public delegate void SetQuestionAction(Constants.Region region);
  public static event SetQuestionAction OnSetQuestion;

  public GameObject globe;
  private Countries globeCountriesScript;
  private GlobeMovement globeMovementScript;

  public int difficulty = 0;
  private GameObject currentCountry;
  public string currentCountryID;
  private int currentCountryIndex;
  private GameObject[] randomCountries;

  public TextMeshPro instructionText;

  void Awake()
  {
    instructionText.text = "";

    MenuRegion.OnMenuClicked += StartGame;
    Country.OnCountryHit += CheckAnswer;
    DifficultySwitcher.OnDifficultyChanged += DifficultyChanged;

    globeCountriesScript = globe.GetComponent<Countries>();
  }

  void onDisable()
  {
    MenuRegion.OnMenuClicked -= StartGame;
    Country.OnCountryHit -= CheckAnswer;
    DifficultySwitcher.OnDifficultyChanged -= DifficultyChanged;
  }

  // Start is called before the first frame update
  public void StartGame(Constants.Region region)
  {
    Debug.Log("Start Game");
    randomCountries = globeCountriesScript.getRandomizedCountries(region, difficulty);

    // Reset variables
    currentCountryIndex = 0;

    if (OnGameStart != null)
    {
      OnGameStart();
    }

    if (OnSetRegion != null)
    {
      OnSetRegion(region);
    }

    SetQuestion();
  }

  private void DifficultyChanged(int newDifficulty)
  {
    difficulty = newDifficulty;
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
      StartCoroutine(CorrectAnswer());
      if (OnCorrectAnswer != null)
        OnCorrectAnswer();

      return true;
    }

    if (OnIncorrectAnswer != null)
      OnIncorrectAnswer();

    Debug.Log("Incorrect.");
    return false;
  }

  private IEnumerator CorrectAnswer()
  {
    Debug.Log("Correct!");
    yield return new WaitForSeconds(0.5f);
    currentCountryIndex++;
    SetQuestion();
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

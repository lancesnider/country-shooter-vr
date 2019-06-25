using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
  public enum GameStatus { Playing, Paused, MainMenu }

  public delegate void GameOverAction();
  public static event GameOverAction OnGameOver;

  public delegate void GameStartAction();
  public static event GameStartAction OnGameStart;

  public delegate void SetQuestionAction();
  public static event SetQuestionAction OnSetQuestion;

  public static GameController instance;

  public GameObject globe;
  private Countries globeCountriesScript;
  private GlobeMovement globeMovementScript;

  public GameStatus gameStatus = GameStatus.MainMenu;
  public int score = 0;
  private int incorrectAnswers;
  public string currentCountry;
  private int currentCountryIndex;
  private string[] randomCountries;

  void Awake()
  {
    // Make sure this is the only GameController
    if (instance == null)
      instance = this;
    else if (instance != this)
      Destroy(gameObject);

    globeCountriesScript = globe.GetComponent<Countries>();
  }

  void Start()
  {
    GameActions.instance.Call(GameActions.ActionTypes.MENU_CLICKED, Constants.Region.Africa);
  }

  // Start is called before the first frame update
  public void StartGame(Constants.Region region)
  {
    Debug.Log("Start Game");
    print(region);
    randomCountries = globeCountriesScript.getRandomizedCountries(region);
    gameStatus = GameStatus.Playing;

    // Reset variables
    score = 0;
    incorrectAnswers = 0;
    currentCountryIndex = 0;
    if (OnGameStart != null)
      OnGameStart();


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
    Debug.Log("Find " + currentCountry);

    if (OnSetQuestion != null)
      OnSetQuestion();
  }

  public bool CheckAnswer(string answer)
  {
    if (answer == currentCountry)
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
    gameStatus = GameStatus.MainMenu;
    if (OnGameOver != null)
      OnGameOver();
  }

  public void PauseGame()
  {
    gameStatus = GameStatus.Paused;
  }

  public void ResumeGame()
  {
    gameStatus = GameStatus.Playing;
  }
}

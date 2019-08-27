using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{
  public int[] possibleScoreByDifficulty;
  public int speedBonus;
  public int maxTimeForSpeedBonus;
  public int incorrectPenalty;

  public TextMeshPro scoreText;
  public GameOverPanel gameOverPanelScript;

  private int difficulty;
  private int score;
  private int incorrectCount;
  private Constants.Region region;
  private int timeToAnswer;

  private float timeOfQuestion;

  void Awake()
  {
    GameController.OnSetRegion += gameStart;
    GameController.OnGameOver += gameOver;
    GameController.OnCorrectAnswer += correctAnswer;
    GameController.OnIncorrectAnswer += incorrectAnswer;
    GameController.OnSetQuestion += newQuestion;
    DifficultySwitcher.OnDifficultyChanged += setDifficulty;
  }

  void onDisable()
  {
    GameController.OnSetRegion -= gameStart;
    GameController.OnGameOver -= gameOver;
    GameController.OnCorrectAnswer -= correctAnswer;
    GameController.OnIncorrectAnswer -= incorrectAnswer;
    GameController.OnSetQuestion -= newQuestion;
    DifficultySwitcher.OnDifficultyChanged -= setDifficulty;
  }

  private void setDifficulty(int gameDifficulty)
  {
    difficulty = gameDifficulty;
  }

  private void gameStart(Constants.Region gameRegion)
  {
    region = gameRegion;
    score = 0;
  }

  private void newQuestion(Constants.Region region)
  {
    incorrectCount = 0;
    timeOfQuestion = Time.time;
  }

  private void gameOver()
  {
    gameOverPanelScript.GameOver(score, region, difficulty);
  }

  private void correctAnswer()
  {
    int scoreForQuestion = possibleScoreByDifficulty[difficulty];

    // Add time bonus
    if ((Time.time - timeOfQuestion) < maxTimeForSpeedBonus){
      scoreForQuestion += speedBonus;
    }
    // Lose points for each incorrect answer
    scoreForQuestion -= incorrectCount * incorrectPenalty;
    // Mimimum 20 points for a correct answer
    if (scoreForQuestion < 20) scoreForQuestion = 20;

    score += scoreForQuestion;

    scoreText.text = score.ToString();
  }

  private void incorrectAnswer()
  {
    incorrectCount++;
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverPanel : MonoBehaviour
{
  public string[] difficultyTranslations;

  public TextMeshPro titleText;
  public TextMeshPro difficultyText;
  public TextMeshPro regionText;
  public TextMeshPro scoreText;

  void Start()
  {
    ClearGameOverMenu();
  }

  void ClearGameOverMenu()
  {
    titleText.text = "";
    difficultyText.text = "";
    regionText.text = "";
    scoreText.text = "";
  }

  public void GameOver(int score, Constants.Region region, int difficulty)
  {
    titleText.text = "Game Over";
    difficultyText.text = "Difficulty: " + difficultyTranslations[difficulty];
    regionText.text = "Region: " + region;
    scoreText.text = "Score: " + score;
  }
}

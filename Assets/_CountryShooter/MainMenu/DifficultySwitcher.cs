using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DifficultySwitcher : MonoBehaviour
{
  private int difficulty = 0;
  public string[] difficultyTranslations;
  public TextMeshPro difficultyText;

  public delegate void DifficultyChangedAction(int difficulty);
  public static event DifficultyChangedAction OnDifficultyChanged;

  void OnCollisionEnter(Collision collision)
  {
    int newDifficulty = (difficulty + 1) % 3;
    difficulty = newDifficulty;
    difficultyText.text = difficultyTranslations[newDifficulty];

    if (OnDifficultyChanged != null)
      OnDifficultyChanged(newDifficulty);
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
  public int score = 0;
  public Text scoreText;

  [Tooltip("Don't put a space at the end, this is handled for you.")]
  public string scoreStringPrefix = "Score:";

  // Start is called before the first frame update
  void Start()
  {
    score = 0;
  }

  public void AddScore(int _value = 1)
  {
    score += _value;
    UpdateScoreDisplay();
  }

  void UpdateScoreDisplay()
  {
    scoreText.text = string.Format("{0} {1}", scoreStringPrefix, score);
  }
}

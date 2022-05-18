using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Text scoreText;

    private int score;

    void Start()
    {
        Initialize();
        scoreText = GetComponentInChildren<Text>();
        scoreText.text = "Score:" + score.ToString();
    }

    private void Initialize()
    {
        score = 0;
    }

    public void AddScore(int deletedRows)
    {
        score += 100 * deletedRows;
        scoreText.text = "Score:" + score.ToString();
    }
}

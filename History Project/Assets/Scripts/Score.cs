using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    int score = 0;
    Text scoreText;

    private void Awake()
    {
        scoreText = GetComponent<Text>();
        scoreText.text = "Score: "+score.ToString();
    }

    public void IncreaseScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = "Score: " + score.ToString();
    }
}

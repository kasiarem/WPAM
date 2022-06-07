using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{

    public TextMeshProUGUI scoreText; // text to display score

    public int scoreCount;// float to count player score 
    public float highScoreCount; // float to count / store our high score 
    public bool scoreIncrease; // does our player is alive and should we adding more score

    public bool newHighScore = false; // is there new high score

    // Update is called once per frame
    void Update()
    {

        //managing the situation when we have our score higher than high score
        if (scoreCount > highScoreCount)
        {
            highScoreCount = scoreCount;
            PlayerPrefs.SetFloat("HighScore", highScoreCount); // setting the playerprefs - keeping the high score 
            newHighScore = true;
        }

        // managing the score and high score to be shown on screen
        scoreText.text = "Score: " + Mathf.Round(scoreCount); // display in the UI text which reference is in the"score  field "score : " + value in scorCount variable
        //mathf.round(x) -> we are rounding the number to the closest whole number
    }
    public void addPoints(int howManyPoints) // quick function to add x points
    {
        scoreCount += howManyPoints;
    }
}

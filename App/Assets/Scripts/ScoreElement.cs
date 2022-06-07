using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreElement : MonoBehaviour
{

    public TMP_Text usernameText;
    public TMP_Text stepsText;
    public TMP_Text scoreText;
    public TMP_Text scoreTodayText;


    public void NewScoreElement (string _username, int _steps, int _score, int _scoreToday )
    {
        usernameText.text = _username;
        stepsText.text = _steps.ToString();
        scoreText.text = _score.ToString();
        scoreTodayText.text = _scoreToday.ToString();
    }

}

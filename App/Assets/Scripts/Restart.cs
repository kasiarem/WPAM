using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;   // to load oder scenes


public class Restart : MonoBehaviour
{
    private ScoreManager theScoreManager; // ref to our score manager script

    // Start is called before the first frame update
    void Start()
    {
        theScoreManager = FindObjectOfType<ScoreManager>();

    }

  
    public void GameRestart()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //ponowne odpalenie sceny gdy skończyły nam się życia
        //restart points
        theScoreManager.scoreCount = 0;
        theScoreManager.scoreIncrease = true;
    }
}

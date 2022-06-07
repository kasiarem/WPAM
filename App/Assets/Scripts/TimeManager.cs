using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TimeManager : MonoBehaviour
{
    //------------------- variables for Time taking---------------------
    public  float timeleft, timeReset; //how much time is left for bomb to live
    public bool countingDown = false;
    public TextMeshProUGUI timeText; //text to display time



    // ---------------------Var for time add--------------------
    //public Text timeText;
    public bool countingTime = false; // do we count down the time
    private float timenow = 0.0f; // float to keep time in sec

    public GameObject endScreen;
    private ScoreManager theScoreManager;
    private ShakeDetector theShakeDetector;

    private Spawner theDroneSpawner;
    private FirebaseManager theFirebase;


    private void Start()
    {
        //------------------if counting up then true
        //countingTime = true; // at start we want to start counting
        //---------------------------------------------------------------------
        //------------------if counting down
        // countingTime = false;
        theScoreManager = FindObjectOfType<ScoreManager>();
        theDroneSpawner = FindObjectOfType<Spawner>();
        theFirebase = FindObjectOfType<FirebaseManager>();
        theShakeDetector = FindObjectOfType<ShakeDetector>();

        timeleft = PlayerPrefs.GetFloat("TimeGame", 0);
        if(timeleft == 0)
            countingTime = true;
        else
        {
             countingTime = false;
            timeleft *= 60;
        }
        timeReset=timeleft  ;

        

       
    }
    void Update()
    {
        if (countingTime) // if we are counting time then add real time in between and display
        {
            timenow += Time.deltaTime;
            TimeDisplay(timenow);
        }
        if (countingDown == false && timeleft > 0)
        {
            StartCoroutine(TimerTake());
        }
        // if time run out
        if (timeleft <= 0 && !countingTime)
        {
            endScreen.SetActive(true);
            theScoreManager.scoreIncrease = false; // dont add more score
            TimeReset(); //Reset time of play
            theDroneSpawner.keepSpawning = false; // don't spawn more 
            theFirebase.SaveDataFunc(theScoreManager.scoreCount, theShakeDetector.stepCount);

        }
    }
    void TimeDisplay(float timenow)
    {
        int minutes = Mathf.FloorToInt(timenow / 60); // minutes to display rounded to int
        int seconds = Mathf.FloorToInt(timenow - minutes * 60); // seconds to display rounded to int
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds); // 0:00 first 0 is a placement of variable, after-> how many digits are going to be shown
    }
    public void TimeStart() // start counting
    {
        countingTime = true;
    }
    public void TimeStop() // stop counting
    {
        countingTime = false;
    }

    public void TimeReset()  // reset time count
    {
        if (!countingTime)
            timeleft = timeReset;
        else
            timenow = 0.0f;
    }// Start is called before the first frame update
    IEnumerator TimerTake()
    {
        countingDown = true;
        yield return new WaitForSeconds(1); // Waiting for the second to pass;
        timeleft -= 1; // decrement the time left
        TimeDisplay(timeleft);
        countingDown = false; // so we can count again

    }
    public void EndScreenActive()
    {
        endScreen.SetActive(true);
        theScoreManager.scoreIncrease = false; // dont add more score
        TimeReset(); //Reset time of play
        theDroneSpawner.keepSpawning = false; // don't spawn more 
        //theFirebase.SaveDataFunc(theScoreManager.scoreCount, theShakeDetector.stepCount);
    }
}

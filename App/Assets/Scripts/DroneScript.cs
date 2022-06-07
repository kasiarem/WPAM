using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DroneScript : MonoBehaviour
{

    private int timeleftDrone = 8; //how much time is left
    public bool countingDown = false; // do we count down the time

    int points;
    private ScoreManager theScoreManager;
    private AudioManager theAudio;

    public GameObject drone;
    public GameObject droneText;
    public bool iWasNotActive;

    // Start is called before the first frame update
    void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();
        theScoreManager = FindObjectOfType<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (iWasNotActive && gameObject.activeSelf) // if is active again
        {
            iWasNotActive = false; // to avoid coming back each update
            countingDown = false; // to start counting down time it has to be active
        }
        // if we are not counting yet/right now and there is time left we want to count down
        if (countingDown == false && timeleftDrone > 0)
        {
            StartCoroutine(TimerTake());
        }
        // if time run out
        if (timeleftDrone <= 0)
        {
            gameObject.SetActive(false); //deactivate game object
            iWasNotActive = true;
            countingDown = true;
            timeleftDrone = 8; // seting time for future actiation

        }
    }
    public int HowManyPoints()
    {
        int point;
        if (gameObject.transform.name == "DroneWhite(Clone)")
        {
            point = 20;
        }
        else
            point = 40;
        return point;
    }
    public IEnumerator GotShotDown()
    {
        theAudio.PlayDestroy();
        drone.SetActive(false);
       
        droneText.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        points = HowManyPoints();
        theScoreManager.addPoints(points);
        droneText.SetActive(false);
        gameObject.SetActive(false);
        drone.SetActive(true);
    }
    IEnumerator TimerTake()
    {
        countingDown = true;
        yield return new WaitForSeconds(1); // Waiting for the second to pass;
        timeleftDrone -= 1; // decrement the time left
        countingDown = false; // so we can count again
    }
}

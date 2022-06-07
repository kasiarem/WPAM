using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    public bool stepCounting, dinoActive, cardioMode;
    int stepStart, stepEnd, stepDiff;
    private ShakeDetector theShakeDetector;
    private AudioManager theAudioManager;
    private ScoreManager theScoreManager;
    public GameObject dino;
    float dinoPresent;

    private void Start()
    {
        theShakeDetector = FindObjectOfType<ShakeDetector>();
        theAudioManager = FindObjectOfType<AudioManager>();
        theScoreManager = FindObjectOfType<ScoreManager>();
        
        dinoActive = false;
        dinoPresent = PlayerPrefs.GetFloat("dino",0);
        //if cardio mode then 1
        if (dinoPresent == 0)
            cardioMode = false;
        else
            cardioMode = true;

    }
    // Update is called once per frame
    void Update()
    {
        if(stepCounting == false && cardioMode)
        {
            StartCoroutine(CheckSteps());
            
        }
    }
    public IEnumerator CheckSteps()
    {
        stepCounting = true;
        stepStart = theShakeDetector.stepCount;
        yield return new WaitForSeconds(10);
        stepEnd = theShakeDetector.stepCount;
        stepDiff = stepEnd - stepStart;
        if (stepDiff < 5)
        {
            if (dinoActive)
            {
                theScoreManager.addPoints(-10);
            }else
                DinosaurActivate();
        }
        else
            DinosaurDeactivate();
        stepCounting = false;
    }

    private void DinosaurActivate()
    {
        dinoActive = true;
        dino.SetActive(true);
        StartCoroutine(theAudioManager.PlayFootstep());
        
        
    }
    private void DinosaurDeactivate()
    {
        dinoActive = false;
        dino.SetActive(false);

    }
}

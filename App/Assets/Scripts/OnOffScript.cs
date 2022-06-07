using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OnOffScript : MonoBehaviour
{
    public TextMeshProUGUI timeMode, cardioMode;
    public bool timeModeOn = true;
    public bool cardioModeOn = true;
    public GameObject timeChoices;
    public int[] timeValues;
    public TextMeshProUGUI timeValue;
    private int selectedTime = 0; // selected time
    private SceneSwitch theSceneSwitch;
    private InterstitalAds theAd;
    public GameObject tooLittleScreen;
    private MetalsMenager theMetalsManager;
    
    

    private void Start()
    {
        theSceneSwitch = FindObjectOfType<SceneSwitch>();
        theAd = FindObjectOfType<InterstitalAds>();
        theMetalsManager = FindObjectOfType<MetalsMenager>();
    }



    public void ChangeTimeMode()
    {
        if (timeModeOn)
        {
            timeModeOn = false;
            timeMode.text = "Off";
            timeChoices.SetActive(false);
        }
        else
        {
            timeModeOn = true;
            timeMode.text = "On";
            timeChoices.SetActive(true);

        }
    }
    public void ChangeCardioMode()
    {
        if (cardioModeOn)
        {
            cardioModeOn = false;
            cardioMode.text = "Off";
        }
        else
        {
            cardioModeOn = true;
            cardioMode.text = "On";
        }
    }
    public void TimeLeft()
    {
        selectedTime--;
        if (selectedTime < 0) // we dont want to go outside the array so when is less than zero we stay with element 0
        {
            selectedTime = 0;
        }
        UpdateTimeValue();
    }
    public void TimeRight()
    {
        selectedTime++;
        if (selectedTime > timeValues.Length - 1) // we dont want to go outside the array so when it is grater than length of array - 1 (arrays starts with 0 position no 1) then we stay with the last element of array
        {
            selectedTime = timeValues.Length - 1;
        }
        UpdateTimeValue(); // update the resolution value 
    }

    public void UpdateTimeValue() // function to change the showed value in the brackets
    {
        timeValue.text = timeValues[selectedTime].ToString() + " min";
        Debug.Log(timeValues[selectedTime]);
    }
    public void PlayGame()
    {
        if (GameData.Metals >= 10) {
            if (timeMode.text == "On")
                PlayerPrefs.SetFloat("TimeGame", timeValues[selectedTime]);
            else
                PlayerPrefs.SetFloat("TimeGame", 0);

            if (cardioModeOn == true)
                PlayerPrefs.SetFloat("dino", 1);
            else
                PlayerPrefs.SetFloat("dino", 0);

            //---------------------AD
            theAd.ShowAd();
            theMetalsManager.DecrementMetals(10);

            theSceneSwitch.SwitchSceneAB();
        }
        else
        {
            tooLittleScreen.SetActive(true);
        }
    }
}

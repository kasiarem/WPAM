using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using TMPro;


public class ShakeDetector : MonoBehaviour
{
    public float ShakeDetectionThreshold;
    public float MinShakeInterval;

    private float sqrShakeDetectionThreshold;
    private float timeSinceLastShake;

    public TextMeshProUGUI stepCounterText;
    public int stepCount = 0;



    // Start is called before the first frame update
    void Start()
    {
        sqrShakeDetectionThreshold = Mathf.Pow(ShakeDetectionThreshold, 2); //squered because we will be using squere magnitute of acceleration vxtor
        //we are omitting sqrt with vector
    }

    // Update is called once per frame
    void Update()
    {
        if (StepCounter.current != null)
        {
            InputSystem.EnableDevice(StepCounter.current);
            stepCount = StepCounter.current.stepCounter.ReadValue();
        }


        if (StepCounter.current == null)
            Debug.Log("StepCounter is not present");
        stepCounterText.text = "Steps: " + stepCount.ToString();

        if (StepCounter.current != null)
            InputSystem.DisableDevice(StepCounter.current);


        if (Input.acceleration.sqrMagnitude >= sqrShakeDetectionThreshold && Time.unscaledTime >= timeSinceLastShake + MinShakeInterval)
        {
            // do sth with the fact it is big enough
            stepCount++;
            stepCounterText.text = "Steps: " + stepCount.ToString();
            timeSinceLastShake = Time.unscaledTime;
        }
    }
}

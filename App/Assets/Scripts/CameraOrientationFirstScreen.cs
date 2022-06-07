using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrientationFirstScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        //Screen.SetResolution(720, 1480,  true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

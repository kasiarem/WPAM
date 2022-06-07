using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrientation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       Screen.orientation = ScreenOrientation.LandscapeLeft;
       //Screen.SetResolution( 1480, 720, true);

    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAtTouch : MonoBehaviour
{
    private AudioManager theAudioManager;
    private Shoot theShoot;
    private void Start()
    {
        theShoot = FindObjectOfType<Shoot>();
        theAudioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            theShoot.ShootNow();

        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dinoAnim : MonoBehaviour
{
    private AudioManager theAudio;

    private void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();
    }


    // Update is called once per frame
    void Update()
    {
        StartCoroutine(theAudio.PlayRoar());
    }

}

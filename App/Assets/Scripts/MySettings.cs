using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MySettings : MonoBehaviour
{
    public Toggle vibrations;
    public AudioMixer theMixer; // we create the mixer then to have access to chose mixer
    public Slider  musicSlider, sfxSlider;// we need that to have information at which volume (according to slider ) we supposed to be
    public bool vibrationsON;


    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("vibrations"))
        {
            if (PlayerPrefs.GetFloat("vibrations") == 0)
                vibrations.isOn = false;
            else
                vibrations.isOn = true;
        }
        if (PlayerPrefs.HasKey("MusicVol"))
        {

            theMixer.SetFloat("MusicVol", PlayerPrefs.GetFloat("MusicVol")); //load player preferences of master volume to theMixer
            musicSlider.value = PlayerPrefs.GetFloat("MusicVol"); // and load to the slider

        }
        if (PlayerPrefs.HasKey("SFXVol"))
        {

            theMixer.SetFloat("SFXVol", PlayerPrefs.GetFloat("SFXVol")); //load player preferences of master volume to theMixer
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVol"); // and load to the slider

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SetMusicVolume()
    {
        theMixer.SetFloat("MusicVol", musicSlider.value);
        PlayerPrefs.SetFloat("MusicVol", musicSlider.value); // save the player preferences
    }
    public void SetSFXVolume()
    {
        theMixer.SetFloat("SFXVol", sfxSlider.value);
        PlayerPrefs.SetFloat("SFXVol", sfxSlider.value); // save the player preferences
    }
    public void SetVibrations()
    {
        if(vibrations.isOn == false)
        {
            PlayerPrefs.SetFloat("vibrations", 0);
            Debug.Log("off");
        }
        else
        {
            PlayerPrefs.SetFloat("vibrations", 1);
            Debug.Log("on");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio; //to have access to audio
public class OptionsMenu : MonoBehaviour
{
    public Toggle fullscreenToggle, vsyncToggle; // toggles which are present in our optons screen

    public ResolutionItem[] resolutions; // array which is called resolutions and will include our resolution options

    private int selectedResolution;

    public Text resolutionValue;

    public AudioMixer theMixer; // we create the mixer then to have access to chose mixer

    public Slider masterSlider, musicSlider, sfxSlider;// we need that to have information at which volume (according to slider ) we supposed to be
    public Text masterValue, musicValue, sfxValue; // we need that to have a value between 0-100 to show on our menu

    public AudioSource sfxLoop; // so we can later start and stop playing effect

    // Start is called before the first frame update
    void Start()
    {
        // we checking if the set value are true and if not then we changing them to match the current settings
        fullscreenToggle.isOn = Screen.fullScreen; //if this is full screen then the toggle id on and if not then the togle is off
        if(QualitySettings.vSyncCount == 0) // if this is equal 0 then the toggle should be off
        {
            vsyncToggle.isOn = false; // we clear the toggle
        }
        else
        {
            vsyncToggle.isOn = true;
        }
        //checking throgh the options and upload the correct resolution on the Text field
        bool foundResolution = false;
        for(int i = 0; i< resolutions.Length; i++)
        {
            //if the width of screen is equal to the horizontal value in i element in array and screen height is equal to it's vertical value
            if (Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)
            {
                //then we have our resolution 
                foundResolution = true;
                
                selectedResolution = i;
               // and we can set this value in the text field
                UpdateResolutionValue();
            }
        }
        if(!foundResolution)  //if we didnt find the resolution
        {
            resolutionValue.text = Screen.width.ToString() + " x " + Screen.height.ToString();
        }

        //checking player prefereces
        if (PlayerPrefs.HasKey("MasterVol"))
        {

            theMixer.SetFloat("MasterVol", PlayerPrefs.GetFloat("MasterVol")); //load player preferences of master volume to theMixer
            masterSlider.value = PlayerPrefs.GetFloat("MasterVol"); // and load to the slider
            
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
        //no matter if the preferences exist we want the slider to have a proper value so:
        masterValue.text = (masterSlider.value + 80).ToString(); // i am taking the value from slider + 80 and putting that in text field
        musicValue.text = (musicSlider.value + 80).ToString(); // i am taking the value from slider + 80 and putting that in text field
        sfxValue.text = (sfxSlider.value + 80).ToString(); // i am taking the value from slider + 80 and putting that in text field


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResolutionLeft()
    {
        selectedResolution--; 
        if(selectedResolution < 0) // we dont want to go outside the array so when is less than zero we stay with element 0
        {
            selectedResolution = 0;
        }
        UpdateResolutionValue();
    }
    public void ResolutionRight()
    {
        selectedResolution++;    
        if(selectedResolution > resolutions.Length - 1) // we dont want to go outside the array so when it is grater than length of array - 1 (arrays starts with 0 position no 1) then we stay with the last element of array
        {
            selectedResolution = resolutions.Length - 1;
        }
        UpdateResolutionValue(); // update the resolution value 
    }
     
    public void UpdateResolutionValue() // function to change the showed value in the brackets
    {
        resolutionValue.text = resolutions[selectedResolution].horizontal.ToString() + " x " + resolutions[selectedResolution].vertical.ToString(); 
    // text window = the value of resolution in horizontal changed to string + " x " + the value of selected resolution vertical changed to string
    }

    public void ApplyGraphics()
    {
        
        //vsync is all about frames per seconds
        //VSYNC if you have a speed computer it can use all your graphics card and give you for example thousands frames per second and that is why this should be optional
        if (vsyncToggle.isOn)
        {
            QualitySettings.vSyncCount = 1; //when we want to be active
        }
        else
        {
            QualitySettings.vSyncCount = 0; //when we want vsyync to be off

        }

        //set Resolution
        Screen.SetResolution(resolutions[selectedResolution].horizontal, resolutions[selectedResolution].vertical, fullscreenToggle.isOn);
        // set resolution and fullscreen if the toggle is on
    }
    public void SetMasterVolume()
    {
        masterValue.text = (masterSlider.value + 80).ToString(); // i am taking the value from slider + 80 and putting that in text field
        theMixer.SetFloat("MasterVol", masterSlider.value); // taking the value from slider and giving it to the mixer
        PlayerPrefs.SetFloat("MasterVol", masterSlider.value); // save the player preferences
    }
    public void SetMusicVolume()
    {
        musicValue.text = (musicSlider.value + 80).ToString();
        theMixer.SetFloat("MusicVol", musicSlider.value);
        PlayerPrefs.SetFloat("MusicVol", musicSlider.value); // save the player preferences
    }
    public void SetSFXVolume()
    {
        sfxValue.text = (sfxSlider.value + 80).ToString();
        theMixer.SetFloat("SFXVol", sfxSlider.value);
        PlayerPrefs.SetFloat("SFXVol", sfxSlider.value); // save the player preferences
    }
    public void PlaySFXLoop()
    {
        sfxLoop.Play(); //play sfxLooop
    }
    public void StopSFXLoop()
    {
        sfxLoop.Stop(); //stop playing sfxloop
    }
}

[System.Serializable] // we do this because if we make a system that serialize table we can display
//this back in unity editors as an individual objects
// without this we couldnt see this list in unity
public class ResolutionItem
{
    public int horizontal, vertical; // it will be our horizontal & vertical resolution

}

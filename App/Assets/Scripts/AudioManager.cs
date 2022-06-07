using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer theMixer;
    public AudioSource clickSound,footSound, attackSound, shootSound, destroySound, soundDink, musicSound; //sound effects to play

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("MasterVol"))
        {

            theMixer.SetFloat("MasterVol", PlayerPrefs.GetFloat("MasterVol")); //load player preferences of master volume to theMixer

        }
        if (PlayerPrefs.HasKey("MusicVol"))
        {

            theMixer.SetFloat("MusicVol", PlayerPrefs.GetFloat("MusicVol")); //load player preferences of master volume to theMixer

        }
        if (PlayerPrefs.HasKey("SFXVol"))
        {

            theMixer.SetFloat("SFXVol", PlayerPrefs.GetFloat("SFXVol")); //load player preferences of master volume to theMixer

        }

    }

    public void PlayClick()
    {
        clickSound.Play();
    }
    public void PlayDestroy()
    {
        destroySound.Play();
    }
    public IEnumerator PlayRoar()
    {
        musicSound.Pause();
        attackSound.Play();
        yield return new WaitForSeconds(17);
        musicSound.UnPause();
    }
    public IEnumerator PlayFootstep()
    {
        footSound.Play();
        yield return new WaitForSeconds(2);
        footSound.Play();
        StartCoroutine(PlayRoar());
    }
    public void PlatShoot()
    {
        shootSound.Play();
    }
    public void PlayDisturbance()
    {
        soundDink.Play();
    }

}

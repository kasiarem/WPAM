using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public GameObject loadingScreen;
    private UIScreenManager theUI;
    private FirebaseManager theFirebaseManager;
    int sceneNumb;
    public GameObject loadingBtn;

    private void Start()
    {
        theFirebaseManager= FindObjectOfType<FirebaseManager>();
        theUI = FindObjectOfType<UIScreenManager>();
        if (PlayerPrefs.HasKey("Screen") && SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (PlayerPrefs.GetFloat("Screen") == 0)
            {
                theUI.LoginScreen();
            }
            else if (PlayerPrefs.GetFloat("Screen") == 1)
            {
                theUI.SettingsScreen();
                PlayerPrefs.SetFloat("Screen", 2);
            }
            else
            {
                theUI.GameChoiceScreen();
            }
        }
           
    }
    public void SwitchSceneAB()
    {
        theUI.ClearScreen();
        loadingScreen.SetActive(true);
        
        //SceneManager.LoadScene(sceneNumb);
        StartCoroutine(LoadStart(sceneNumb));
    }
    public void SwitchSceneBA()
    {
        loadingScreen.SetActive(true);

        //SceneManager.LoadScene(sceneNumb);
        StartCoroutine(LoadStart(sceneNumb));
    }
    public IEnumerator LoadStart(int number)
    {

        yield return new WaitForSeconds(3);
        loadingBtn.SetActive(true);
    }
    public void LoadNew()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
            SceneManager.LoadScene(1);
        else
        {

            //PlayerPrefs.SetFloat("Screen", 2);
            SceneManager.LoadScene(0);
            //theFirebaseManager.LoginNew();
        }
    }
}

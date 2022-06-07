using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // we need it to load scenes
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string firstLevel; // which level we want to load by clicking "play"

    public GameObject optionsScreen;  // new object in which there will be a Option Screen

    public GameObject loadingScreen, loadingIcon; // reference to our loading screen, loading icon 
    public Text loadingText; // reference to loading text

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
       // SceneManager.LoadScene(firstLevel);
        StartCoroutine(LoadStart());

    }

    public void OpenOptions()
    {
        optionsScreen.SetActive(true); // activate Options Screen
    }

    public void CloseOptions()
    {
        optionsScreen.SetActive(false); //deactivate Options Screen
    }
    
    public void QuitGame()
    {
        Application.Quit();  //quit the application
    }
    public IEnumerator LoadStart()
    {
        loadingScreen.SetActive(true);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(firstLevel); // what it will do is that the scene will be loaded in backround
        // so we can have our loading screen on and in backround the next scene will be loaded(in this case main menu)
        // we need loading screen so the player wont be confused when the next scene can take a minute to load and our game will be just frozen
        asyncLoad.allowSceneActivation = false; // so we are blocking the ext scene activation after it will be ready

        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= .9f) // if it is greater than 90 % loaded
            {
                loadingText.text = "Press any key to continue";  // we inform the player that game is load and to push any button to continue
                //helpful because somebody could go away from the screen
                loadingIcon.SetActive(false);     //we dont want to activate our screen just yet

                if (Input.anyKeyDown) // if somebody clicked any button on keyboard
                {
                    asyncLoad.allowSceneActivation = true; // we allowing the screen to activate
                    Time.timeScale = 1f; // we need to remember to make a time run again
                }
            }
            yield return null; // we need that so the program wont be trying to exacute every single line of code in LoadMain at once
            // we inform the program that we end the loop once and stop searching for change
            // so it wont freze up the whole system
        }
    }

}

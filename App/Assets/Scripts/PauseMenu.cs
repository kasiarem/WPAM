using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;   // to load oder scenes
using UnityEngine.UI; // to use ui elements
/// </summary>

public class PauseMenu : MonoBehaviour
{
    public GameObject optionsScreen, pauseScreen, pointsSetUp; // to refer to our option + pause Screen + score text fields
    // Start is called before the first frame update
    public string mainMenuScene; // to get the name of the main menu scene

    public bool isPaused; // it will help us to determine if the game is paused or not

    public GameObject loadingScreen, loadingIcon; // reference to our loading screen, loading icon 
    public Text loadingText; // reference to loading text

   


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // if somebody pressed the escape button
        {
            PauseUnpause();
        }
    }
    public void PauseUnpause()
    {
        if (!isPaused)
        {
            pauseScreen.SetActive(true); // we setting the screen active
            isPaused = true; // we changing it to be true because we set the pause screen active

            Time.timeScale = 0f; // time will move at zero scale - stopping game
        }
        else // if the isPaused is true and somebody clicked the resume button or the escape button
        {
            pauseScreen.SetActive(false);
            isPaused = false; // so now when somebody clicks on escape button menu will appaer

            Time.timeScale = 1f; // time will be running again
        }
    }
    public void OpenOptions()
    {
        optionsScreen.SetActive(true);  //switch on optionsScreen
        pointsSetUp.SetActive(false); // swich off the text fields with points
    }
    public void CloseOptions()
    {
        optionsScreen.SetActive(false);  //switch off optionsScreen
        pointsSetUp.SetActive(true); // swich on the text fields with points
    }
    public void BackToMain()
    {      
        pointsSetUp.SetActive(false); // swich off the text fields with points

        // SceneManager.LoadScene(mainMenuScene); //load main menu scene
        //Time.timeScale = 1f; // time will be running again
        StartCoroutine(LoadMain());
    }
    public IEnumerator LoadMain()
        {
        loadingScreen.SetActive(true);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(mainMenuScene); // what it will do is that the scene will be loaded in backround
        // so we can have our loading screen on and in backround the next scene will be loaded(in this case main menu)
        // we need loading screen so the player wont be confused when the next scene can take a minute to load and our game will be just frozen
        asyncLoad.allowSceneActivation = false; // so we are blocking the ext scene activation after it will be ready

        while (!asyncLoad.isDone)
        {
            if(asyncLoad.progress >= .9f) // if it is greater than 90 % loaded
            {
                loadingText.text = "Press any key to continue";  // we inform the player that game is load and to push any button to continue
                //helpful because somebody could go away from the screen
                loadingIcon.SetActive(false);     //we dont want to activate our screen just yet

                if(Input.anyKeyDown) // if somebody clicked any button on keyboard
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

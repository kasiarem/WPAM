using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScreenManager : MonoBehaviour
{
    public static UIScreenManager instance;
    //Screen object variables
    public GameObject loginUI;
    public GameObject registerUI;
    public GameObject userDataUI;
    public GameObject scoreboardUI;
    public GameObject gameChoseUI;
    public GameObject gameChoseUILeave;
    public GameObject gameChoseUIOff;
    public GameObject upperHUD;
    public GameObject gemeChoseUI_secondS;
    public GameObject settigsUI;
    public GameObject shopUI;
    public GameObject upsUI;
    public GameObject metalCount;
    private MetalsMenager theMetals;

    private FirebaseManager theFirebaseManager;

    private void Start()
    {
        theMetals = FindObjectOfType<MetalsMenager>();

        theFirebaseManager = FindObjectOfType<FirebaseManager>();
        if (theFirebaseManager.isLogIn)
        {
            GameChoiceScreen();
        }
    }
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }
    public void ClearScreen() //Turn off all screens
    {
        loginUI.SetActive(false);
        registerUI.SetActive(false);
        userDataUI.SetActive(false);
        scoreboardUI.SetActive(false);
        settigsUI.SetActive(false);
        upperHUD.SetActive(false);
        gameChoseUI.SetActive(false);
        gemeChoseUI_secondS.SetActive(false);
    }
   
    //Functions to change the login screen UI
    public void LoginScreen() //Back button
    {
        ClearScreen();
        loginUI.SetActive(true);
    }
    public void RegisterScreen() // Regester button
    {
        ClearScreen();
        registerUI.SetActive(true);
    }
    public void UserDataScreen() //Logged in
    {
        ClearScreen();
        userDataUI.SetActive(true);
        upperHUD.SetActive(true);
    }

    public void ScoreboardScreen() //Scoreboard button
    {
        ClearScreen();
        scoreboardUI.SetActive(true);
        upperHUD.SetActive(true);
    }
    public void SettingsScreen()
    {
        
        ClearScreen();
        settigsUI.SetActive(true);
        upperHUD.SetActive(true);
    }
    public void GameChoiceScreen()
    {
        ClearScreen();
        gameChoseUI.SetActive(true);
        upperHUD.SetActive(true);

    }
    public void GameChoiceSecondScreen()
    {
        upperHUD.SetActive(false);
        gameChoseUIOff.SetActive(false);
        gemeChoseUI_secondS.SetActive(true);
    }
    public void AfterPlayCleanUp()
    {
        gameChoseUIOff.SetActive(true);
        ClearScreen();
        gameChoseUI.SetActive(true);
        upperHUD.SetActive(true);

    }
    public void OpenShop()
    {
        shopUI.SetActive(true);
    }
    public void CloseShop()
    {
        shopUI.SetActive(false);
    }
    public void CloseUps()
    {
        upsUI.SetActive(false);
    }
    public void CloseChosenGame()
    {
       
        gameChoseUIOff.SetActive(true);
        ClearScreen();
        gameChoseUI.SetActive(true);
        upperHUD.SetActive(true);
    }
    public void AddMetal()
    {
        theMetals.AddMetals(1);
        metalCount.SetActive(true);
    }
}

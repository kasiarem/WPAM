using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using TMPro;
using System;
using System.Linq;

public class FirebaseManager : MonoBehaviour
{
    int giveMeScore;
    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;
    public DatabaseReference DBreference;

    //Login variables
    [Header("Login")]
    public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginField;
    public TMP_Text warningLoginText;
    public TMP_Text confirmLoginText;
    public bool isLogIn;

    //Register variables
    [Header("Register")]
    public TMP_InputField usernameRegisterField;
    public TMP_InputField emailRegisterField;
    public TMP_InputField passwordRegisterField;
    public TMP_InputField passwordRegisterVerifyField;
    public TMP_Text warningRegisterText;


    //User data variables
    [Header("UserData")]
    public TMP_InputField userNameField;
    public TMP_InputField xpField;
    public TMP_InputField killsField;
    public TMP_InputField deathsField;
    public GameObject scoreElement;
    public Transform scoreboardContent;
    public TMP_InputField score;
    public TMP_InputField steps;
    public TMP_InputField dateG;
    public TMP_Text calendarMsg;

    public TextMeshProUGUI scoreGame;
    public TextMeshProUGUI stepsGame;


    private void Start()
    {
        
    }
    private void Awake()
    { 
        //Check if all of the necessary dependencies for Firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependecies:  " + dependencyStatus);
            }
        }

        );
   //StartCoroutine(Login(PlayerPrefs.GetString("email"), PlayerPrefs.GetString("password")));
    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
    }
    public void ClearLoginFields()
    {
        emailLoginField.text = "";
        passwordLoginField.text = "";
    }
    public void ClearRegisterFields()
    {
        usernameRegisterField.text = "";
        emailRegisterField.text = "";
        passwordRegisterField.text = "";
        passwordRegisterVerifyField.text = "";
    }

    public void LoginButton()
    {
        StartCoroutine(Login(emailLoginField.text, passwordLoginField.text));
    }
    public void LoginNew()
    {
        //StartCoroutine(Login(PlayerPrefs.GetString("email"), PlayerPrefs.GetString("password")));
    }

    public void RegisterButton()
    {
        StartCoroutine(Register(emailRegisterField.text, passwordRegisterField.text, usernameRegisterField.text));

    }

    public void SignOutButton()
    {
        PlayerPrefs.SetFloat("Screen", 0);
        PlayerPrefs.SetString("email", "");
        PlayerPrefs.SetString("password", "");
        auth.SignOut();
        isLogIn = false;
        UIScreenManager.instance.LoginScreen();
        ClearRegisterFields();
        ClearLoginFields();
    }
    public void SaveDataButton()
    {
        StartCoroutine(UpdateUsernameAuth(userNameField.text));
        StartCoroutine(UpdateUsernameDatabase(userNameField.text));

        //StartCoroutine(UpdateXp(int.Parse(xpField.text)));
       // StartCoroutine(UpdateKills(int.Parse(killsField.text)));
       // StartCoroutine(UpdateDeaths(int.Parse(deathsField.text)));
    }
    public void SaveGame()
    {
        string date = GetDate();
        StartCoroutine(UpdateScoreManual(int.Parse(scoreGame.text), date));
        StartCoroutine(UpdateStepsManual(int.Parse(stepsGame.text), date));
        StartCoroutine(UpdateMainScore(int.Parse(scoreGame.text)));
    }
    public void SaveDataFunButton()
    {
        StartCoroutine(UpdateScoreManual(int.Parse(score.text), dateG.text));
        StartCoroutine(UpdateStepsManual(int.Parse(steps.text), dateG.text));
        StartCoroutine(UpdateMainScore(int.Parse(score.text)));

        
    }
    public void SaveDataFunc(int score, int steps)
    {
        StartCoroutine(UpdateScore(score));
        StartCoroutine(UpdateSteps(steps));
    }
    public void ScoreboardButton()
    {
        string date = GetDate();
        StartCoroutine(LoadScoreboardData(date));
    }

    private IEnumerator Login(string _email, string _password)
    {
        //Call the Firebase auth signin function passing the email and password
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;
            }

            warningLoginText.text = message;
        }
        else
        {
            //User is now logged in
            //Now get the result
            User = LoginTask.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);
            warningLoginText.text = "";
            confirmLoginText.text = "Logged In";
            
            PlayerPrefs.SetFloat("Screen", 2);
            PlayerPrefs.SetString("email", _email);
            PlayerPrefs.SetString("password", _password);
            string date = GetDate();
            StartCoroutine(LoadCalendarData(date));
            StartCoroutine(LoadScoreboardData(date));

            yield return new WaitForSeconds(2);

            userNameField.text = User.DisplayName;
            UIScreenManager.instance.GameChoiceScreen(); // change to choice screen
            confirmLoginText.text = "";
            ClearLoginFields();
            ClearRegisterFields();
        }
    }

    private IEnumerator Register(string _email, string _password, string _username)
    {
        if (_username == "")
        {
            //If the username field is blank show a warning
            warningRegisterText.text = "Missing Username";
        }
        else if (passwordRegisterField.text != passwordRegisterVerifyField.text)
        {
            //If the password does not match show a warning
            warningRegisterText.text = "Password Does Not Match!";
        }
        else
        {
            //Call the Firebase auth signin function passing the email and password
            var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
            //Wait until the task completes
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                //If there are errors handle them
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Register Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak Password";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email Already In Use";
                        break;
                }
                warningRegisterText.text = message;
            }
            else
            {
                //User has now been created
                //Now get the result
                User = RegisterTask.Result;

                if (User != null)
                {
                    //Create a user profile and set the username
                    UserProfile profile = new UserProfile { DisplayName = _username };

                    //Call the Firebase auth update user profile function passing the profile with the username
                    var ProfileTask = User.UpdateUserProfileAsync(profile);
                    //Wait until the task completes
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {
                        //If there are errors handle them
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        warningRegisterText.text = "Username Set Failed!";
                    }
                    else
                    {
                        //Username is now set
                        //Now return to login screen
                        UIScreenManager.instance.LoginScreen();
                        warningRegisterText.text = "";
                        ClearLoginFields();
                        ClearRegisterFields();
                    }
                }
            }
        }
    }
    private IEnumerator UpdateUsernameAuth(string _username)
    {
        //Create a user profile and set the username
        UserProfile profile = new UserProfile { DisplayName = _username };

        //Call the Firebase auth update user profile function passing the profile with the username
        var ProfileTask = User.UpdateUserProfileAsync(profile);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

        if (ProfileTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
        }
        else
        {
            //Auth username is now updated
        }
    }

    private IEnumerator UpdateUsernameDatabase(string _username)
    {
        //Set the currently logged in user username in the database
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("username").SetValueAsync(_username);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Database username is now updated
        }
    }

    private IEnumerator UpdateXp(int _xp)
    {
        //Set the currently logged in user xp
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("xp").SetValueAsync(_xp);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Xp is now updated
        }
    }
    private string GetDate()
    {
        string result_m;
        string date_y = System.DateTime.UtcNow.ToLocalTime().ToString("yyyy");
        string date_m = System.DateTime.UtcNow.ToLocalTime().ToString("MM");
        string date_d = System.DateTime.UtcNow.ToLocalTime().ToString("dd");
        var temp_m = date_m.ToCharArray();
        var temp_d = date_d.ToCharArray();

        if (temp_m[0].Equals('0'))
        {
            date_m = date_m.Substring(1);
        }
        if (temp_d[0].Equals('0'))
        {
            date_d = date_d.Substring(1);
        }

        string date = date_y + "_" + date_m + "_" + date_d;
        Debug.Log(date);
        return date;
    }
    private IEnumerator UpdateScore(int _score )
    {
        string date = GetDate();
        string score = "";
        
        //Get the currently logged in user data
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("dates").Child(date).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else if (DBTask.Result.Value == null)
        {
            //No data exists yet
            score = "0";
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;

            score = snapshot.Child("score").Value.ToString();
        }
        int end = Int32.Parse(score);
        //Set the currently logged in user deaths
        _score = _score + end;
        var DBTask1 = DBreference.Child("users").Child(User.UserId).Child("dates").Child(date).Child("score").SetValueAsync(_score);

        yield return new WaitUntil(predicate: () => DBTask1.IsCompleted);

        //DBTask = DBreference.Child("users").Child(User.UserId).Child("xp").Child(xpField.text).Child("kills").Child(killsField.text).Child("deaths").SetValueAsync(_deaths);

        // yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask1.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask1.Exception}");
        }
        else
        {
            //Deaths are now updated
        }
    }
    private IEnumerator UpdateSteps(int _steps)
    {
        string steps ="";
        string date = GetDate();
        //Get the currently logged in user data
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("dates").Child(date).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else if (DBTask.Result.Value == null)
        {
            //No data exists yet
            steps = "0";
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;

            steps = snapshot.Child("steps").Value.ToString();
        }
        int end = Int32.Parse(steps);
        //Set the currently logged in user deaths
        _steps = _steps + end;
        var DBTask1 = DBreference.Child("users").Child(User.UserId).Child("dates").Child(date).Child("score").SetValueAsync(_steps);

        yield return new WaitUntil(predicate: () => DBTask1.IsCompleted);

        if (DBTask1.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask1.Exception}");
        }
        else
        {
            //Deaths are now updated
        }
    }
    private IEnumerator UpdateScoreManual(int _score, string date)
    {
        
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("dates").Child(date).Child("score").SetValueAsync(_score);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        //DBTask = DBreference.Child("users").Child(User.UserId).Child("xp").Child(xpField.text).Child("kills").Child(killsField.text).Child("deaths").SetValueAsync(_deaths);

        // yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Deaths are now updated
        }
    }
    private IEnumerator UpdateStepsManual(int _steps, string date)
    {
        
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("dates").Child(date).Child("steps").SetValueAsync(_steps);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Deaths are now updated
        }
    }
    public IEnumerator LoadCalendarData(string date)
    {
        //Get the currently logged in user data
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("dates").Child(date).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else if (DBTask.Result.Value == null)
        {
            //No data exists yet
            //score.text = "0";
           //steps.text = "0";
            calendarMsg.text = "Hi, your achievemnts from " + date + "\n\n" +"Ups, it looks like you didn't used this app that day";
           
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;

            calendarMsg.text = "Hi, your achievemnts from " + date + "\n\n" + "Score: " +snapshot.Child("score").Value.ToString() + "\n" +"Steps: " + snapshot.Child("steps").Value.ToString();

        }
    }
    private IEnumerator UpdateMainScore(int _kills)
    {
        //Set the currently logged in user kills
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("score").SetValueAsync(_kills);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Kills are now updated
        }
    }

    private IEnumerator UpdateDeaths(int _deaths)
    {
        //Set the currently logged in user deaths
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("deaths").SetValueAsync(_deaths);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Deaths are now updated
        }
    }

    private IEnumerator LoadUserData()
    {
        //Get the currently logged in user data
        var DBTask = DBreference.Child("users").Child(User.UserId).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else if (DBTask.Result.Value == null)
        {
            //No data exists yet
            xpField.text = "0";
            killsField.text = "0";
            deathsField.text = "0";
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;

            xpField.text = snapshot.Child("xp").Value.ToString();
            killsField.text = snapshot.Child("kills").Value.ToString();
            deathsField.text = snapshot.Child("deaths").Value.ToString();
        }
    }

    public IEnumerator LoadScoreData(string date)
    {
        Debug.Log("CheckIn");
        giveMeScore = 0;
        //Get the currently logged in user data
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("dates").Child(date).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else if (DBTask.Result.Value == null)
        {
            //No data exists yet
            giveMeScore = 0;
            Debug.Log("0");

        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;

            giveMeScore = int.Parse(snapshot.Child("score").Value.ToString());
            Debug.Log("sth" + giveMeScore);

        }
    }
    private IEnumerator LoadScoreboardData(string date)
    {
        
        //Get all the users data ordered by score amount
        var DBTask = DBreference.Child("users").OrderByChild("score").GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            
            
            string date_firstH = date.Substring(0, 7);
            Debug.Log(date_firstH);
            string date_secondH = date.Substring(7, 2);
            Debug.Log("date second part before: " + date_secondH);
            int day = int.Parse(date_secondH);

            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;

            //Destroy any existing scoreboard elements
            foreach (Transform child in scoreboardContent.transform)
            {
                Destroy(child.gameObject);
            }

            //Loop through every users UID
            foreach (DataSnapshot childSnapshot in snapshot.Children.Reverse<DataSnapshot>())
            {
                //string username = childSnapshot.Child("username").Value.ToString();
                int score = int.Parse(childSnapshot.Child("dates").Child(date).Child("score").Value.ToString());
                int steps = int.Parse(childSnapshot.Child("dates").Child(date).Child("steps").Value.ToString());
                string username = "tester";
                int scoreToday = score;

                for (int i = 1; i <= 7; i++)
                {
                    date_secondH = (day - i).ToString();
                    Debug.Log("date second part after: " + date_secondH);
                    date = String.Concat(date_firstH, date_secondH);
                    Debug.Log("date: " + date);
                    StartCoroutine(LoadScoreData(date));
                    yield return new WaitForSeconds(1);
                    Debug.Log("score before: " + score);

                    score += giveMeScore;
                    Debug.Log("givemescore: " + giveMeScore);
                    Debug.Log("score after: " + score);

                }

                //Instantiate new scoreboard elements
                GameObject scoreboardElement = Instantiate(scoreElement, scoreboardContent);
                scoreboardElement.GetComponent<ScoreElement>().NewScoreElement(username, steps, score, scoreToday);
            }

            //Go to scoareboard screen
           // UIScreenManager.instance.ScoreboardScreen();
        }
    }
}

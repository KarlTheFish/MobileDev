using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;

public class AuthManager : MonoBehaviour
{
    //Class ref and Firebase variables

    public MainMenu menu;
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;
    public DatabaseReference DBreference;

    //Login & Register variables

    public InputField emailLoginField;
    public InputField passwordField;
    public Text warningLoginText;
    public Text confirmLoginText;

    public InputField usernameRegisterField;
    public InputField passwordRegisterField;
    public InputField emailRegisterField;
    public Text warningRegisterText;

    //User Data variables

    public GameObject scoreElement;
    public Transform scoreboardContent;
    public InputField usernameField;
    public InputField scoreField;


    //Funktsioonide välja kutsumine
    private void Awake()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();
            }
        
        else
        {
            Debug.LogError("Could not resolve all Firebase dependecies: " + dependencyStatus);
        }
    });

    }

    public void ClearLoginFields()
    {
        emailLoginField.text = "";
        passwordField.text = ""; 
    }

    public void ClearRegisterField()
    {
        usernameRegisterField.text = "";
        emailRegisterField.text = "";
        passwordRegisterField.text = "";
    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");

        auth = FirebaseAuth.DefaultInstance;

        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
    }
    public void LoginButton()
    {
        StartCoroutine(Login(emailLoginField.text, passwordField.text));
    }
    public void RegisterButton()
    {
        StartCoroutine(Register(emailRegisterField.text, passwordRegisterField.text, usernameRegisterField.text));
    }

    public void SignOutButton()
    {
        auth.SignOut();
        ClearLoginFields();
        ClearRegisterField();
        menu.loginUI.SetActive(true);
        menu.loginCanvasUI.SetActive(true);
        menu.mainMenuUI.SetActive(false);

    }

    public void SaveDataButton()
    {
        StartCoroutine(UpdateUsernameAuth(usernameField.text));
        StartCoroutine(UpdateUsernameDatabase(usernameField.text));

        StartCoroutine(UpdateScore(int.Parse(scoreField.text)));

    }

    //Sisse logimine parameetritega email ja pass
    private IEnumerator Login(string _email, string _password)
    {
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        //Ootame kuni valmis
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            //Kui midagi läheb valesti, siis saame sisseehitatud veateateid näidata
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEX = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEX.ErrorCode;

            string message = "Login Faild!";
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
            //Hunnik asju nagu paneelivahetused kui sisselogimine õnnestus + konsooli sõnum + kasutaja teadaanne 
            menu.loginUI.SetActive(false);
            menu.loginCanvasUI.SetActive(false);
            menu.mainMenuUI.SetActive(true);
            StartCoroutine(LoadUserData());
            User = LoginTask.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);
            warningLoginText.text = null;
            confirmLoginText.text = "Logged In";
            Debug.Log("Logged in");
            ClearLoginFields();
            ClearRegisterField();


        }

    }

    //Registreerimise coroutine, sisendiks email, pass ja username
    private IEnumerator Register(string _email, string _password, string _username)
    {
        if (_username == "")
        {
            warningRegisterText.text = "Missing Username";
        }
        else
        {
            //Kasutaja loomine parameetritega email ja pass
            var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);

            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);
            //Vea puhul feedback
            if(RegisterTask.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.InnerException.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Register Failed";
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
                User = RegisterTask.Result;

                if (User != null)
                {
                    //Registreerimine
                    UserProfile profile = new UserProfile { DisplayName = _username };

                    var ProfileTask = User.UpdateUserProfileAsync(profile);

                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {   
                        // kui midagi läks valesti
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        warningRegisterText.text = "Username Set Failed";
                    }
                    else
                    {
                        //Hunnik asju debugimiseks + stseeni vahetus
                        warningRegisterText.text = "Registration successful!";
                        usernameField.text = User.DisplayName;
                        yield return new WaitForSeconds(2);
                        menu.loginUI.SetActive(true);
                        menu.registerUI.SetActive(false);
                        ClearLoginFields();
                        ClearRegisterField();
                    }
                }
            }
        }
    }
    //Data uuendamine kliendist andmebaasi
    private IEnumerator UpdateUsernameAuth(string _username)
    {
        //Create new profile and set username
        UserProfile profile = new UserProfile { DisplayName = _username };
        //Calling Fbase function
        var ProfileTask = User.UpdateUserProfileAsync(profile);
        //Wait until completed
        yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

        if (ProfileTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
        }
        else
        {
            //Auth username is updated
        }
    }

    
    private IEnumerator UpdateUsernameDatabase(string _username)
    {
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("username").SetValueAsync(_username);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Auth username is updated
        }
    }
    
    private IEnumerator UpdateScore(int _score)
    {
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("score").SetValueAsync(_score);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if(DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //score is now updated
        }
    }
    //Data laadimine andmebaasist
    private IEnumerator LoadUserData()
    {
        var DBTask = DBreference.Child("users").Child(User.UserId).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if(DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else if(DBTask.Result.Value == null)
        {
            //Pole andmeid
            scoreField.text = "0";
        }
        else
        {
            DataSnapshot snapshot = DBTask.Result;

            scoreField.text = snapshot.Child("score").Value.ToString();
        }
    }
}

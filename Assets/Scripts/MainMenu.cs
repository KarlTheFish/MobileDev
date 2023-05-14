using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject loginUI;
    public GameObject registerUI;
    public GameObject loginCanvasUI;
    // Start is called before the first frame update

    //Erinevad funktsioonid stseeni ja paneelide vaheetamiseks
   public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void LoginScreen()
    {
        loginUI.SetActive(true);
        registerUI.SetActive(false);
    }
    public void RegisterScreen()
    {
        loginUI.SetActive(false);
        registerUI.SetActive(true);
    }
    public void MainMenuScreen()
    {
        loginCanvasUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }
}

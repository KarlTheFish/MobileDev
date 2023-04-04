using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public CanvasGroup SettingsPanel;

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Settings()
    {
        SettingsPanel.alpha = 1;
        SettingsPanel.blocksRaycasts = true;
    }

    public void Back()
    {
        SettingsPanel.alpha = 0;
        SettingsPanel.blocksRaycasts = false;
    }

    public void Quit()
    {
        Application.Quit();
    }
}

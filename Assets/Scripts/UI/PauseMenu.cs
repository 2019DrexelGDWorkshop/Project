using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    #region Variables

    [SerializeField, Tooltip("UI for entire pause menu")]
    GameObject pauseMenu;

    [SerializeField, Tooltip("UI for main pause menu with all the buttons")]
    GameObject pauseMain;

    [SerializeField, Tooltip("UI for settings menu that shows up after clicking settings")]
    GameObject settings;

    #endregion Variables

    private void Update()
    {
        if (PauseManager.Instance.Paused)
            ShowPauseMenu();
        else
            HidePauseMenu();

    }

    private void OnDisable()
    {
        PauseManager.Instance.OnPause -= ShowPauseMenu;
        PauseManager.Instance.OnResume -= HidePauseMenu;
    }

    public void ShowPauseMenu()
    {
        pauseMenu.SetActive(true);
        pauseMain.SetActive(true);
        settings.SetActive(false);
    }

    public void HidePauseMenu()
    {
        pauseMenu.SetActive(false);
    }

    public void ResumeButton()
    {
        PauseManager.Instance.TogglePause();
    }
    public void RestartButton()
    {
        PauseManager.Instance.TogglePause();
        pauseMenu.SetActive(false);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void QuitButton()
    {
        Application.Quit();
        Debug.Log("Quit the Game");
    }

    public void MainMenuButton()
    {
        PauseManager.Instance.TogglePause();
        pauseMenu.SetActive(false);
        SceneManager.LoadScene(1);
    }
    public void SettingsButton()
    {
        pauseMain.SetActive(false);
        settings.SetActive(true);
    }
    public void SettingsBackButton()
    {
        pauseMain.SetActive(true);
        settings.SetActive(false);
    }
    public void SurveyButton()
    {
        Application.OpenURL("https://forms.gle/qxjZcJfFgwTVSzLGA");
        Debug.Log("Menu.cs: Open Link");
    }
}

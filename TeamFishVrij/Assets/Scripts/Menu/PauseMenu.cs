using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    PlayerInputActions _menuNavigation;
    public Button _primaryButton;

    public static bool _gameIsPaused = false;
    //private bool _buttonPressed = false;

    public GameObject _pauseMenuUI;

    private void Awake()
    {
        _menuNavigation = new PlayerInputActions();
    }

    public void Resume()
    {
        _pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        _gameIsPaused = false;
    }

    void Pause()
    {
        _pauseMenuUI.SetActive(true);
        _primaryButton.Select();
        Time.timeScale = 0f;
        _gameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quiting game...");
    }

    void OnPause()
    {
        if (_gameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }
}

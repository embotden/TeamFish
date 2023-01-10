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

    public IEnumerator Pause()
    {
        _pauseMenuUI.SetActive(true);

        yield return new WaitForSeconds(.5f);

        _primaryButton.Select();
        Time.timeScale = 0f;
        _gameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuV2");
    }

    public void QuitGame()
    {
        Debug.Log("Quiting game...");
        Application.Quit();
    }

    void OnPause()
    {
        if (_gameIsPaused)
        {
            Resume();
        }
        else
        {
            StartCoroutine(Pause());
        }
    }
}

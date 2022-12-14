using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator _startButton;
    public Animator _quitButton;

    public Animator _levelLoader;

    public GameObject _optionsMenu;
    public GameObject _mainMenu;

    public void PlayGame()
    {
        StartCoroutine(StartTheGame());
        //_startButton.SetBool("startPressed", true);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        StartCoroutine(QuitTheGame());
    }

    public void OptionsScreen()
    {
        _optionsMenu.SetActive(true);
        _mainMenu.SetActive(false);
        
    }

    private IEnumerator QuitTheGame()
    {
        yield return new WaitForSeconds(0.5f);

        _quitButton.SetBool("quitPressed", true);

        yield return new WaitForSeconds(2f);

        _levelLoader.SetTrigger("Start");

        yield return new WaitForSeconds(1.5f);

        Application.Quit();
    }

    private IEnumerator StartTheGame()
    {
        yield return new WaitForSeconds(0.5f);

        _startButton.SetBool("startPressed", true);

        yield return new WaitForSeconds(2f);

        _levelLoader.SetTrigger("Start");

        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

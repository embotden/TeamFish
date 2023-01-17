using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Menu Screens")]
    public GameObject _optionsMenu;
    public GameObject _mainMenu;
    public GameObject _credits;

    [Header("VFX")]
    [SerializeField] private GameObject _startEffect;
    [SerializeField] private Animator _crossfade;
    public Animator _vignetteStart;

    [Header("Level Loader")]
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private Slider _loadingBar;

    [Header("Camera")]
    [SerializeField] private Animator _cameraTransitions;

    [Header("Conditions")]
    private float _setup = 2.5f;
    [SerializeField] private bool _mainCamera;

    [Header("Start setup")]
    public Button _mainMenuPrimaryButton;
    public Slider _settingsMenuPrimaryButton;

    PlayerInputActions _menuNavigation;



    private void Start()
    {
        _startEffect.SetActive(false);

        _optionsMenu.SetActive(false);
        _credits.SetActive(false);
        _loadingScreen.SetActive(false);

        _mainMenuPrimaryButton.Select();

        _menuNavigation = new PlayerInputActions();
    }

    public void PlayGame()
    {
        StartCoroutine(StartTheGame(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void QuitGame()
    {
        StartCoroutine(QuitTheGame());
    }

    public void OptionsButton()
    {
        StartCoroutine(OptionsScreen());
    }

    public void ReturnToMainMenu()
    {
        StartCoroutine(BackToMain());
    }

    public IEnumerator OptionsScreen()
    {
        _mainMenu.SetActive(false);

        _cameraTransitions.Play("Exposition camera");

        yield return new WaitForSeconds(_setup);

        _optionsMenu.SetActive(true);

        _settingsMenuPrimaryButton.Select();


        _mainCamera = !_mainCamera;
    }

    private IEnumerator BackToMain()
    {
        _optionsMenu.SetActive(false);
        _credits.SetActive(false);

        _cameraTransitions.Play("Window camera");

        yield return new WaitForSeconds(_setup);

        _mainMenu.SetActive(true);

        _mainMenuPrimaryButton.Select();

        _mainCamera = !_mainCamera;

    }

    private IEnumerator QuitTheGame()
    {
        yield return new WaitForSeconds(0.5f);

        //_quitButton.SetBool("quitPressed", true);

        yield return new WaitForSeconds(2f);

        _crossfade.SetTrigger("Start");

        yield return new WaitForSeconds(1.5f);

        Application.Quit();
    }


    private IEnumerator StartTheGame(int levelIndex)
    {
        //yield return new WaitForSeconds(0.5f);

        //_startButton.SetBool("startPressed", true);

        yield return new WaitForSeconds(0.5f);

        _mainMenu.SetActive(false);

        //yield return new WaitForSeconds(0.5f);

        _vignetteStart.Play("AN_MM_Start");
        _startEffect.SetActive(true);

        yield return new WaitForSeconds(2f);

        _crossfade.SetTrigger("Start");

        yield return new WaitForSeconds(4f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);

        _loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            _loadingBar.value = progress;

            yield return null;
        }
    }

    void OnCancel()
    {
        if (_optionsMenu) StartCoroutine(BackToMain());
    }
}

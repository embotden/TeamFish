using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MainMenuTriggerCheck : MonoBehaviour
{
    PlayerInputActions _inputSystemCheck;

    public MainMenuNavigator _menuManager;
    public GameObject _sectionHeader;

    [Header("Eye animations")]
    public Animator _eyeAnimation;
    //public Animator _windowView;
    //public Animator _pictures;
    //public Animator _startGame;


    private bool _canChoose;

    private void Start()
    {
        _sectionHeader.SetActive(false);

        _eyeAnimation.SetBool("inRange", false);

        _inputSystemCheck = new PlayerInputActions();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fhinn"))
        {
            _sectionHeader.SetActive(true);

            _eyeAnimation.SetBool("inRange", true);

            _menuManager._visualCue.SetActive(true);

            _canChoose = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Fhinn"))
        {
            _eyeAnimation.SetBool("inRange", false);
            _menuManager._visualCue.SetActive(false);

            _canChoose = false;
        }
    }

    private void findState()
    {
        _canChoose = false;

        if (gameObject.CompareTag("Options"))
        {
            _menuManager.StartCoroutine(_menuManager.OptionsState());
        }
        else if (gameObject.CompareTag("Story"))
        {
            _menuManager.StoryViewState();
        }
        else if (gameObject.CompareTag("Collection"))
        {
            _menuManager.CollectionViewState();
        }
        else if (gameObject.CompareTag("Bath"))
        {
            _menuManager.StartCoroutine(_menuManager.StartGame(SceneManager.GetActiveScene().buildIndex + 1));
        }
    }

    void OnWaterGrab()
    {
        if (_canChoose) findState();
    }
}

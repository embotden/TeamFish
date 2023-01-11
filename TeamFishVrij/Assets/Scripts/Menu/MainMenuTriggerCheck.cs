using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MainMenuTriggerCheck : MonoBehaviour
{
    PlayerInputActions _inputSystemCheck;

    public MainMenuNavigator _menuManager;
    //public GameObject _sectionHeader;

    [Header("Eye animations")]
    //public Animator _eyeAnimation;
    //public Animator _windowView;
    //public Animator _pictures;
    //public Animator _startGame;


    private bool _canChoose;

    private void Start()
    {
        //_sectionHeader.SetActive(false);

        //_eyeAnimation.SetBool("inRange", false);

        _inputSystemCheck = new PlayerInputActions();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fhinn") && _menuManager._canShow)
        {
            //_sectionHeader.SetActive(true);

            //_eyeAnimation.SetBool("inRange", true);

            _menuManager._interactableUI.SetBool("canShow", true);

            _canChoose = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Fhinn"))
        {
            //_eyeAnimation.SetBool("inRange", false);
            _menuManager._interactableUI.Play("AN_UI_X_Disappear");
            _menuManager._interactableUI.SetBool("canShow", false);


            _canChoose = false;

            _menuManager._canShow = false;
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
            _menuManager.StartCoroutine(_menuManager.StoryViewState());
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

    void OnSelect()
    {
        if (_canChoose && !_menuManager._isWatching) findState();
    }
}

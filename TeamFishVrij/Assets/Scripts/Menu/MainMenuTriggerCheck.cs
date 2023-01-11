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

    [Header("Interaction Cue")]
    public Animator _xInteractionAnimation;

    //public Animator _eyeAnimation;
    //public Animator _windowView;
    //public Animator _pictures;
    //public Animator _startGame;


    private bool _canChoose;

    private void Start()
    {
        //_sectionHeader.SetActive(false);

        //_eyeAnimation.SetBool("inRange", false);

        _xInteractionAnimation.SetBool("canShow", false);

        _inputSystemCheck = new PlayerInputActions();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fhinn"))
        {
            //_sectionHeader.SetActive(true);

            //_eyeAnimation.SetBool("inRange", true);

            if(!_menuManager._isWatching) _xInteractionAnimation.SetBool("canShow", true);

            _canChoose = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Fhinn"))
        {
            //_eyeAnimation.SetBool("inRange", false);
            //_menuManager._interactableUI.Play("AN_UI_X_Disappear");
            
            _xInteractionAnimation.SetBool("canShow", false);

            _canChoose = false;

            _menuManager._canShow = false;
        }
    }

    private void findState()
    {
        _canChoose = false;
        _xInteractionAnimation.Play("AN_UI_X_Clicked");
        _xInteractionAnimation.SetBool("canShow", false);

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

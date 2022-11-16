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

    private bool _canChoose;

    private void Start()
    {
        _sectionHeader.SetActive(false);
        _inputSystemCheck = new PlayerInputActions();
    }

    private void Update()
    {
        /*if (_canChoose && Input.GetKeyDown(KeyCode.Q))
        {
            findState();
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fhinn"))
        {
            _sectionHeader.SetActive(true);
            _menuManager._visualCue.SetActive(true);

            _canChoose = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Fhinn"))
        {
            _sectionHeader.SetActive(false);
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

    void OnJump()
    {
        if (_canChoose) findState();
    }
}

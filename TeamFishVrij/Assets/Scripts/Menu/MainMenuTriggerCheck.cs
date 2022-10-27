using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuTriggerCheck : MonoBehaviour
{
    public MainMenuNavigator _menuManager;
    public GameObject _sectionHeader;

    private bool _canChoose;

    private void Start()
    {
        _sectionHeader.SetActive(false);
    }

    private void Update()
    {
        if (_canChoose && Input.GetKeyDown(KeyCode.Q))
        {
            findState();
        }
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
            _menuManager.StartGame();
        }
    }
}

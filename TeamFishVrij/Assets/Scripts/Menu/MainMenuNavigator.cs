using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuNavigator : MonoBehaviour
{
    [Header("Visual Cues")]
    public GameObject _visualCue;
    public GameObject _optionsMenu;
    public GameObject _startTrigger;
    public GameObject _startHeader;

    [Header("Transition")]
    public Animator _menuAnimator;
    public Animator _crossfadeTransition;
    [SerializeField] private float _setupTime = 2f;
    [SerializeField] private float _animationDuration = 0.5f;

    [Header("Conditions")]
    private bool _isWatching;
    //public bool _canExit;
    private bool _mainCamera;
    private bool _canStart;

    [Header("Options")]
    [SerializeField] private bool _isOptions;
    private bool _isCollection;
    private bool _isStory;
    private bool _isStart;
    private bool _reset;



    void Start()
    {
        _visualCue.SetActive(false);
        _optionsMenu.SetActive(false);
        _startTrigger.SetActive(false);
        _startHeader.SetActive(false);

        _isWatching = false;
        _reset = false;
        _canStart = false;
    }

    private void Update()
    {
        if(_isOptions || _isCollection || _isStory)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                StartCoroutine(SwitchToMainState());
            }
        }

        if (_canStart)
        {
            _startTrigger.SetActive(true);
            _startHeader.SetActive(true);
        }

        /*if(_reset)
        {
            _isStory = false;
            _isOptions = false;
            _isCollection = false;
        }*/

        if (_isWatching && Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("quit options button");
        }
    }
    

    public void StoryViewState()
    {
        _isStory = true;

        _menuAnimator.Play("Exposition camera");
        _mainCamera = !_mainCamera;

        _isWatching = true;
        _canStart = true;

        //_startHeader.SetActive(true);
    }

    public void CollectionViewState()
    {
        _isCollection = true;

        _menuAnimator.Play("Memories camera");
        _mainCamera = !_mainCamera;

        _isWatching = true;

    }

    public IEnumerator StartGame(int levelIndex)
    {
        Debug.Log("2");
        _isStart = true;
        _isWatching = true;
        _visualCue.SetActive(false);

        _menuAnimator.Play("Start camera");
        _mainCamera = !_mainCamera;
        yield return new WaitForSeconds(.5f);

        //_transition.Play("Crossfade_End");
        _crossfadeTransition.SetTrigger("Start");

        yield return new WaitForSeconds(3f);


        Debug.Log("3");

        SceneManager.LoadScene(levelIndex);
    }


    public IEnumerator OptionsState()
    {
        _isOptions = true;

        _menuAnimator.Play("Window camera");
        _mainCamera = !_mainCamera;


        yield return new WaitForSeconds(_setupTime);


        //Set options canvas active
        _optionsMenu.SetActive(true);


        _isWatching = true;

    }

    public IEnumerator SwitchToMainState()
    {
        _optionsMenu.SetActive(false);

        _menuAnimator.Play("Overview camera");

        yield return new WaitForSeconds(_setupTime);

        _isWatching = false;

        //_canExit = true;
        _reset = true;
        _mainCamera = !_mainCamera;

    }
}

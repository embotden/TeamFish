using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuNavigator : MonoBehaviour
{
    [Header("Visual Cues")]
    public GameObject _visualCue;

    [Header("Transition")]
    public Animator _menuAnimator;
    [SerializeField] private float _setupTime = 2f;

    [Header("Conditions")]
    private bool _isWatching;
    //public bool _canExit;
    private bool _mainCamera;

    [Header("Player")]
    public GameObject _Fhinn;

    [Header("Options")]
    [SerializeField] private bool _isOptions;
    private bool _isCollection;
    private bool _isStory;
    private bool _isStart;
    private bool _reset;



    void Start()
    {
        _visualCue.SetActive(false);
        _isWatching = false;
        _reset = false;
    }

    private void Update()
    {
        Debug.Log(_isWatching);

        if(_isOptions || _isCollection || _isStory)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                StartCoroutine(SwitchToMainState());
            }
        }

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
    }

    public void CollectionViewState()
    {
        _isCollection = true;

        _menuAnimator.Play("Memories camera");
        _mainCamera = !_mainCamera;

        _isWatching = true;

    }

    public void StartGame()
    {
        _isStart = true;

        _menuAnimator.Play("Start camera");
        _mainCamera = !_mainCamera;

        _isWatching = true;
    }


    public IEnumerator OptionsState()
    {
        _isOptions = true;

        _menuAnimator.Play("Window camera");
        _mainCamera = !_mainCamera;


        yield return new WaitForSeconds(_setupTime);


        //Set options canvas active

        _isWatching = true;

    }

    public IEnumerator SwitchToMainState()
    {
        Debug.Log("Switching back!");
        _menuAnimator.Play("Overview camera");

        yield return new WaitForSeconds(_setupTime);

        _isWatching = false;
        //_canExit = true;
        _reset = true;
        _mainCamera = !_mainCamera;

    }
}

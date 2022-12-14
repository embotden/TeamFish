using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MainMenuNavigator : MonoBehaviour
{
    PlayerInputActions _playerMenuControls;

    [Header("Visual Cues")]
    public GameObject _visualCue;
    public GameObject _optionsMenu;
    public GameObject _startTrigger;
    public GameObject _startHeader;
    //public Slider _optionsFirstButton;

    [Header("Transition")]
    public Animator _menuAnimator;
    public Animator _crossfadeTransition;
    [SerializeField] private float _setupTime = 2f;
    //[SerializeField] private float _animationDuration = 0.5f;

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
    //private bool _reset;

    [Header("Game Start")]
    public GameObject _loadingScreen;
    public Slider _slider;

    [Header("Dialogue")]
    [SerializeField] private TextAsset _windowDialogue;
    [SerializeField] private TextAsset _paintingDialogue;
    [SerializeField] private TextAsset _startDialogue;


    void Start()
    {
        _visualCue.SetActive(false);
        //_optionsMenu.SetActive(false);
        _startTrigger.SetActive(false);
        _startHeader.SetActive(false);

        _isWatching = false;
        //_reset = false;
        _canStart = false;

        _playerMenuControls = new PlayerInputActions();
    }

    private void Update()
    {
        if (_canStart)
        {
            _startTrigger.SetActive(true);
            //_startHeader.SetActive(true);
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
        _canStart = true;
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
        _isStart = true;
        _isWatching = true;
        _visualCue.SetActive(false);

        _menuAnimator.Play("Start camera");
        _mainCamera = !_mainCamera;
        yield return new WaitForSeconds(.5f);

        //start dialogue
        DialogueManager.GetInstance().EnterDialogueMode(_startDialogue);

        if (DialogueManager.GetInstance()._isDialogueFinished)
        {
            _crossfadeTransition.SetTrigger("Start");

            yield return new WaitForSeconds(1f);


            AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);

            _loadingScreen.SetActive(true);

            while(!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / .9f);

                _slider.value = progress;

                yield return null;
            }

        }

    }


    public IEnumerator OptionsState()
    {
        _isOptions = true;

        _menuAnimator.Play("Window camera");
        _mainCamera = !_mainCamera;

        yield return new WaitForSeconds(_setupTime);

        _isWatching = true;
    }

    public IEnumerator SwitchToMainState()
    {
        _optionsMenu.SetActive(false);

        _menuAnimator.Play("Overview camera");

        yield return new WaitForSeconds(_setupTime);

        _isWatching = false;

        _mainCamera = !_mainCamera;

        //start dialogue
        if (_isOptions)
        {
            DialogueManager.GetInstance().EnterDialogueMode(_windowDialogue);
            _isOptions = false;
        }
        else if(_isStory)
        {
            DialogueManager.GetInstance().EnterDialogueMode(_paintingDialogue);
            _isStory = false;
        }
    }

    void OnJump()
    {
        if (_isOptions || _isCollection || _isStory)
        {
                StartCoroutine(SwitchToMainState());
        }
    }
}

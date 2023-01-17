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
    //public GameObject _UI_InteractableOBJ;
    //public Animator _interactableUI;
    public GameObject _optionsMenu;
    public GameObject _startTrigger;
    //public Animator _startHeader;
    public Animator _lightCue;
    public GameObject _godRay;

    [Header("Transition")]
    public Animator _menuAnimator;
    public Animator _crossfadeTransition;
    [SerializeField] private float _setupTime = 2f;

    public Animator _windowVolume;

    [Header("Conditions")]
    public bool _isWatching;
    private bool _mainCamera;
    private bool _canStart;
    private bool _canSwitch;
    public bool _canShow;

    [Header("Options")]
    [SerializeField] private bool _isOptions;
    private bool _isCollection;
    private bool _isStory;

    [Header("Game Start")]
    public GameObject _loadingScreen;
    public Slider _slider;

    [Header("Dialogue")]
    [SerializeField] private TextAsset _openingDialogue;
    [SerializeField] private TextAsset _windowDialogue;
    [SerializeField] private TextAsset _paintingDialogue;
    [SerializeField] private TextAsset _startDialogue;

    [Header("Triggers")]
    public GameObject _windowViewTrigger;

    public GameObject _Fhinn;
    public PlayerController CanFhinnMove;


    void Start()
    {
        _startTrigger.SetActive(false);
        _windowViewTrigger.SetActive(false);
        _godRay.SetActive(false);

        _isWatching = false;
        _canStart = false;

        _playerMenuControls = new PlayerInputActions();

        _windowVolume.SetBool("viewActive", true);
        StartCoroutine(OpeningDialogue());
        //_Fhinn = GameObject.Find("/Characters/MC/MOD_Fhinn");

    }

    private void Update()
    {
        if (_canStart)
        {
            _startTrigger.SetActive(true);
        }
    }
    

    public IEnumerator OpeningDialogue()
    {
        //_canShow = false;

        yield return new WaitForSeconds(2f);

        //start dialogue
        DialogueManager.GetInstance().EnterDialogueMode(_openingDialogue);

        _isWatching = true;

        while (!DialogueManager.GetInstance()._isDialogueFinished)
        {
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        _menuAnimator.Play("Overview camera");
        _windowVolume.SetBool("viewActive", false);


        yield return new WaitForSeconds(.5f);

        _windowViewTrigger.SetActive(true);
        _mainCamera = !_mainCamera;
        _isWatching = false;

        //_UIX_Interactable.SetActive(true);

        //_canShow = true;
        //_UI_InteractableOBJ.SetActive(true);
    }

    public IEnumerator StoryViewState()
    {
        _isStory = true;
        //_canShow = false;

        _menuAnimator.Play("Exposition camera");
        _mainCamera = !_mainCamera;

        _isWatching = true;
        _canSwitch = false;

        yield return new WaitForSeconds(1.5f);

        DialogueManager.GetInstance().EnterDialogueMode(_paintingDialogue);

        while (!DialogueManager.GetInstance()._isDialogueFinished)
        {
            yield return null;
        }
        _canSwitch = true;

    }

    public void CollectionViewState()
    {
        _isCollection = true;
        //_canShow = false;

        _menuAnimator.Play("Memories camera");
        _mainCamera = !_mainCamera;

        _isWatching = true;

    }

    public IEnumerator StartGame(int levelIndex)
    {
        CanFhinnMove._canIMove = false;
        
        //_isStart = true;
        _isWatching = true;
        //_canShow = false;
        Animator _FhinnAnimator = _Fhinn.GetComponent<Animator>();

        _menuAnimator.Play("Start camera");
        _mainCamera = !_mainCamera;

        yield return new WaitForSeconds(.5f);

        //start dialogue
        DialogueManager.GetInstance().EnterDialogueMode(_startDialogue);

        while (DialogueManager.GetInstance()._isDialoguePlaying)
        {
            yield return null;
        }

        // Fhinn going to sleep
        yield return new WaitForSeconds(0.1f);
        _FhinnAnimator.SetBool("IsSleeping", true);


        yield return new WaitForSeconds(9.3f);
        
        //screen dip to black
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


    public IEnumerator OptionsState()
    {
        _isOptions = true;
        //_canShow = false;

        _menuAnimator.Play("Window camera");
        _windowVolume.SetBool("viewActive", true);
        _mainCamera = !_mainCamera;

        _canSwitch = false;

        yield return new WaitForSeconds(.5f);

        DialogueManager.GetInstance().EnterDialogueMode(_windowDialogue);

        while (!DialogueManager.GetInstance()._isDialogueFinished)
        {
            yield return null;
        }

        _canSwitch = true;

        _isWatching = true;
    }

    public IEnumerator SwitchToMainState()
    {
        _optionsMenu.SetActive(false);

        _menuAnimator.Play("Overview camera");
        _windowVolume.SetBool("viewActive", false);

        yield return new WaitForSeconds(_setupTime);

        _isWatching = false;

        _mainCamera = !_mainCamera;

        //_interactableUI.SetBool("canShow", true);
        //_canShow = true;

        if (_isOptions)
        {
            _isOptions = false;
        }
        else if(_isStory)
        {
            _isStory = false;
            if(!_canStart)
            {
                _lightCue.Play("AN_VOLLG_Appear");
                _godRay.SetActive(true);
                //_startHeader.SetTrigger("canStart");
                _canStart = true;
            }
        }

    }

    void OnBack()
    {
        if (_isOptions || _isCollection || _isStory)
        {
            if (_canSwitch)
            {
                StartCoroutine(SwitchToMainState());

            }
        }
    }
}

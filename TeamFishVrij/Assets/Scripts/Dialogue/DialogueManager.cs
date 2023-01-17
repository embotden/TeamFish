using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.UI;

//when i add choices and the animation isn't flowing: Unity typing text effect for dialogue | Unity tutorial 2021

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject _dialoguePanel;
    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private TextMeshProUGUI _displayNameTag;
    [SerializeField] private GameObject _continueIcon;
    [SerializeField] private GameObject _worldCanvas;

    [Header("Ink Unity Linkup")]
    private const string SPEAKER_TAG = "speaker";
    private const string LAYOUT_TAG = "layout";

    [Header("Conditions")]
    private Story _currentStory;
    private static DialogueManager _instance;
    public bool _isDialoguePlaying { get; private set; }
    public bool _dialogueGoingCheck = false;

    public Animator _dialogueBoxAnimations;
    [SerializeField] private Animator _layoutAnimator;
    [SerializeField] private Animator _FhinnNametag;
    [SerializeField] private Animator _SteevinNametag;
    [SerializeField] private Animator _ContinueButtonAnimations;
    [SerializeField] private PlayerController _fhinnAnimations;
    [SerializeField] private bool _buttonClicked;
    [SerializeField] private bool _justStarted = false;

    public bool _isDialogueFinished = false;
    //private bool _submitSkip;

    private GameObject _Fhinn;
    private GameObject _Steevin;

    public Animator _FhinnAnimator;
    public Animator _SteevinAnimator;


    private void Awake()
    {
        if(_instance != null)
        {
            Debug.LogWarning("Found more than one dialogue manager in scene!");
        }
        _instance = this;

        _Fhinn = GameObject.Find("/Characters/MC/MOD_Fhinn");
        _Steevin = GameObject.Find("/Characters/Shark/MOD_Steefin");

        
    }

    public static DialogueManager GetInstance()
    {
        return _instance;
    }

    private void Start()
    {
        _isDialoguePlaying = false;
        _dialoguePanel.SetActive(false);
        _continueIcon.SetActive(false);
        _buttonClicked = false;


        _dialogueText.text = "";
    }

    private void Update()
    {
        //return right away if dialogue isn't playing
        if (!_isDialoguePlaying)
        {
            return;
        }

        if(_buttonClicked)
        {
            _ContinueButtonAnimations.SetBool("isClicked", false);
            _buttonClicked = false;
        }

    }

    public void EnterDialogueMode(TextAsset _inkJSON)
    {
        //retreive Inky file
        _currentStory = new Story(_inkJSON.text);

        //Set UI Active
        _dialoguePanel.SetActive(true);
        _continueIcon.SetActive(true);

        //Conditions
        _justStarted = true;
        _isDialoguePlaying = true;
        _dialogueGoingCheck = true;
        _isDialogueFinished = false;

        //stop non dialogue UI from becoming visible
        _worldCanvas.SetActive(false);

        //UI animations
        _dialogueBoxAnimations.SetBool("canTalk", true);
        _ContinueButtonAnimations.SetBool("canStart", true);

        //reset dialogue layout
        _displayNameTag.text = "";
        //_layoutAnimator.Play("UI_Dialogue_LayoutFhinn");

        //Name tag animations
        _FhinnNametag.SetBool("canStart", true);
        _SteevinNametag.SetBool("canStart", true);
        _FhinnNametag.SetBool("canLeave", false);
        _SteevinNametag.SetBool("canLeave", false);

        ContinueStory();

    }

    private void ContinueStory()
    {
        //Animator _FhinnAnimator = _Fhinn.GetComponent<Animator>();
        //Animator _SteevinAnimator = _Steevin.GetComponent<Animator>();

        if (_currentStory.canContinue || _justStarted)
        {
            //_FhinnAnimator.SetBool("IsTalking", true);
            //_SteevinAnimator.SetBool("IsTalking", true);

            _dialogueText.text = _currentStory.Continue();

            //handle tags
            HandleTags(_currentStory.currentTags);

            if (_continueIcon.activeSelf) _ContinueButtonAnimations.Play("UI_Continue_Clicked");
            
            _buttonClicked = true;

            _justStarted = false;
        }
        else
        {
            StartCoroutine(ExitDialogueMode());

            //_FhinnAnimator.SetBool("IsTalking", false);
            //_SteevinAnimator.SetBool("IsTalking", false);
        }
        
    }

    private void HandleTags(List<string> currentTags)
    {
        

        //loop through each tag and handle it accordingly
        foreach (string tag in currentTags)
        {
            //Debug.Log("next step");

            //parse the tag
            string[] splitTag = tag.Split(':');
            if(splitTag.Length != 2)
            {
                Debug.LogError("Tag could not be appropriately parsed: " + tag);
            }
            
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            //handle the tag
            switch(tagKey)
            {
                case SPEAKER_TAG:
                    //Debug.Log("speaker=" + tagValue);
                    _displayNameTag.text = tagValue;
                    break;
                case LAYOUT_TAG:
                    //Debug.Log("layout=" + tagValue);
                    _layoutAnimator.Play(tagValue);
                    break;
                default:
                    Debug.LogWarning("Tag came in but is not currently being handled " + tag);
                    break;
            }
        }

        if (_displayNameTag.text == "Fhinn") 
        {
            _FhinnAnimator.SetBool("IsTalking", true);
        }

        else
        {
            _FhinnAnimator.SetBool("IsTalking", false);
        }


        if (_displayNameTag.text == "Steevin")
        {
            _SteevinAnimator.SetBool("IsTalking", true);
        }

        else
        {
            _SteevinAnimator.SetBool("IsTalking", false);
        }

    }

    private IEnumerator ExitDialogueMode()
    {
        _dialogueText.text = "";
        _displayNameTag.text = "";

        _isDialogueFinished = true;

        _dialogueBoxAnimations.SetBool("canTalk", false);
        _ContinueButtonAnimations.SetBool("canStart", false);

        //NAMETAG ANIMATION
        if (_FhinnNametag)
        {
            _FhinnNametag.SetBool("canStart", false);
        }
        else if(_SteevinNametag)
        {
            _SteevinNametag.SetBool("canStart", false);
        }

        if (_continueIcon.activeSelf)
        {
            _ContinueButtonAnimations.Play("UI_Continue_ClickedEnd");
        }

        yield return new WaitForSeconds(0.2f);

        _isDialoguePlaying = false;

        _dialogueBoxAnimations.SetBool("isFinished", true);

        //NAMETAG ANIMATION
        if (_FhinnNametag)
        {
            _FhinnNametag.SetBool("canLeave", true);
        }
        else if(_SteevinNametag)
        {
            _SteevinNametag.SetBool("canLeave", true);
        }

        if (_displayNameTag.text == "")
        {
            _SteevinAnimator.SetBool("IsTalking", false);
            _FhinnAnimator.SetBool("IsTalking", false);
        }

        yield return new WaitForSeconds(1f);

        _dialoguePanel.SetActive(false);
        _dialogueGoingCheck = false;
        _worldCanvas.SetActive(true);

    }

    void OnDialogue()
    {
        if(_isDialoguePlaying) ContinueStory();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using TMPro;
using Ink.Runtime;

//when i add choices and the animation isn't flowing: Unity typing text effect for dialogue | Unity tutorial 2021

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject _dialoguePanel;
    [SerializeField] private TextMeshProUGUI _dialogueText;

    [Header("Conditions")]
    private Story _currentStory;
    private static DialogueManager _instance;
    //public WaterAbility _waterUICheck;
    public bool _isDialoguePlaying { get; private set; }
    public bool _DialogueWaterCheck = false;

    public Animator _dialogueBoxAnimations;

    public bool _isDialogueFinished = false;
    private bool _submitSkip;


    private void Awake()
    {
        if(_instance != null)
        {
            Debug.LogWarning("Found more than one dialogue manager in scene!");
        }
        _instance = this;
    }

    public static DialogueManager GetInstance()
    {
        return _instance;
    }

    private void Start()
    {
        _isDialoguePlaying = false;
        _dialoguePanel.SetActive(false);
    }

    private void Update()
    {
        //return right away if dialogue isn't playing
        if (!_isDialoguePlaying)
        {
            return;
        }
    }

    public void EnterDialogueMode(TextAsset _inkJSON)
    {
        _currentStory = new Story(_inkJSON.text);
        _isDialoguePlaying = true;
        _dialoguePanel.SetActive(true);
        _DialogueWaterCheck = true;

        _dialogueBoxAnimations.SetBool("canTalk", true);

        ContinueStory();
    }

    private void ContinueStory()
    {
        if(_currentStory.canContinue)
        {
            Debug.Log("can continue!");
            _dialogueText.text = _currentStory.Continue();
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
            Debug.Log("exiting dialoguemode!");
        }
        
    }

    private IEnumerator ExitDialogueMode()
    {
        _dialogueText.text = "";

        _isDialogueFinished = true;

        _dialogueBoxAnimations.SetBool("canTalk", false);

        yield return new WaitForSeconds(0.2f);

        _isDialoguePlaying = false;

        _dialogueBoxAnimations.SetBool("isFinished", true);

        yield return new WaitForSeconds(1f);

        _dialoguePanel.SetActive(false);

        _DialogueWaterCheck = false;

    }

    void OnDialogue()
    {
        _submitSkip = true;

        //handle continuing to the next line in the dialogue when submit is pressed
        if (_currentStory.canContinue)
        {
            ContinueStory();
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
            Debug.Log("exiting dialoguemode!");
        }

    }

    /*PlayerInputActions _inputButtons;

    [Header("Params")]
    private float _typingSpeed = 0.005f/10f;
    

    [Header("Dialogue UI")]

    [SerializeField] private GameObject _dialoguePanel;

    [SerializeField] private TextMeshProUGUI _dialogueText;

    [SerializeField] private GameObject _continueIcon;

    public Animator _dialogueBoxAnimations;


    [Header("Conditions")]

    private static DialogueManager _instance;

    public bool _isDialoguePlaying { get; private set; }

    private Story _currentStory;

    public bool _isDialogueFinished = false;

    private bool _canSkip = false;

    private bool _submitSkip;


    [Header("Flowing text effect")]

    private Coroutine _displayLineCoroutine;

    private bool _canContinueToNextLine = false;



    private void Awake()
    {
        _inputButtons = new PlayerInputActions();

        if (_instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        _instance = this;
    }

    public static DialogueManager GetInstance()
    {
        return _instance;
    }

    private void Start()
    {
        _isDialoguePlaying = false;
        _dialoguePanel.SetActive(false);
        _dialogueBoxAnimations.SetBool("canTalk", false);
    }

    private void Update()
    {
        //return right away if dialogue isn't playing
        if (!_isDialoguePlaying)
        {
            return;
        }
    }

    public void EnterDialogueMode(TextAsset _inkJSON)
    {
        _currentStory = new Story(_inkJSON.text);
        _isDialoguePlaying = true;
        _dialoguePanel.SetActive(true);

        _dialogueBoxAnimations.SetBool("canTalk", true);

        ContinueStory();
    }

    private IEnumerator ExitDialogueMode()
    {
        _isDialogueFinished = true;

        _dialogueBoxAnimations.SetBool("canTalk", false);

        yield return new WaitForSeconds(0.2f);

        _isDialoguePlaying = false;

        _dialogueBoxAnimations.SetBool("isFinished", true);

        yield return new WaitForSeconds(1f);

        _dialoguePanel.SetActive(false);

        _dialogueText.text = "";
    }

    private void ContinueStory()
    {
        if (_currentStory.canContinue)
        {
            //set text for current dialogue line
            if (_displayLineCoroutine != null)
            {
                StopCoroutine(_displayLineCoroutine);
            }

            _displayLineCoroutine = StartCoroutine(DisplayLine(_currentStory.Continue()));

            //display choices, if any, for this dialogue line

            //handle tags

        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    private IEnumerator DisplayLine(string line)
    {
        //empty the dialogue text
        _dialogueText.text = "";

        //hide items while text is typing
        _continueIcon.SetActive(false);

        _submitSkip = false;
        _canContinueToNextLine = false;

        StartCoroutine(CanSkip());

        //display each letter one at a time
        foreach (char letter in line.ToCharArray())
        {
            //if the submit button is pressed, finish up displaying the line right away
            if(_canSkip && Input.GetButtonDown("Jump"))
            {
                _submitSkip = false;
                _dialogueText.text = line;
                break;
            }

            _dialogueText.text += letter;
            yield return new WaitForSeconds(_typingSpeed);
        }

        //actions to take after the entire line has finished displaying
        _continueIcon.SetActive(true);

        _canContinueToNextLine = true;

        _canSkip = false;
    }

    void OnDialogue()
    {
        _submitSkip = true;

        //handle continuing to the next line in the dialogue when submit is pressed
        if(_canContinueToNextLine)
        {
            ContinueStory();
        }

    }

    //new test

    //time for typing effect to check if the player kan skip
    private IEnumerator CanSkip()
    {
        _canSkip = false;

        yield return new WaitForSeconds(0.05f);

        _canSkip = true;
    }*/
}

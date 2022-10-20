using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;

//when i add choices and the animation isn't flowing: Unity typing text effect for dialogue | Unity tutorial 2021

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]

    [SerializeField] private GameObject _dialoguePanel;

    [SerializeField] private TextMeshProUGUI _dialogueText;

    [SerializeField] private GameObject _continueIcon;


    [Header("Conditions")]

    [SerializeField] private float _typingSpeed = 0.04f;

    private static DialogueManager _instance;

    public bool _isDialoguePlaying { get; private set; }

    private Story _currentStory;

    public bool _isDialogueFinished = false;


    [Header("Flowing text effect")]

    private Coroutine _displayLineCoroutine;

    private bool _canContinueToNextLine = false;





    private void Awake()
    {
        if(_instance != null)
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
    }

    private void Update()
    {

        //return right away if dialogue isn't playing
        if(!_isDialoguePlaying)
        {
            return;
        }

        //hanndle continuing to the next line in the dialogue when submit is pressed
        if(_canContinueToNextLine && Input.GetKeyDown(KeyCode.B))
        {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset _inkJSON)
    {
        _currentStory = new Story(_inkJSON.text);
        _isDialoguePlaying = true;
        _dialoguePanel.SetActive(true);


        ContinueStory();
    }

    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);

        _isDialoguePlaying = false;
        _isDialogueFinished = true;
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

        _canContinueToNextLine = false;

        //display each letter one at a time
        foreach (char letter in line.ToCharArray())
        {
            //if the submit button is pressed, finish up displaying the line right away
            if(Input.GetKeyDown(KeyCode.B))
            {
                _dialogueText.text = line;
                break;
            }

            _dialogueText.text += letter;
            yield return new WaitForSeconds(_typingSpeed);
        }

        //actions to take after the entire line has finished displaying
        _continueIcon.SetActive(true);

        _canContinueToNextLine = true;
    }
}

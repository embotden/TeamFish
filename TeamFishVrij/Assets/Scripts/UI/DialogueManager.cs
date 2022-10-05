using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]

    [SerializeField] private GameObject _dialoguePanel;

    [SerializeField] private TextMeshProUGUI _dialogueText;

    private static DialogueManager _instance;

    private Story _currentStory;

    public bool _isDialoguePlaying { get; private set; }


    private void Awake()
    {
        if(_instance != null)
        {
            Debug.LogWarning("Found ore than one Dialogue Manager in the scene");
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
        if(Input.GetKeyDown(KeyCode.Space))
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
        _dialoguePanel.SetActive(false);
        _dialogueText.text = "";
    }

    private void ContinueStory()
    {
        if (_currentStory.canContinue)
        {
            _dialogueText.text = _currentStory.Continue();
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }
}

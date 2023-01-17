using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    //[SerializeField] private GameObject _visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset _inkJSON;

    private bool _isPlayerInRange;

    private void Awake()
    {
        _isPlayerInRange = false;
        //_visualCue.SetActive(false);
    }

    private void Update()
    {
        if (_isPlayerInRange && !DialogueManager.GetInstance()._isDialoguePlaying) StartCoroutine(DialoguePlaying());

        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Fhinn")
        {
            _isPlayerInRange = true;
        }
    }

    private IEnumerator DialoguePlaying()
    {
        yield return new WaitForSeconds(1.5f);

        DialogueManager.GetInstance().EnterDialogueMode(_inkJSON);

        while (!DialogueManager.GetInstance()._isDialogueFinished)
        {
            yield return null;
        }

        gameObject.SetActive(false);
    }
}

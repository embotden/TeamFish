using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneTrigger : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset _inkJSON;

    private bool _isPlayerInRange;

    private void Awake()
    {
        _isPlayerInRange = true;
    }

    private void Update()
    {
        if(_isPlayerInRange && !DialogueManager.GetInstance()._isDialoguePlaying) DialogueManager.GetInstance().EnterDialogueMode(_inkJSON);

        if (DialogueManager.GetInstance()._isDialogueFinished) gameObject.SetActive(false);
    }

}

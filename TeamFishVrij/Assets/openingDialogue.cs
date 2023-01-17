using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openingDialogue : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset _inkJSON;
    public PlayerController _Fhinnsmovement1;

    private bool _isPlayerInRange;

    private void Start()
    {
        StartCoroutine(OpeningCutscene());
    }
    private void Awake()
    {
        _isPlayerInRange = true;
    }

    public IEnumerator OpeningCutscene()
    {
        _Fhinnsmovement1._canIMove = false;

        //crossfade timing
        yield return new WaitForSeconds(2f);

        //waking up animation


        //yield return voor timing wanneer dialoog mag starten


        //start dialogue
        DialogueManager.GetInstance().EnterDialogueMode(_inkJSON);


        while (!DialogueManager.GetInstance()._isDialogueFinished)
        {
            yield return null;
        }

        gameObject.SetActive(false);
        _Fhinnsmovement1._canIMove = true;
    }

}

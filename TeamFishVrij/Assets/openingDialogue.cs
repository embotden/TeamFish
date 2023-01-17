using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openingDialogue : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset _inkJSON;
    public PlayerController _Fhinnsmovement1;
    private GameObject _Fhinn;

    private bool _isPlayerInRange;

    private void Start()
    {
        StartCoroutine(OpeningCutscene());
    }
    private void Awake()
    {
        _isPlayerInRange = true;
        _Fhinn = GameObject.Find("/Characters/MC/MOD_Fhinn");
    }

    public IEnumerator OpeningCutscene()
    {
        _Fhinnsmovement1._canIMove = false;
        Animator _FhinnAnimator = _Fhinn.GetComponent<Animator>();

        //waking up animation
        _FhinnAnimator.SetBool("IsWakingUp", true);

        yield return new WaitForSeconds(1f);

        _FhinnAnimator.SetBool("IsWakingUp", false);


        //yield return voor timing wanneer dialoog mag starten
        yield return new WaitForSeconds(15f);

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

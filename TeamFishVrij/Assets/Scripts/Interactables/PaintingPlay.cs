using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingPlay : MonoBehaviour
{
    public Animator _paintingAnimation;
    public GameObject _plantTouchBorder;
    public GameObject _uiCue;

    public float _timeToStop;

    [Header("Inky")]
    [SerializeField] private TextAsset _inkJSON1;
    [SerializeField] private TextAsset _inkJSON2;

    public Animator _lightAnimations;

    public bool isCutscenePlaying;

    public PlayerController playerInteractions;


    private void Start()
    {
        _plantTouchBorder.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Fhinn"))
        {
            StartCoroutine(ThisCutscene());
        }
    }

    private IEnumerator ThisCutscene()
    {
        playerInteractions._canIMove = false;

        yield return new WaitForSeconds(2f);

        DialogueManager.GetInstance().EnterDialogueMode(_inkJSON1);

        while (!DialogueManager.GetInstance()._isDialogueFinished)
        {
            yield return null;
        }
        
        _uiCue.SetActive(false);

        _lightAnimations.SetTrigger("canStart");

        yield return new WaitForSeconds(3f);

        _paintingAnimation.SetTrigger("canPlay");

        yield return new WaitForSeconds(_timeToStop);

        Destroy(_paintingAnimation);

        _lightAnimations.SetTrigger("canMove");
        
        yield return new WaitForSeconds(1.5f);

        _uiCue.SetActive(true);
        

        DialogueManager.GetInstance().EnterDialogueMode(_inkJSON2);

        while (!DialogueManager.GetInstance()._isDialogueFinished)
        {
            yield return null;
        }

        _plantTouchBorder.SetActive(true);
        
        playerInteractions._canIMove = true;
        //_playerMovements._canKeepMoving = true;

        Destroy(gameObject);
    }
}

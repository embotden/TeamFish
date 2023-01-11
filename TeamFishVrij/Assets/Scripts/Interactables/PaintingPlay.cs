using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingPlay : MonoBehaviour
{
    //public GameObject _videoPlayer;
    //public GameObject _renderTexture;

    public Animator _paintingAnimation;
    public GameObject _plantTouchBorder;
    public WaterAbility _uiCue;

    public float _timeToStop;

    [Header("Inky")]
    [SerializeField] private TextAsset _inkJSON1;
    [SerializeField] private TextAsset _inkJSON2;

    public Animator _lightAnimations;

    private bool _isReadyforText;

    private void Start()
    {
        _isReadyforText = false;

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
        _uiCue._visualCue.SetActive(false);

        yield return new WaitForSeconds(2f);

        DialogueManager.GetInstance().EnterDialogueMode(_inkJSON1);

        while (!DialogueManager.GetInstance()._isDialogueFinished)
        {
            yield return null;
        }

        _lightAnimations.SetTrigger("canStart");

        yield return new WaitForSeconds(3.5f);

        _paintingAnimation.SetTrigger("canPlay");

        yield return new WaitForSeconds(_timeToStop);

        Destroy(_paintingAnimation);

        _lightAnimations.SetTrigger("canMove");
        
        yield return new WaitForSeconds(2.5f);

        DialogueManager.GetInstance().EnterDialogueMode(_inkJSON2);

        while (!DialogueManager.GetInstance()._isDialogueFinished)
        {
            yield return null;
        }

        _plantTouchBorder.SetActive(true);
        _uiCue._visualCue.SetActive(true);

        Destroy(gameObject);
    }
}

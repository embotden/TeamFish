using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingPlay : MonoBehaviour
{
    public GameObject _videoPlayer;
    public GameObject _renderTexture;

    public float _timeToStop;

    [Header("Inky")]
    [SerializeField] private TextAsset _inkJSON;

    private bool _isReadyforText;

    private void Start()
    {
        _videoPlayer.SetActive(false);
        _renderTexture.SetActive(false);
        _isReadyforText = false;
    }

    private void Update()
    {
        if (_isReadyforText && !DialogueManager.GetInstance()._isDialoguePlaying) DialogueManager.GetInstance().EnterDialogueMode(_inkJSON);

        if (_isReadyforText && DialogueManager.GetInstance()._isDialogueFinished) gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Fhinn"))
        {
            StartCoroutine(VideoPlaying());
            //_videoPlayer.SetActive(true);
            //Destroy(_videoPlayer, _timeToStop);

            Debug.Log("a");
        }
    }

    private IEnumerator VideoPlaying()
    {
        _videoPlayer.SetActive(true);
        _renderTexture.SetActive(true);

        Debug.Log("b");

        yield return new WaitForSeconds(_timeToStop);

        Destroy(_videoPlayer);
        Destroy(_renderTexture);

        //yield return new WaitForEndOfFrame();

        Debug.Log("c");

        _isReadyforText = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject _visualCue;

    [Header("image")]
    [SerializeField] private GameObject _painting;
    [SerializeField] private bool _isPlayerInRange;
    [SerializeField] private bool _waitingTimeOver;
    [SerializeField] private float _waitingTime;

    [Header("Trigger")]
    [SerializeField] private GameObject _trigger;

    private void Awake()
    {
        _visualCue.SetActive(false);
    }

    private void Update()
    {
        if (_isPlayerInRange) StartCoroutine(StoryPainting());

        if (_waitingTimeOver && Input.GetKeyDown(KeyCode.B)) CloseImage();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Fhinn")
        {
            Debug.Log("Incoming!");
            _isPlayerInRange = true;
        }
    }

    private IEnumerator StoryPainting()
    {
        //Play image animation
        _painting.SetActive(true);

        //Wait for set amount of time
        yield return new WaitForSeconds(_waitingTime);

        //Show continue button
        _visualCue.SetActive(true);

        _waitingTimeOver = true;
    }

    private void CloseImage()
    {
        _visualCue.SetActive(false);
        _painting.SetActive(false);
        _trigger.SetActive(false);

    }
}

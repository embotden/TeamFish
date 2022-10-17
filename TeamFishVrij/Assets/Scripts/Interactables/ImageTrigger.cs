using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject _visualCue;

    [Header("image")]
    [SerializeField] private GameObject _painting;
    [SerializeField] private bool _waitingTimeOver;
    [SerializeField] private float _waitingTime;

    public bool _isImageClosed = false;
    

    private void Awake()
    {
        _visualCue.SetActive(false);
    }

    private void Update()
    {
        if (_waitingTimeOver && Input.GetKeyDown(KeyCode.B)) CloseImage();
    }

    public IEnumerator StoryPainting()
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
        gameObject.SetActive(false);
        /*_visualCue.SetActive(false);
        _painting.SetActive(false);
        _isImageClosed = true;*/

    }
}

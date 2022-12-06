using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    [Header("Plant state")]
    public float _damage;
    public float _revival;
    [SerializeField] private float _planteState;
    [SerializeField] private float _maxHealth = 10f;

    [Header("Succes feedback")]
    [SerializeField] private float _waitingTime;
    //private ImageTrigger _imageInteraction;
    public GameObject _succesFeedback;

    [SerializeField] private bool _succesFeedbackTriggered = false;

    [Header("Animations")]
    public Animator _hangingPlant;


    private void Start()
    {
        _succesFeedback.SetActive(false);

    }
    private void Update()
    {
        if (_planteState >= _maxHealth && !_succesFeedbackTriggered) Invoke("PlantMaxedOut", _waitingTime);
    }

    public void PlantGrowing()
    {
        _planteState += _revival;
    }

    private void PlantMaxedOut()
    {
        _succesFeedback.SetActive(true);

        _hangingPlant.SetBool("isHit", true);
        //_imageInteraction = _succesFeedback.GetComponent<ImageTrigger>();
       // _imageInteraction.StartCoroutine(_imageInteraction.StoryPainting());

        _succesFeedbackTriggered = true;
    }

    public void PlantShrinking()
    {
        _planteState -= _damage;
    }


}

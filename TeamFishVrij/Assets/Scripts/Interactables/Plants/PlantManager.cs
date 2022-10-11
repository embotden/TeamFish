using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    public float _damage;
    public float _revival;
    [SerializeField] private float _planteState;
    [SerializeField] private float _maxHealth = 10f;

    public GameObject _triggerEvent;
    public GameObject _continueButton;
    [SerializeField] private float _waitingTime;


    private void Update()
    {
        if (_planteState >= _maxHealth) Invoke("PlantMaxedOut", _waitingTime);
    }

    public void PlantGrowing()
    {
        _planteState += _revival;
    }

    private void PlantMaxedOut()
    {
        _triggerEvent.SetActive(true);
    }

    public void PlantShrinking()
    {
        _planteState -= _damage;
    }


}

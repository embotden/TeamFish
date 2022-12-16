using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leverInteraction : MonoBehaviour
{
    //checks if player is holding the lever and what way they are pushing

    [SerializeField] private GameObject _visualCue;
    //public GameObject _platform;

    [SerializeField] private bool _canSwitch;
    public bool _isHolding;

    PlayerInputActions _playerInteractions;

    public Platforminteraction _singlePlatform;

    private void Start()
    {
        _canSwitch = false;
        _visualCue.SetActive(false);
        _playerInteractions = new PlayerInputActions();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Fhinn")
        {
            _visualCue.SetActive(true);
            _canSwitch = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Fhinn")
        {
            _visualCue.SetActive(false);
            _canSwitch = false;
        }
    }

    public void OnWaterGrab()
    {
        if(_canSwitch)
        {
            _singlePlatform._isActive = true;

            //animation of character grabbing the lever

            //camera perspective changes to overview
        }

        if(_singlePlatform._isActive)
        { 

            _singlePlatform._isActive = false;

            //animation of character letting go of lever

            //camera perspective changes to closeup
        }
    }
}

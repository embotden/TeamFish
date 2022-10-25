using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CameraTriggerVolume : MonoBehaviour
{
    public Animator _animator;

    private bool _playerCamera = true;


    private void Awake()
    {
        //_animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fhinn")) SwitchState();
    }

    private void OnTriggerExit(Collider other)
    {
        SwitchToMainState();
        //if (other.CompareTag("Fhinn")) SwitchState();

        Debug.Log("Switching back");
    }

    private void SwitchState()
    {
        /*if (_playerCamera)
        {
            _animator.Play("Lookout camera");
        }
        else
        {
            _animator.Play("Player camera");
        }*/
        if (gameObject.CompareTag("LookOut"))
        {
            _animator.Play("Lookout camera");
        }
        else if (gameObject.CompareTag("Door"))
        {
            _animator.Play("Door camera");
        }
        else if (gameObject.CompareTag("Shark"))
        {
            _animator.Play("Shark camera");
        }
        else
        {
            _animator.Play("Player camera");
        }
        _playerCamera = !_playerCamera;
    }

    private void SwitchToMainState()
    {
        _animator.Play("Player camera");
    }


}

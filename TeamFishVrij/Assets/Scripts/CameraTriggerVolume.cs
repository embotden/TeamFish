using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CameraTriggerVolume : MonoBehaviour
{
    public Animator _animator;

    private bool _playerCamera = true;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fhinn")) SwitchState();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Fhinn")) SwitchToMainState();
    }

    private void SwitchState()
    {
        if(gameObject.CompareTag("Lookout"))
        {
            _animator.Play("Lookout camera");
        }
        else if(gameObject.CompareTag("Door"))
        {
            _animator.Play("Door camera");
        }
        else if(gameObject.CompareTag("Shark"))
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
        Debug.Log("Switching to player");
    }
}

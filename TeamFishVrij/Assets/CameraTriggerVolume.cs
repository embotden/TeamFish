using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CameraTriggerVolume : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private bool _playerCamera = true;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fhinn")) SwitchState();
        Debug.Log("Look out point");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Fhinn")) SwitchState();
        Debug.Log("player camera");
    }

    private void SwitchState()
    {
        if (_playerCamera)
        {
            _animator.Play("Lookout camera");
        }
        else
        {
            _animator.Play("Player camera");
        }
        _playerCamera = !_playerCamera;
    }
}

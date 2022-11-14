using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CM_S3 : MonoBehaviour
{
    public Animator _s3_animator;

    private bool _playerCamera;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fhinn")) SwitchState();
    }

    private void SwitchState()
    {

    }
}

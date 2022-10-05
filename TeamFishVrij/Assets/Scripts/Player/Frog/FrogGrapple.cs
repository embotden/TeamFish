using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Dave - Advanced Grappling Hook in 11 minutes

public class FrogGrapple : MonoBehaviour
{
    [Header("References")]
    private FrogController _frogmove;
    public Transform _cam;
    public Transform _mouth;
    public LineRenderer _lr;

    [Header("Grappling")]
    public LayerMask _whatIsGrappleable;
    public float _maxGrappleDistance;
    public float _grappleDelayTime;
    private Vector3 _tonguetip;

    [Header("Cooldown")]
    public float _grCooldown;
    private float _grCooldownTimer;

    [Header("Input")]
    public KeyCode _grappleKey = KeyCode.P;

    private bool _isGrappling;



    void Start()
    {
        _frogmove = GetComponent<FrogController>();

    }


    void Update()
    {
        if (Input.GetKeyDown(_grappleKey)) StartGrapple();

        if(_grCooldownTimer > 0)
        {
            _grCooldownTimer -= Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        if(_isGrappling)
        {
            _lr.SetPosition(0, _mouth.position);
        }
    }


    //GRAPPLING
    private void StartGrapple()
    {
        if (_grCooldownTimer > 0) return;

        _isGrappling = true;

        RaycastHit hit;
        if(Physics.Raycast(_cam.position, _cam.forward, out hit, _maxGrappleDistance, _whatIsGrappleable))
        {
            _tonguetip = hit.point;

            Invoke(nameof(ExecuteGrapple), _grappleDelayTime);
        } else
        {
            _tonguetip = _cam.position + _cam.forward * _maxGrappleDistance;

            Invoke(nameof(StopGrapple), _grappleDelayTime);
        }

        _lr.enabled = true;
        _lr.SetPosition(1, _tonguetip);
    }

    private void ExecuteGrapple()
    {

    }

    private void StopGrapple()
    {
        _isGrappling = false;

        _grCooldownTimer = _grCooldown;

        _lr.enabled = false;

    }
}

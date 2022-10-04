using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _speed; //movement speed
    [SerializeField] private Rigidbody _rb;
    public float _turnSmoothTime = 0.1f;
    float _turnSmoothVelocity;

    [Header("Jumping")]
    [SerializeField] private float _jumpForce = 200;
    [SerializeField] private float _fallMultiplier = 7;
    [SerializeField] private float _lowJumpMultiplier = 5f;
    [SerializeField] private bool _isGrounded;
    [SerializeField] private bool _canDoubleJump;

    [Header("Sprinting")]
    [SerializeField] private bool _isSprinting = false;
    [SerializeField] private float _sprintingSpeed = 6f;
    [SerializeField] private float _walkingSpeed = 2f;

    [Header("Climbing")]
    public Transform _orientation;
    public LayerMask _whatIsWall;

    public float _climbSpeed;
    public float _maxClimbTime;
    private float _climbTimer;

    private bool _isClimbing;

    public float _detectionLength;
    public float _sphereCastRadius;
    public float _maxWallLookAngle;
    private float _wallLookAngle;

    private RaycastHit frontWallHit;
    private bool WallFront;




    private void Awake()
    {
        _speed = _walkingSpeed;
    }




    void Update()
    {
        //WALKING
        var vel = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized * _speed; //walking around
        vel.y = _rb.velocity.y;
        _rb.velocity = vel;

        //TURNING CHARACTER
        if (vel != Vector3.zero)
        {
            float targetAngle = Mathf.Atan2(vel.x, vel.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

        //JUMPING + DOUBLE JUMP
        if (_isGrounded)
        {
            _canDoubleJump = true;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _rb.AddForce(Vector3.up * _jumpForce); //if player press spacebar & on ground: jump
            }

        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && _canDoubleJump)
            {
                _rb.AddForce(Vector3.up * _jumpForce); //if player press spacebar & in air: jump
                _canDoubleJump = false;
            }
        }

        if (_rb.velocity.y < 0)
        {
            _rb.velocity += Vector3.up * Physics2D.gravity.y * (_fallMultiplier - 1) * Time.deltaTime;
        }
        else if (_rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            _rb.velocity += Vector3.up * Physics2D.gravity.y * (_lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if (_rb.velocity.y < 0) _rb.velocity += Vector3.up * Physics.gravity.y * _fallMultiplier * Time.deltaTime;

        //SPRINTING
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _isSprinting = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _isSprinting = false;
        }

        if (_isSprinting)
        {
            _speed = _sprintingSpeed;
        }
        else
        {
            _speed = _walkingSpeed;
        }

        //CLIMBING
        WallCheck();
        //StateMachine();
        if (_isClimbing) ClimbingMovement();
    }




    //IS THE PLAYER STANDING ON THE FLOOR?
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            _isGrounded = true;
        }
    }
    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            _isGrounded = false;
        }
    }


    //CLIMBING
    /*private void StateMachine()
    {
        //state 1 - climbing
        if (WallFront && Input.GetKey(KeyCode.Q) && _wallLookAngle < _maxWallLookAngle)
        {
            if (!_isClimbing && _climbTimer > 0) StartClimbing();

            //timer
            if (_climbTimer > 0) _climbTimer -= Time.deltaTime;
            if (_climbTimer < 0) StopClimbing();
        }

        //state 3 - None
        else
        {
            if (_isClimbing) StopClimbing();
        }
    }*/

    private void WallCheck()
    {
        WallFront = Physics.SphereCast(transform.position, _sphereCastRadius, _orientation.forward, out frontWallHit, _detectionLength, _whatIsWall);
        _wallLookAngle = Vector3.Angle(_orientation.forward, -frontWallHit.normal);

        if (_isGrounded)
        {
            _climbTimer = _maxClimbTime;
        }
    }

    private void StartClimbing()
    {
        _isClimbing = true;
    }

    private void ClimbingMovement()
    {
        _rb.velocity = new Vector3(_rb.velocity.x, _climbSpeed, _rb.velocity.z);
    }

    private void StopClimbing()
    {
        _isClimbing = false;
    }
}

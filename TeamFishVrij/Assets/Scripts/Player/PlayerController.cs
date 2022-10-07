using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//How to make a moving character in Unity - Tarodev
//Sprinting Unity Tutorial - Brackeys
//Third person movement in Unity - Brackeys

public class PlayerController : MonoBehaviour
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

    [Header("Raycast")]
    private RaycastHit frontWallHit;


    private void Awake()
    {
        _speed = _walkingSpeed;
    }


    void Update()
    {
        if (DialogueManager.GetInstance()._isDialoguePlaying)
        {
            return;
        }

        //WALKING
        var vel = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized * _speed; //walking around
        vel.y = _rb.velocity.y;
        _rb.velocity = vel;


            //TURNING CHARACTER
            if (vel != Vector3.zero) //If we're not standing still
        {
            float targetAngle = Mathf.Atan2(vel.x, vel.z) *Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //Walking animation


        }

        //JUMPING + DOUBLE JUMP
        if (_isGrounded)
        {
            _canDoubleJump = true;

            if (Input.GetKeyDown(KeyCode.Space))
            {
            _rb.AddForce(Vector3.up * _jumpForce); //if player press spacebar & on ground: jump


                //Jumping animation

            }

        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && _canDoubleJump)
            {
                _rb.AddForce(Vector3.up * _jumpForce); //if player press spacebar & in air: jump
                _canDoubleJump = false;

                //Double jump animation


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
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            _isSprinting = false;

        }

        if(_isSprinting)
        {
            _speed = _sprintingSpeed;

            //Sprinting animation

        }
        else
        {
            _speed = _walkingSpeed;

            //Sprinting animation stop

        }
    }


    //IS THE PLAYER STANDING ON THE FLOOR?
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor")
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

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//How to make a moving character in Unity - Tarodev
//Sprinting Unity Tutorial - Brackeys
//Third person movement in Unity - Brackeys

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    private CharacterController _characterController;
    public MainMenuNavigator _MainMenuOptions;

    public Animator animator;

    private Vector3 playerVelocity;

    private float _speed; //movement speed
    public float _turnSmoothTime = 0.1f;
    float _turnSmoothVelocity;

    [Header("Jumping")]
    PlayerInputActions _jumpControls;
    private bool _jumpPressed = false;
    private bool _isGrounded;
    private bool _isJumping;
    private bool _isFalling;
    private float _jumpForce = 1f;

    [SerializeField] private float _yVelocity;
    [SerializeField] private float _gravity = -5f;

    [Header("Sprinting")]
    public bool _isSprinting = false;
    private float _sprintingSpeed = 6f;
    private float _walkingSpeed = 2f;


    private void Awake()
    {
        _speed = _walkingSpeed;

        _jumpControls = new PlayerInputActions();

        _characterController = GetComponent<CharacterController>();
        _yVelocity = _gravity;
    }


    void Update()
    {
        //_isGrounded = _characterController.isGrounded;

        /*if (_isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }*/

        //WALKING
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); //walking around

        if(!DialogueManager.GetInstance()._isDialoguePlaying) _characterController.Move(move * Time.deltaTime * _speed);



        //DIALOGUE
        if (DialogueManager.GetInstance()._isDialoguePlaying)
        {
            return;
        }

        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        var characterMovement = new Vector3(horizontal, 0, vertical); //walking around


        //JUMPING
        MovementJump();


        //TURNING CHARACTER
        if (move != Vector3.zero) //If we're not standing still
        {
            float targetAngle = Mathf.Atan2(characterMovement.x, characterMovement.z) *Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //Walking animation
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }


        //SPRINTING
        if(_isSprinting)
        {
            //change speed to running
            _speed = _sprintingSpeed;

            //Sprinting animation
            animator.SetBool("IsSprinting", true);
        }
        else
        {
            //change speed to walking
            _speed = _walkingSpeed;

            //Sprinting animation stop
            animator.SetBool("IsSprinting", false);
        }

        //ANIMATIONS
        if(_isGrounded)
        {
            _isFalling = false;
            animator.SetBool("IsGrounded", true);
            animator.SetBool("IsFalling", false);
        }

        if(_isFalling)
        {
            _isJumping = false;
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsFalling", true);
        }
        else if (_isJumping)
        {
            _isGrounded = false;
            animator.SetBool("IsGrounded", false);
            animator.SetBool("IsJumping", true);
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
            animator.SetBool("IsGrounded", false);
        }
    }

    private void MovementJump()
    {
        _isGrounded = _characterController.isGrounded;

        if (_isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        if(_jumpPressed && _isGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(_jumpForce * -3f * _gravity);
            _isJumping = true;
            _jumpPressed = false;
        }

        if (_isJumping)
        {
            //animator.SetBool("IsFalling", true);
            _isFalling = true;
        }

        playerVelocity.y += _gravity * Time.deltaTime;

        _characterController.Move(playerVelocity * Time.deltaTime);
    }

    public void OnJump()
    {
        if (_isGrounded)
        {
            _jumpPressed = true;
        }
    }

    public void OnSprintStart()
    {
        _isSprinting = true;
    }

    public void OnSprintStop()
    {
        _isSprinting = false;
    }

}

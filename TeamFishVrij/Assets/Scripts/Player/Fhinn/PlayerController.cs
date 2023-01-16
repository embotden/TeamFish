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
    private MainMenuNavigator _watchingMenuOption;
    private DialogueManager _checkDialogue;
    private PaintingPlay _checkCutscene;

    public Animator animator;

    public int AbilityLayerIndex;
    private int TailLayerIndex;

    private float currentLayerWeightTail;
    private float currentLayerWeightAbility;
    private float yVelocity = 0.0F;
    private float smoothTime = 0.3f;
    private bool _isPlayingRelease;
    //public bool _canKeepMoving = true;
    public bool _canIMove = true;

    //public WaterAbility Ability_1;
    //public WaterAbility Ability_2;
    //public WaterAbility Ability_3;

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

        AbilityLayerIndex = animator.GetLayerIndex("ArmAbility");
        TailLayerIndex = animator.GetLayerIndex("Tail");
    }


    void Update()
    {
        // USED FOR SMOOTHLY SWITCHING BETWEEN ANIMATION LAYERS
        currentLayerWeightTail = animator.GetLayerWeight(TailLayerIndex);
        currentLayerWeightAbility = animator.GetLayerWeight(AbilityLayerIndex);

        //WALKING
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); //walking around

        //DIALOGUE
        if (DialogueManager.GetInstance()._isDialoguePlaying || !_canIMove)
        {
            animator.SetBool("IsSprinting", false);
            animator.SetBool("IsWalking", false);
            return;
        }

        //if(!DialogueManager.GetInstance()._isDialoguePlaying && _canIMove) _characterController.Move(move * Time.deltaTime * _speed);
        _characterController.Move(move * Time.deltaTime * _speed);


        /*if (DialogueManager.GetInstance()._isDialoguePlaying)
        {
            return;
        }
        else if(_watchingMenuOption)
        {
            if(_watchingMenuOption._isWatching)
            {
                return;
            }
        }*/

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

        //STOP PLAYER FROM MOVING
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Ability"))
        {
            _speed = 0.0f;
        }

        // Checks if the AbilityRelease state has a transition
        if (animator.GetAnimatorTransitionInfo(2).IsName("AbilityRelease -> Idle") || animator.GetAnimatorTransitionInfo(2).IsName("AbilityRelease -> Walk") || animator.GetAnimatorTransitionInfo(2).IsName("AbilityRelease -> Sprint"))
        {
            _isPlayingRelease = true;
        }

        else
        {
            _isPlayingRelease = false;
        }

        // SWITCH ANIMATION LAYER
        if (animator.GetCurrentAnimatorStateInfo(1).IsName("Idle"))
        {
            float endWeightA = Mathf.SmoothDamp(currentLayerWeightAbility, 0, ref yVelocity, smoothTime);
            animator.SetLayerWeight(AbilityLayerIndex, endWeightA);
        }

        if (animator.GetCurrentAnimatorStateInfo(1).IsName("Ability Hold"))
        {
            float startWeightT = Mathf.SmoothDamp(currentLayerWeightTail, 1, ref yVelocity, smoothTime);
            animator.SetLayerWeight(TailLayerIndex, startWeightT);
        }

        if (animator.GetCurrentAnimatorStateInfo(1).IsName("Idle") && _isPlayingRelease == false)
        {
            float endWeightT = Mathf.SmoothDamp(currentLayerWeightTail, 0, ref yVelocity, smoothTime);
            animator.SetLayerWeight(TailLayerIndex, endWeightT);
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

        /*if(_jumpPressed && _isGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(_jumpForce * -3f * _gravity);
            _isJumping = true;
            _jumpPressed = false;
        }

        if (_isJumping)
        {
            //animator.SetBool("IsFalling", true);
            _isFalling = true;
        }*/

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

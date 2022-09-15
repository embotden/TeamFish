using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//How to make a moving character in Unity - Tarodev

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed; //movement speed
    [SerializeField] private Rigidbody _rb;
    public float _turnSmoothTime = 0.1f;
    float _turnSmoothVelocity;

    [SerializeField] private float _jumpForce = 200;
    [SerializeField] private float _fallMultiplier = 7;
    [SerializeField] private float _lowJumpMultiplier = 5f;
    [SerializeField] private bool _isGrounded;
    [SerializeField] private bool _canDoubleJump;
    [SerializeField] private float _doubleJumpMultiplier = 0.1f;

    [SerializeField] private bool _isSprinting = false;
    [SerializeField] private float _sprintingMultiplier = 2f;



    void Update()
    {
        //WALKING
        var vel = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized * _speed; //walking around
        vel.y = _rb.velocity.y;
        _rb.velocity = vel;

        //TURNING CHARACTER
        if(vel != Vector3.zero)
        {
            float targetAngle = Mathf.Atan2(vel.x, vel.z) *Mathf.Rad2Deg;
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
        else
        {
            _isSprinting = false;
        }

        if(_isSprinting == true)
        {
            _speed *= _sprintingMultiplier;
        }
    }


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

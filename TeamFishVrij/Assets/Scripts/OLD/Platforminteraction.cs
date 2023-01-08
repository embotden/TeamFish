using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforminteraction : MonoBehaviour
{
    public bool _isCarrying;
    public bool _isActive = false;

    public GameObject _statue1;

    public float _heightDifference = 0.32f;

    public leverInteraction _movingDirection;

    private Vector3 _rotation;

    private float speed;

    PlayerInputActions _rotationMechanic;



    private void Start()
    {
        _rotationMechanic = new PlayerInputActions();
    }

    private void Update()
    {
        if (_isActive)
        {
            //raise platform
            gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + _heightDifference, transform.position.z);
        }
        else
        {
            gameObject.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("statue"))
        {
            Debug.Log("I'm carrying someting");
            _isCarrying = true;

            if(_isActive)
            {
                other.transform.SetParent(this.transform); //make statue a parent of this platform.
            }
            else
            {
                other.transform.SetParent(null);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("statue"))
        {
            Debug.Log("I let go");
            _isCarrying = false;

            other.transform.SetParent(null);
        }
    }

    public void OnLeverLeft()
    {
        if (_isActive)
        {
            if (_isCarrying)
            {
                _rotation = Vector3.up;

                transform.Rotate(_rotation * speed * Time.deltaTime);

                Debug.Log("Pushing left!");
            }
            else
            {
                Debug.Log("Noting to push");

                //add a short animation that shows platform rising and dropping
            }
        }

    }

    public void OnLeverRight()
    {
        if (_isActive)
        {
            if (_isCarrying)
            {
                _rotation = Vector3.down;

                transform.Rotate(_rotation * speed * Time.deltaTime);

                Debug.Log("Pushing right!");
            }
            else
            {
                Debug.Log("Noting to push");

                //add a short animation that shows platform rising and dropping
            }
        }
    }

    public void OnLeverLetGo()
    {
        if (_isActive)
        {
            Debug.Log("I stopped pushing");

            _rotation = Vector3.zero;

            transform.Rotate(_rotation * speed * Time.deltaTime);

        }

    }
}







    

   


    //1 lever connected to platform

    //if player pushes lever left, platform rotates counter clockwise

    //if player pushes lever right, platform rotates clockwise

    //statues move along waypoint

    //platform can't rotate further if statue is agains bath

    //statue can switch platform on cross points


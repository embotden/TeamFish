using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Implementing climbing - HamJoy Games

public class PlayerClimbing : MonoBehaviour
{

    [Header("References")]
    public Transform orientation;
    public Rigidbody _rb;
    public LayerMask whatIsWall;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //DOWNWARD CAST TO FIND A LEDGE


        //FORWARD CAST TO SAMPLE THE VERTICAL SURFACE


        //OVERPASS CAST TO SEE IF WE SHOULD JUST WALK UNDERNEATH IT


        //DETERMINE IF LEDGE IS NOT TOO STEEP


        //CHECK HOW DIRECTLY THE CHARACTER IS MOVING TOWARDS THE WALL


        //APPLY AN OFFSET TO MOVE AWAY FROM THE EDGE


        //DEPENETRATE THE POINT FOR THE CAPSULE


        //CHECK FOR SPACE BY DOING A CAPSULE SWEEP UP AND FORWARD
    }


}

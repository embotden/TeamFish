using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GargoyleManager : MonoBehaviour
{
    //manages movement of all the platforms



    public bool _movingLeft;
    //public bool _movingRight;




    private void Start()
    {
        //_movingLeft = false;
    }




    /*public Transform _getWaypoint(int _waypointIndex)
    {
        return transform.GetChild(_waypointIndex);
    }

    public int GetNextWaypointIndex(int _currentWaypointIndex)
    {
        //int _leftWaypointIndex = _currentWaypointIndex - 1;
        //int _rightWaypointIndex = _currentWaypointIndex + 1;

        int _nextWaypointIndex;

        if (_movingLeft)
        {
            _nextWaypointIndex = _currentWaypointIndex - 1;

            if (_nextWaypointIndex < 0)
            {
                //_nextWaypointIndex = 0;
                Debug.Log("I hit a wall on my left!");
            }

            //return _nextWaypointIndex;
        }
        else
        {
            _nextWaypointIndex = _currentWaypointIndex + 1;

            if (_nextWaypointIndex == transform.childCount)
            {
                //_nextWaypointIndex = 0;
                Debug.Log("I hit a wall on my right");
            }

            //return _nextWaypointIndex;
        }

        return _nextWaypointIndex;
    }*/


    //3 levers

    //1 script per platform

    //if player rotates le

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingStatue : MonoBehaviour
{
    //checks if statue hits light

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("light beam"))
        {
            Debug.Log("light");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("light beam"))
        {
            Debug.Log("Dark");
        }
    }


}

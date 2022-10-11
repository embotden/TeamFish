using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterReaction : MonoBehaviour
{
    public WaterFollow _waterMovementScript;
    public PlantReaction _plantDestructionScript;



    private void OnCollisionEnter(Collision collidor)
    {
        //_waterMovementScript._hitObject = true;
        Debug.Log("I hit something");

        /*if (collidor.gameObject.tag == "plant hitpoint")
        {
            //Make plant react
            //_waterMovementScript._hitPlant = true;
            _plantDestructionScript.plantReaction();
            Debug.Log("That was a plant");
        }*/
    }
}

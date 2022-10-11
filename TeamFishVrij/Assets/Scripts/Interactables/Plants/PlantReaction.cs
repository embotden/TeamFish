using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantReaction : MonoBehaviour
{
    public GameObject _plantManager;

    public bool _plantIsHitWater = false;


    private void Update()
    {
        if (_plantIsHitWater) plantReactingWater();
    }


    public void plantReactingWater()
    {
        //Call upon plant manager
        _plantManager.GetComponent<PlantManager>().PlantGrowing();

        //Deactivate hitbox
        gameObject.GetComponent<BoxCollider>().enabled = false;

        //Turn off visual cue
        gameObject.GetComponent<MeshRenderer>().enabled = false;

        //Reset
        _plantIsHitWater = false;
    }
}

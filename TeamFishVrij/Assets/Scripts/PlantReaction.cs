using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantReaction : MonoBehaviour
{
    /*
    public WaterAbility _waterControlScript;
    public GameObject _water;
    public GameObject _vines;*/

    public GameObject _destructionReaction;
    //private GameObject _thisVine;

    public bool _plantIsHit = false;

    private void Update()
    {
        if (_plantIsHit) plantReacting();
    }


    public void plantReacting()
    {
        _destructionReaction.SetActive(true);

        gameObject.SetActive(false);
    }

    public void plantGrow()
    {
        //Plant grows taller - Collider moves with it.

        Destroy(gameObject);
    }
}

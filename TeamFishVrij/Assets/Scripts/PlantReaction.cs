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


    public void plantReaction()
    {
        _destructionReaction.SetActive(true);

        gameObject.SetActive(false);
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractables : MonoBehaviour
{
    //[Header("Visual Cues")]
    //public GameObject _water;
    //public GameObject _dialogue;
    //public GameObject _jump;

    [Header("Conditions")]
    public bool _canTalk;
    public bool _canUseWater;
    //public bool _canJump;


    // Start is called before the first frame update
    void Awake()
    {
        //_water.SetActive(false);
        //_dialogue.SetActive(false);

        _canUseWater = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_canUseWater);

        
    }
}

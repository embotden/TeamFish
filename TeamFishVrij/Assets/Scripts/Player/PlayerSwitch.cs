using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitch : MonoBehaviour
{
    public PlayerController mcController;
    public FrogController frogController;

    public bool mcActive = true;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {

        }
    }

    public void SwitchPlayer()
    {
        if(mcActive)
        {
            mcController.enabled = false;
            frogController.enabled = true;
            mcActive = false;
        }
        else
        {
            mcController.enabled = true;
            frogController.enabled = false;
            mcActive = true;
        }
    }
}

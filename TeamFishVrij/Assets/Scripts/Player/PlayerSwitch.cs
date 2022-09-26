using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitch : MonoBehaviour
{
    public PlayerController mcController;
    public FrogController frogController;

    private Animator animator;

    public bool mcActive = true;

    private bool mcCamera = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SwitchPlayer();
        }
    }

    public void SwitchPlayer()
    {
        if(mcActive)
        {
            mcController.enabled = false;
            frogController.enabled = true;
            mcActive = false;

            animator.Play("Camera_Frog");
        }
        else
        {
            mcController.enabled = true;
            frogController.enabled = false;
            mcActive = true;

            animator.Play("Camera_MC");
        }

        mcCamera = !mcCamera;
    }
}

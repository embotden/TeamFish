using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitch : MonoBehaviour
{
    public PlayerController mcController;
    public FrogController frogController;
    public SharkController sharkController;
    public FireflyController fireflyController;

    private Animator animator;

    public bool mcActive = true;
    public bool frogActive = false;
    public bool sharkActive = false;
    public bool fireflyActive = false;

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
            sharkController.enabled = false;
            fireflyController.enabled = false;

            mcActive = false;
            frogActive = true;
            sharkActive = false;
            fireflyActive = false;

            animator.Play("Camera_Frog");
        }
        else if(frogActive)
        {
            mcController.enabled = false;
            frogController.enabled = false;
            sharkController.enabled = true;
            fireflyController.enabled = false;

            mcActive = false;
            frogActive = false;
            sharkActive = true;
            fireflyActive = false;

            animator.Play("Camera_Shark");
        }
        else if(sharkActive)
        {
            mcController.enabled = false;
            frogController.enabled = false;
            sharkController.enabled = false;
            fireflyController.enabled = true;

            mcActive = false;
            frogActive = false;
            sharkActive = false;
            fireflyActive = true;

            animator.Play("Camera_Firefly");
        }
        else
        {
            mcController.enabled = true;
            frogController.enabled = false;
            sharkController.enabled = false;
            fireflyController.enabled = false;

            mcActive = true;
            frogActive = false;
            sharkActive = false;
            fireflyActive = false;

            animator.Play("Camera_MC");
        }

        mcCamera = !mcCamera;
    }
}

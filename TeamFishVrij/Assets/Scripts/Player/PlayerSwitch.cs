using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitch : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private bool mcCamera = true;


    public PlayerController mcController;
    public FrogController frogController;

    public bool mcActive = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SwitchPlayer();
            //SwitchState();

        }
    }

    public void SwitchPlayer()
    {
        if(mcActive)
        {
            mcController.enabled = false;
            frogController.enabled = true;
            mcActive = false;

            animator.Play("Frog camera");
        }
        else
        {
            mcController.enabled = true;
            frogController.enabled = false;
            mcActive = true;

            animator.Play("MC camera");
        }

        mcCamera = !mcCamera;
    }
}

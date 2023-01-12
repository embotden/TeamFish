using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FhinnEmblemFlicker : MonoBehaviour
{
    private Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("IsOld", true);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonAnimation : MonoBehaviour
{
    private Animator _buttonAnimator;

    //[SerializeField] private PlayerInputActions _buttonInteractions;

    void OnSubmit()
    {
        _buttonAnimator.SetTrigger("Pressed");
    }



    
}

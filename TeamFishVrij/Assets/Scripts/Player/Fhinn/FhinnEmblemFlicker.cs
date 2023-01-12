using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FhinnEmblemFlicker : MonoBehaviour
{
    private GameObject _Fhinn;

    void Start()
    {
        _Fhinn = GameObject.Find("/Characters/MC/MOD_Fhinn");
        Animator _FhinnAnimator = _Fhinn.GetComponent<Animator>();
        _FhinnAnimator.SetBool("IsOld", true);
    }
}

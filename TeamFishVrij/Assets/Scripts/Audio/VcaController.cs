using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VcaController : MonoBehaviour
{
    private FMOD.Studio.VCA _VcaController;

    public string _VcaName; //write the name of the needed VCA in the inspector in Unity

    private Slider _slider;

    [SerializeField] private float _VcaVolume;


    void Start()
    {
        _VcaController = FMODUnity.RuntimeManager.GetVCA("vca:/" + _VcaName);
        _slider = GetComponent<Slider>();
        _VcaController.getVolume(out _VcaVolume);
    }


    public void SetVolume(float volume)
    {
        _VcaController.setVolume(volume);
        _VcaController.getVolume(out _VcaVolume);
    }
}

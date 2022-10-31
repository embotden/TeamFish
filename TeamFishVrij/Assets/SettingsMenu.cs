using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    //public AudioMixer _audioMixer;
    Resolution[] _resolutions;
    //public Dropdown _resDropdown;
    public TMP_Dropdown _TMPResDropdown;

    private void Start()
    {
        _resolutions = Screen.resolutions;

        //_resDropdown.ClearOptions();
        _TMPResDropdown.ClearOptions();

        List<string> _options = new List<string>();

        int _currentResIndex = 0;

        for(int i=0; i<_resolutions.Length; i++)
        {
            string option = _resolutions[i] + "x" + _resolutions[i].height;
            _options.Add(option);

            if (_resolutions[i].width == Screen.currentResolution.width && _resolutions[i].height == Screen.currentResolution.height)
            {
                _currentResIndex = i;
            }
        }

        _TMPResDropdown.AddOptions(_options);
        _TMPResDropdown.value = _currentResIndex;
        _TMPResDropdown.RefreshShownValue();
        /*_resDropdown.AddOptions(_options);
        _resDropdown.value = _currentResIndex;
        _resDropdown.RefreshShownValue();*/
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution _resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(_resolution.width, _resolution.height, Screen.fullScreen);
    }

    public void SetVolumeMusic(float volume)
    {
        Debug.Log("music volume changed");
    }

    public void SetVolumeSound(float volume)
    {
        Debug.Log("music volume changed");
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    public List<ResItem> _resolutions = new List<ResItem>();

    private int _selectedRes;

    public TMP_Text _resLabel;

    public Toggle _fullScreen;

    private void Start()
    {
        _fullScreen.isOn = Screen.fullScreen;

        bool _foundRes = false;
        for(int i=0; i< _resolutions.Count; i++)
        {
            if(Screen.width == _resolutions[i]._horizontal && Screen.height == _resolutions[i]._vertical)
            {
                _foundRes = true;

                _selectedRes = i;

                UpdateResLabel();
            }
        }

        if(!_foundRes)
        {
            ResItem _newRes = new ResItem();
            _newRes._horizontal = Screen.width;
            _newRes._vertical = Screen.height;

            _resolutions.Add(_newRes);
            _selectedRes = _resolutions.Count - 1;

            UpdateResLabel();
        }
    }

    public void ResLeft()
    {
        _selectedRes--;
        if (_selectedRes < 0) _selectedRes = 0;

        UpdateResLabel();
    }

    public void ResRight()
    {
        _selectedRes++;
        if (_selectedRes > _resolutions.Count - 1) _selectedRes = _resolutions.Count - 1;

        UpdateResLabel();
    }

    public void UpdateResLabel()
    {
        _resLabel.text = _resolutions[_selectedRes]._horizontal.ToString() + "x" + _resolutions[_selectedRes]._vertical.ToString();
    }

    public void ApplyGraphics()
    {
        Screen.SetResolution(_resolutions[_selectedRes]._horizontal, _resolutions[_selectedRes]._vertical, _fullScreen.isOn);
    }
}

[System.Serializable]
public class ResItem
{
    public int _horizontal, _vertical;
}
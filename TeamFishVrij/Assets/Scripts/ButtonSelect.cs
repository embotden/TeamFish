using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelect : MonoBehaviour
{
    public Button _primaryButton;

    /*private void Update()
    {
        Debug.Log("I exist as well");
    }*/

    private void Start()
    {
        _primaryButton.Select();
    }
}

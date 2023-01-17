using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BforBack : MonoBehaviour
{

    public Animator b_animation;
    PlayerInputActions _playerInputB;
    public bool _canBeShown;

    // Start is called before the first frame update
    void Start()
    {
        _playerInputB = new PlayerInputActions();
    }

    // Update is called once per frame
    void Update()
    {
        if (_canBeShown) b_animation.SetBool("canShow", true);
    }

    void OnBack()
    {
        if(_canBeShown)
        {
            b_animation.SetBool("canShow", false);
            _canBeShown = false;
        }
    }

}

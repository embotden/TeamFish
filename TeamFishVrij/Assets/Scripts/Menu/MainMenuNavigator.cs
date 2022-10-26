using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuNavigator : MonoBehaviour
{
    public GameObject _headerOption;
    public GameObject _visualCue;
    

    public Animator _menuAnimator;

    public bool _isWatching { get; private set; }
    [SerializeField] private bool _canExit;
    [SerializeField] private bool _canSwitch;

    // Start is called before the first frame update
    void Awake()
    {
        _headerOption.SetActive(false);
        _visualCue.SetActive(false);
        _isWatching = false;
        _canExit = true;
        _canSwitch = false;
    }

    private void Update()
    {
        if(_canSwitch && Input.GetKeyDown(KeyCode.Q))
        {
            SwitchState();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(_canExit)
            {
                //Turn on are you sure UI

                Debug.Log("Suggesting quit game");
            }
            else
            {
                SwitchToMainState();
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Fhinn"))
        {
            _headerOption.SetActive(true);
            _visualCue.SetActive(true);

            _canSwitch = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Fhinn"))
        {
            _headerOption.SetActive(false);
            _visualCue.SetActive(false);

            _canSwitch = false;
        }
    }

    private void SwitchState()
    {
        //Debug.Log("Switching state...");
                
        if (gameObject.CompareTag("Options"))
        {
            _menuAnimator.Play("Window camera");
            _isWatching = true;
        }
        else if (gameObject.CompareTag("Story"))
        {
            _menuAnimator.Play("Exposition camera");
            _isWatching = true;
        }
        else if (gameObject.CompareTag("Collection"))
        {
            _menuAnimator.Play("Memories camera");
            _isWatching = true;
        }
        else if (gameObject.CompareTag("Bath"))
        {
            _menuAnimator.Play("Start camera");
            _isWatching = true;
        }
        else
        {
            SwitchToMainState();
        }
    }

    private void SwitchToMainState()
    {
        _menuAnimator.Play("Overview camera");
        _isWatching = false;
    }
}

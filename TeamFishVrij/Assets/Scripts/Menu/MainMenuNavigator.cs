using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuNavigator : MonoBehaviour
{
    public GameObject _headerOption;
    public GameObject _visualCue;
    

    public Animator _menuAnimator;

    public bool _isWatching { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        _headerOption.SetActive(false);
        _visualCue.SetActive(false);
        _isWatching = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SwitchState();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Fhinn"))
        {
            _headerOption.SetActive(true);
            _visualCue.SetActive(true);

            if(Input.GetKeyDown(KeyCode.Q)) SwitchState();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Fhinn"))
        {
            _headerOption.SetActive(false);
            _visualCue.SetActive(false);
        }
    }

    private void SwitchState()
    {
        _isWatching = true;

        if (gameObject.CompareTag("Options"))
        {
            _menuAnimator.Play("Window camera");
        }
        else if (gameObject.CompareTag("Story"))
        {
            _menuAnimator.Play("Exposition camera");
        }
        else if (gameObject.CompareTag("Collection"))
        {
            _menuAnimator.Play("Memories camera");
        }
        else if (gameObject.CompareTag("Bath"))
        {
            _menuAnimator.Play("Start camera");
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

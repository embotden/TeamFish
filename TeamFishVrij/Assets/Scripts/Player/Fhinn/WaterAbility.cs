using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterAbility : MonoBehaviour
{
    PlayerInputActions _waterAbilityButton;


    [Header("Assets")]
    public GameObject _waterEffect;
    //public GameObject _splashEffect;
    public GameObject _waterSpawn;
    //[SerializeField] private GameObject _fhinn;
    public GameObject _visualCue;
    //public WaterFollow _waterAbilityScript;
    //public PlayerInteractables _visualCueScript;
    public GameObject _targetHint;

    public DialogueManager _dialogueCheck;

    [Header("Start")]
    [SerializeField] public bool _isNearWater;
    [SerializeField] public bool _canPickupWater;

    [Header("Duration")]
    public bool _hitObject = false;
    [SerializeField] private float _duration;
    public PlantReaction _plantHitpoint;

    [Header("Throwing")]
    public bool _waterThrown = true;
    [SerializeField] public bool _waterHolding;

    [Header("UI Animation")]
    public Animator _UIAnimation;
    private bool _canShowUI = false;
    private bool _isShowingUI = false;

    //[Header("Water Animation")]
    //public Animator _wateranimations;

    public bool _isStartingAbility;
    public bool _abilityReleased;



    private void Awake()
    {
        _canPickupWater = true;
        _isNearWater = false;

        //_visualCue.SetActive(false);
        _targetHint.SetActive(false);

        _waterAbilityButton = new PlayerInputActions();

        _UIAnimation.SetBool("canShow", false);
    }

    //check if player is near water source
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fhinn")
        {
            _isNearWater = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Fhinn")
        {
            _isNearWater = false;
        }
    }


    private void Update()
    {
        if (!_dialogueCheck._DialogueWaterCheck)
        {
            if (_isNearWater && _canPickupWater) //if the player is near water
            {
                _canShowUI = true;

                if (!_isShowingUI) StartCoroutine(ShowQUI()); //is player is near water and ui is not showing: show UI
            }
            else
            {
                _canShowUI = false; //if player is not near water 
            }
        }

        if (!_canShowUI && _isShowingUI) StartCoroutine(CloseQUI()); //if UI is showing while it can't


    }

    //check if player can grab water
    void OnWaterGrab()
    {
        if (_isShowingUI) StartCoroutine(Pickup());
    }

    public IEnumerator Pickup()
    {
        StartCoroutine(FhinnAnimation());

        //Stop player from grabbing water again
        _canPickupWater = false;

        //make ui dissapear
        _canShowUI = false;

        //spawn waterball
        var cloneWater = Instantiate(_waterEffect, _waterSpawn.transform.position, Quaternion.identity);
        WaterFollow _waterbalScript = cloneWater.GetComponent<WaterFollow>();

        //visual hint
        _targetHint.SetActive(true);

        //Time water lasts
        yield return new WaitForSeconds(_duration);

        //Destroy
        if(_waterbalScript) _waterbalScript.DestroyBall();

        //turn off visual hing
        _targetHint.SetActive(false);

        //player can grab water again
        _canPickupWater = true;
    }

    private IEnumerator ShowQUI()
    {
        _isShowingUI = true;

        yield return new WaitForEndOfFrame();

        _UIAnimation.SetBool("canShow", true);
        _UIAnimation.SetBool("canLeave", false);

    }

    private IEnumerator CloseQUI()
    {
        _UIAnimation.SetBool("canShow", false);
        _UIAnimation.SetBool("canLeave", true);

        yield return new WaitForEndOfFrame();

        _isShowingUI = false;

    }

    public IEnumerator FhinnAnimation()
    {
        _isStartingAbility = true;

        yield return new WaitForSeconds(1f);

        _isStartingAbility = false;

        yield return new WaitForSeconds(3.8f);

        _abilityReleased = true;

        yield return new WaitForSeconds(0.5f);

        _abilityReleased = false;

    }

}

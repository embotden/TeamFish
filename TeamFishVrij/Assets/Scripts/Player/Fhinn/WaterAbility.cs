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

    [Header("Start")]
    [SerializeField] public bool _isNearWater;
    [SerializeField] public bool _canPickupWater;


    [Header("Duration")]
    public bool _hitObject = false;
    [SerializeField] private float _duration;

    [Header("Throwing")]
    public bool _waterThrown = true;
    public bool _abilityReleased = false;
    [SerializeField] public bool _waterHolding = false; 

    

    private void Awake()
    {
        _canPickupWater = true;
        _isNearWater = false;

        _visualCue.SetActive(false);
        _targetHint.SetActive(false);

        _waterAbilityButton = new PlayerInputActions();

    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fhinn")
        {
            _isNearWater = true;
            _visualCue.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Fhinn")
        {
            _isNearWater = false;
            _visualCue.SetActive(false);
        }
    }

    private void Update()
    {
        if(_isNearWater && _canPickupWater)
        {
            if (Input.GetKeyDown(KeyCode.Q)) StartCoroutine(Pickup());
        }

    }

    public IEnumerator Pickup()
    {
        Debug.Log("a");
        //Stop player from grabbing water again
        _canPickupWater = false;
        _waterHolding = true;
        _waterThrown = false;
        _abilityReleased = false;

        //Animation raise water
        var cloneWater = Instantiate(_waterEffect, _waterSpawn.transform.position, Quaternion.identity);

        //Turn on light hint
        _targetHint.SetActive(true);

        //Time water lasts
        yield return new WaitForSeconds(_duration);

        //fall animation
        //Instantiate(_splashEffect, _waterEffect.transform.position, _waterEffect.transform.rotation);
        _waterThrown = true;

        yield return new WaitForSeconds(1.5f); // Extra seconds to match animation with dissapearance water

        _abilityReleased = true;

        yield return new WaitForSeconds(0.5f);

        Debug.Log("b");

        //Remove water
        //DroppingWater();

        Destroy(cloneWater);

        yield return new WaitForSeconds(0.5f); // Extra seconds to stop Fhinn release animation from repeating

        _abilityReleased = false;

        yield return new WaitForSeconds(1.5f);

        _targetHint.SetActive(false);

        _canPickupWater = true;
        _waterHolding = false;
    }

    public IEnumerator ThrowingWater()
    {
        //Stop Coroutine

        //Throw water animation

        yield return new WaitForEndOfFrame();

        //Destroy clone

    }

    public IEnumerator DroppingWater()
    {
        //Drop water animation

        yield return new WaitForEndOfFrame();

        //Destroy clone
        //Destroy(cloneWater);
    }


    /*void OnWaterGrab()
    {
        if(_isNearWater && _canPickupWater)
        {
            StartCoroutine(Pickup());
        }
        else if(_waterHolding)
        {
            _waterThrown = true;
        }
    }*/
}

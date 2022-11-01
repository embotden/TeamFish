using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterAbility : MonoBehaviour
{
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
    [SerializeField] private bool _isNearWater;
    [SerializeField] private bool _canPickupWater;


    [Header("Duration")]
    public bool _hitObject = false;
    [SerializeField] private float _duration;

    

    private void Awake()
    {
        _canPickupWater = true;
        _isNearWater = false;

        _visualCue.SetActive(false);
        _targetHint.SetActive(false);
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
        //Stop player from grabbing water again
        _canPickupWater = false;

        //Animation raise water
        var cloneWater = Instantiate(_waterEffect, _waterSpawn.transform.position, Quaternion.identity);

        //Turn on light hint
        _targetHint.SetActive(true);

        //Time water lasts
        yield return new WaitForSeconds(_duration);

        //fall animation
        //Instantiate(_splashEffect, _waterEffect.transform.position, _waterEffect.transform.rotation);

        //Remove water
        //DroppingWater();

        Destroy(cloneWater);

        yield return new WaitForSeconds(2f);

        _targetHint.SetActive(false);

        _canPickupWater = true;
    }
}

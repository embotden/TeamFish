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

    [Header("Start")]
    [SerializeField] private bool _isNearWater;
    [SerializeField] private bool _canPickupWater;


    [Header("Duration")]
    public bool _hitObject = false;
    [SerializeField] private float _duration;

    

    private void Awake()
    {
        //_waterEffect.SetActive(false);
        //_visualCue.SetActive(false);
        _canPickupWater = true;
        _isNearWater = false;

        _visualCue.SetActive(false);

        //GameObject _player = GameObject.FindWithTag("Fhinn");
        //_visualCueScript = _player.GetComponent<PlayerInteractables>();

    }


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

        if(_isNearWater && _canPickupWater)
        {
            //_visualCueScript._canUseWater = true;
            _visualCue.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Q)) StartCoroutine(Pickup());
        }
        else
        {
            //_visualCueScript._canUseWater = false;
            _visualCue.SetActive(false);
        }

        /*if(_isNearWater && Input.GetKeyDown(KeyCode.Q))
        {
            if (_canPickupWater)
            {
                StartCoroutine(Pickup());
            }
            else
            {
                Debug.Log("Water dropped");
                //DroppingWater();

            }
        }*/

    }

    public IEnumerator Pickup()
    {
        //Stop player from grabbing water again
        _canPickupWater = false;

        //Animation raise water
        var cloneWater = Instantiate(_waterEffect, _waterSpawn.transform.position, Quaternion.identity);

        //Time water lasts
        yield return new WaitForSeconds(_duration);

        //fall animation
        //Instantiate(_splashEffect, _waterEffect.transform.position, _waterEffect.transform.rotation);

        //Remove water
        //DroppingWater();

        Destroy(cloneWater);

        yield return new WaitForSeconds(2f);

        _canPickupWater = true;
    }
}

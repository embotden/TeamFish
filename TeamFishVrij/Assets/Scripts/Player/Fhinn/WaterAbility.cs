using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAbility : MonoBehaviour
{
    [Header("Assets")]
    public GameObject _waterEffect;
    public GameObject _splashEffect;
    public GameObject _waterSpawn;
    [SerializeField] private GameObject _fhinn;
    [SerializeField] private GameObject _visualCue;
    public WaterFollow _waterAbilityScript;

    [Header("Conditions")]
    [SerializeField] private bool _isNearWater = false;
    [SerializeField] private bool _canPickupWater = true;
    public bool _hitObject = false;

    [SerializeField] private float _duration;

    

    private void Awake()
    {
        //_waterEffect.SetActive(false);
        _visualCue.SetActive(false);

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
        _isNearWater = false;
    }

    private void Update()
    {
        if(_isNearWater && Input.GetKeyDown(KeyCode.Q))
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
        }

        if(_isNearWater && _canPickupWater)
        {
            _visualCue.SetActive(true);
        }
        else
        {
            _visualCue.SetActive(false);
        }
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

        _canPickupWater = true;
    }
    
    public void DroppingWater()
    {

        //remove water
        //_waterEffect.SetActive(false);
        Destroy(_waterEffect);

        //Reset
        _canPickupWater = true;
    }
}

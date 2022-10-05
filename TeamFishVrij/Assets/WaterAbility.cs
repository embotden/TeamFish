using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAbility : MonoBehaviour
{
    [Header("Water ability")]
    public GameObject _waterEffect;
    public GameObject _splashEffect;
    [SerializeField] private GameObject _fhinn;
    [SerializeField] private bool _isNearWater = false;
    [SerializeField] private bool _canPickupWater = true;
    [SerializeField] private float _duration;

    private void Awake()
    {
       // _waterEffect.SetActive()
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fhinn")
        {
            _isNearWater = true;
        }
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
            }
        }
    }

    private IEnumerator Pickup()
    {

        Debug.Log("Picking up Water");

        //Stop player from grabbing water again
        _canPickupWater = false;

        //Animation raise water
        //_waterEffect.SetActive(true);
        Instantiate(_waterEffect, transform.position, transform.rotation);

        //Time water lasts
        yield return new WaitForSeconds(_duration);

        //Splash animation
        Instantiate(_splashEffect, _waterEffect.transform.position, _waterEffect.transform.rotation);

        //Remove water
        //Destroy(_waterEffect);

        //Reset
        _canPickupWater = true;
    }
}

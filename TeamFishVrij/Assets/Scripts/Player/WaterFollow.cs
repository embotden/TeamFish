using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaterFollow : MonoBehaviour
{
    [Header("Water location")]
    public Transform _target;
    NavMeshAgent nav;

    public float _smoothSpeed = 10f;

    public Vector3 offset;

    [Header("Destruction")]
    public PlantReaction _plantDestructionScript;
    public WaterAbility _waterLifeScript;
    public bool _hitObject;
    public bool _hitPlant;

    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        _target = GameObject.Find("/Characters/MC/Temporary MC Object/Ability Position").transform;
    }

    private void LateUpdate()
    {
        Debug.Log(_target.position);
        nav.SetDestination(_target.position);
        //transform.position = _target;
    }

    private void OnCollisionEnter(Collision collidor)
    {
        _hitObject = true;
        Debug.Log("I hit something");

        if(collidor.gameObject.tag == "plant hitpoint")
        {
            //Make plant react
            _hitPlant = true;
            _plantDestructionScript.plantReaction();
            Debug.Log("That was a plant");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaterFollow : MonoBehaviour
{
    private Animator _animator;

    [Header("Water location")]
    public Transform _target;
    //public Transform _newTarget;
    NavMeshAgent nav;

    public float _smoothSpeed = 10f;

    public Vector3 offset;

    [Header("Destruction")]
    public PlantReaction _plantDestructionScript;
    //public WaterAbility _waterLifeScript;
    //public bool _hitObject;
    //public bool _hitPlant;

    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _target = GameObject.Find("/Characters/MC/Ability Position").transform;
        //_newTarget = GameObject.Find("/Characters/MC/Throw position").transform;
    }

    private void Update()
    {
        nav.SetDestination(_target.position);
        //transform.position = _target.transform.position * Time.deltaTime;

        /*if (_waterLifeScript._waterThrown)
        {
            //nav.SetDestination(_newTarget.position);
            _animator.SetBool("canDrop", true);
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "plant hitpoint")
        {
            var plantScript = other.GetComponent<PlantReaction>();

            plantScript._plantIsHitWater = true;
        }
    }
}

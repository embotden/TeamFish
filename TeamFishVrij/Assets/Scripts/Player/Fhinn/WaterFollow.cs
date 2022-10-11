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
    //public bool _hitObject;
    //public bool _hitPlant;

    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        _target = GameObject.Find("/Characters/MC/Temporary MC Object/Ability Position").transform;
    }

    private void Update()
    {
        nav.SetDestination(_target.position);
        //transform.position = _target.transform.position * Time.deltaTime;
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

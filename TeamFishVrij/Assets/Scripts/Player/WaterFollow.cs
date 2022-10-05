using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaterFollow : MonoBehaviour
{
    public Transform _target;
    NavMeshAgent nav;

    public float _smoothSpeed = 10f;

    public Vector3 offset;

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


}

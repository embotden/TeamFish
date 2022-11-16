using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 

public class FhinnFollow : MonoBehaviour
{
    public Transform _targetLocation;
    NavMeshAgent _navMA;



    
    // Start is called before the first frame update
    void Awake()
    {
        _navMA = GetComponent<NavMeshAgent>();
        _targetLocation = GameObject.Find("/Characters/MC/Character Position").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //_navMA.Warp(_targetLocation.position);
        _navMA.SetDestination(_targetLocation.position);
    }
}

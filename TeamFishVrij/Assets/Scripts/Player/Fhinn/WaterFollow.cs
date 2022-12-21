using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Threading.Tasks;

public class WaterFollow : MonoBehaviour
{
    private Animator _animator;

    [Header("Water location")]
    public Transform _target;
    NavMeshAgent nav;

    public float _smoothSpeed = 10f;

    public Vector3 offset;

    [Header("Destruction")]
    public PlantReaction _plantDestructionScript;

    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _target = GameObject.Find("/Characters/MC/Ability Position").transform;

        //play grab animation
        _animator.SetBool("waterGrabbed", true);
    }

    private void Update()
    {
        nav.SetDestination(_target.position);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "plant hitpoint")
        {
            var plantScript = other.GetComponent<PlantReaction>();

            plantScript._plantIsHitWater = true;

            DestroyBall();
        }
    }

    public async void DestroyBall()
    {
        //check if exist
        if (!gameObject) return;
        
        //play destruction animation
        _animator.SetBool("waterGrabbed", false);
        _animator.SetBool("canBeDropped", true);

        //delay
        await Task.Delay(400);

        //Destroy(cloneWater);
        if(gameObject) Destroy(gameObject);
    }
}

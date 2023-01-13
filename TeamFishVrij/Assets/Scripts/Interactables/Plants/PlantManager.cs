using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    [Header("Plant state")]
    public float _damage;
    public float _revival;
    public GameObject _toxicWarning;

    [Header("Succes feedback")]
    public GameObject _succesFeedback;
    public GameObject _toxicParticles;
    public Animator _plantAnimation;
    //public Animator _volumeChange;
    public bool _plantMaxedOut;

    [Header("Animations")]
    public GameObject _puzzleColider;
    private GameObject _Steevin;


    private void Start()
    {
        _succesFeedback.SetActive(false);
        _puzzleColider.SetActive(true);
        
        _Steevin = GameObject.Find("/Characters/Shark/MOD_Steefin");
    }
    private void Update()
    {
        Animator _SteevinAnimator = _Steevin.GetComponent<Animator>();
    }

    public void PlantGrowing()
    {
        StartCoroutine(PlantMaxedOut());
    }

    private IEnumerator PlantMaxedOut()
    {
        Debug.Log("3");

        _plantAnimation.SetTrigger("maxedOut");
        //_volumeChange.SetTrigger("canLeave");
        
        _succesFeedback.SetActive(true);
        _toxicParticles.SetActive(false);

        _toxicWarning.SetActive(false);

        yield return new WaitForSeconds(2f);

        _puzzleColider.SetActive(false);
        _plantMaxedOut = true;
    }




}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    [Header("Plant state")]
    public float _damage;
    public float _revival;
    [SerializeField] private float _planteState;
    [SerializeField] private float _maxHealth = 10f;

    [Header("Succes feedback")]
    private float _waitingTime = 0.2f;
    //private ImageTrigger _imageInteraction;
    public GameObject _succesFeedback;
    public Material _toxicPlantMat;
    [SerializeField] private float _waterLevel;


    [SerializeField] private bool _succesFeedbackTriggered = false;

    [Header("Animations")]
    //public Animator _hangingPlant1;
    //public Animator _hangingPlant2;
    public GameObject _puzzleColider;
    //
    private GameObject _Steevin;


    private void Start()
    {
        _succesFeedback.SetActive(false);
        _puzzleColider.SetActive(true);
        //
        _Steevin = GameObject.Find("/Characters/Shark/MOD_Steefin");

        _waterLevel = 0.428f;

        _toxicPlantMat.SetFloat("AlphaClip", 0f);

    }
    private void Update()
    {
        Animator _SteevinAnimator = _Steevin.GetComponent<Animator>();

        //if (_planteState >= _maxHealth && !_succesFeedbackTriggered) Invoke("PlantMaxedOut", _waitingTime);

        /*if (_hangingPlant2.GetCurrentAnimatorStateInfo(0).IsName("Limp To Straight"))
        {
            _SteevinAnimator.SetBool("IsStuck", false);
        }

        else
        {
            _SteevinAnimator.SetBool("IsStuck", true);
        }*/
        

    }

    public void PlantGrowing()
    {
        _planteState += _revival;

        _toxicPlantMat.SetFloat("AlphaClip", 0.428f);
    }

    private IEnumerator PlantMaxedOut()
    {
        _succesFeedback.SetActive(true);
        _puzzleColider.SetActive(false);

        yield return new WaitForSeconds(2f);

        //_hangingPlant1.SetBool("isHit", true);
        //_hangingPlant2.SetBool("isHit", true);
        //_imageInteraction = _succesFeedback.GetComponent<ImageTrigger>();
        // _imageInteraction.StartCoroutine(_imageInteraction.StoryPainting());



        _succesFeedbackTriggered = true;
    }

    public void PlantShrinking()
    {
        _planteState -= _damage;
    }




}

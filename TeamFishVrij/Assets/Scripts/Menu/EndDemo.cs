using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndDemo : MonoBehaviour
{
    [Header("End Credits")]
    public Animator _endCredits;
    public Animator _crossfadeEnd;

    [Header("Cutscene Assets")]
    public Animator _HelpingSteevinUI;

    [Header("Inky")]
    [SerializeField] private TextAsset _inkJSON1;
    [SerializeField] private TextAsset _inkJSON2;


    [SerializeField] private float _crossfadeAnimationDuration = 0.5f;

    [Header("Player")]
    public PlayerController _characterMovements;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Fhinn")
        {
            //Invoke("EndGameDemo", 3f);

            Debug.Log("End Triggered");
            StartCoroutine(DemoEnding());
        }
    }

    public void EndGameDemo()
    {
        StartCoroutine(LoadNextLevel(SceneManager.GetActiveScene().buildIndex - 2));
    }

    private IEnumerator LoadNextLevel(int levelIndex)
    {
        //play animation
        _endCredits.Play("Crossfade_End");

        //wait
        yield return new WaitForSeconds(_crossfadeAnimationDuration);

        //Load scene
        SceneManager.LoadScene(levelIndex);
    }

    private IEnumerator DemoEnding()
    {
        //turn off player movement
        _characterMovements._canIMove = false;

        //wait for camera to finish rotating
        yield return new WaitForSeconds(2f);

        //start first part of dialogue
        DialogueManager.GetInstance().EnterDialogueMode(_inkJSON1);

        while (!DialogueManager.GetInstance()._isDialogueFinished)
        {
            yield return null;
        }

        //transition where Steevin gets "unstuck"
        _HelpingSteevinUI.Play("AN_HelpSteevin_Play");

        //positie switch characters

        yield return new WaitForSeconds(3f);

        //start second part of dialogue
        DialogueManager.GetInstance().EnterDialogueMode(_inkJSON2);

        while (!DialogueManager.GetInstance()._isDialogueFinished)
        {
            yield return null;
        }

        //start end credits
        _endCredits.SetTrigger("canStart");

        //wait for end credits to finish
        yield return new WaitForSeconds(79f);

        EndGameDemo();

    }

}

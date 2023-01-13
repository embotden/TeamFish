using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndDemo : MonoBehaviour
{
    public Animator _transition;
   

    [SerializeField] private float _animationDuration = 0.5f;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Fhinn")
        {
            Invoke("EndGameDemo", 3f);

            Debug.Log("End Triggered");
        }
    }

    public void EndGameDemo()
    {
        StartCoroutine(LoadNextLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    private IEnumerator LoadNextLevel(int levelIndex)
    {
        //play animation
        _transition.Play("Crossfade_End");

        //wait
        yield return new WaitForSeconds(_animationDuration);

        //Load scene
        SceneManager.LoadScene(levelIndex);
    }

}

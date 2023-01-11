using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    private Animator animator;
    

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(BlinkAndWait(Random.Range(4, 10)));
    }


    public IEnumerator BlinkAndWait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        animator.SetBool("IsBlinking", true);
        yield return new WaitForSeconds(1);
        animator.SetBool("IsBlinking", false);
        StartCoroutine(BlinkAndWait(Random.Range(4, 10)));
    }            
    
  
}

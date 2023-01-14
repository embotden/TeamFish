using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toxicWarning : MonoBehaviour
{
    [SerializeField] private TextAsset _inkJSON;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fhinn"))
        {
            StartCoroutine(Dialogue());
        }
    }

    private IEnumerator Dialogue()
    {
        DialogueManager.GetInstance().EnterDialogueMode(_inkJSON);

        while (!DialogueManager.GetInstance()._isDialogueFinished)
        {
            yield return null;
        }

        yield return new WaitForSeconds(3f);
    }
}

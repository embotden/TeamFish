using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPlantState : MonoBehaviour
{
    public PlantManager _stateToCheck;

    private void Update()
    {
        if (_stateToCheck._plantMaxedOut) Destroy(gameObject);
    }
}

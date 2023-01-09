using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DirectionalLightShader : MonoBehaviour
{

    void Update()
    {
        Shader.SetGlobalVector("_LightDirectionVec", -transform.forward);

    }
}


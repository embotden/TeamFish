using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTrail : MonoBehaviour
{

    //private bool _isTrailActive;

    [Header("mesh related")]
    public float _meshRefreshRate = 0.1f;
    public float _meshDestroyDelay = 3f;
    public Transform _positionToSpawn;

    [Header("Shader related")]
    public Material _mat;
    public string _shaderVarRef;
    public float _shaderVarRate = 0.1f;
    public float _shaderVarRefreshRate = 0.5f;

    public float _activeTime = 5f;
    private SkinnedMeshRenderer[] _skinnedMeshRenderers;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            //_isTrailActive = true;
            StartCoroutine(ActivateTrail(_activeTime));
        }
    }

    IEnumerator ActivateTrail(float timeActive)
    {
        while(timeActive > 0)
        {
            timeActive -= _meshRefreshRate;

            if (_skinnedMeshRenderers == null) _skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

            for(int i=0; i< _skinnedMeshRenderers.Length; i++)
            {
                GameObject _gObj = new GameObject();
                _gObj.transform.SetPositionAndRotation(_positionToSpawn.position, _positionToSpawn.rotation);

                MeshRenderer _mr = _gObj.AddComponent<MeshRenderer>();
                MeshFilter _mf = _gObj.AddComponent<MeshFilter>();

                Mesh _mesh = new Mesh();
                _skinnedMeshRenderers[i].BakeMesh(_mesh);

                _mf.mesh = _mesh;
                _mr.material = _mat;

                StartCoroutine(AnimateMaterialFloat(_mr.material, 0, _shaderVarRate, _shaderVarRefreshRate));

                Destroy(_gObj, _meshDestroyDelay);
            }

            yield return new WaitForSeconds(_meshRefreshRate);

        }

        //_isTrailActive = false;
    }

    IEnumerator AnimateMaterialFloat(Material mat, float goal, float rate, float refreshRate)
    {
        float valueToAnimate = mat.GetFloat(_shaderVarRef);

        while(valueToAnimate > goal)
        {
            valueToAnimate -= rate;

            mat.SetFloat(_shaderVarRef, valueToAnimate);

            yield return new WaitForSeconds(refreshRate);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFollow : MonoBehaviour
{
    public Transform _target;

    public float _smoothSpeed = 10f;

    public Vector3 offset;

    private void LateUpdate()
    {
        Vector3 desiredPosition = _target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }


}

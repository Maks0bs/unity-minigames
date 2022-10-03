using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateConstantly : MonoBehaviour
{
    [SerializeField] Vector3 rotateDirection;

    private void Update()
    {
        transform.Rotate(rotateDirection * Time.deltaTime);
    }
}

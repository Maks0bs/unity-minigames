using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunLightController : MonoBehaviour
{
    [SerializeField] public float rotationSpeed = 1.0f;
    private void Update()
    {
        transform.Rotate(Vector3.right * Time.deltaTime * rotationSpeed);
    }
}

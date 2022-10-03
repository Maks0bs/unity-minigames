using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] public GameObject player;
    [SerializeField] public Vector3 offset;
    void Update()
    {
        transform.position = player.transform.position + offset;
    }
}

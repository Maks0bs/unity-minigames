using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrightnessManager : MonoBehaviour
{
    SpriteRenderer sr;

    private void Awake() => sr = GetComponent<SpriteRenderer>();

    void Update()
    {
        Color color = sr.color;
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            color.a = Mathf.Min(color.a + 0.05F, 1F);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            color.a = Mathf.Max(color.a - 0.05F, 0F);
        }
        sr.color = color;
    }
}

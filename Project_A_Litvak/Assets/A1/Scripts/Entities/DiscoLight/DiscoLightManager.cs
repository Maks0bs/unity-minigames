using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoLightManager : MonoBehaviour
{
    SpriteRenderer sr;
    public Coroutine routine;
    private void Awake() => sr = GetComponent<SpriteRenderer>();

    void Start()
    {
        routine = StartCoroutine(ChangeColor());
    }

    IEnumerator ChangeColor()
    {
        while (true)
        {
            Color newColor = new Color(
                Random.Range(0f, 1f),
                Random.Range(0f, 1f),
                Random.Range(0f, 1f)
            );
            float waitTime = Random.Range(2f, 3f);
            sr.color = newColor;
            yield return new WaitForSeconds(waitTime);
        }
    }

    public void StopChangeColor()
    {
        if (routine != null)
        {
            StopCoroutine(routine);
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingHintMovement : MonoBehaviour
{
    SpriteRenderer sr;
    public Coroutine routine;
    float timer = 0.0f;
    private void Awake() => sr = GetComponent<SpriteRenderer>();

    private void Start()
    {
        routine = StartCoroutine(DanceRotation());
    }

    private void OnDestroy()
    {
        if (routine != null)
        {
            StopCoroutine(routine);
        }
    }

    IEnumerator DanceRotation()
    {
        float originalScale = transform.localScale.x;
        float currentScale = originalScale;
        float scaleMultiplier = 1.0f;
        while (true)
        {
            int timerStep = (int) timer;
            float rotateMultiplier = (timerStep % 2 == 0) ? 1.0f : -1.0f;

            transform.Rotate(0.05f * transform.forward * rotateMultiplier);
            timer += Time.deltaTime;

            currentScale += 0.01f * scaleMultiplier;
            if ((currentScale >= originalScale * 2f) || (currentScale <= originalScale))
            {
                scaleMultiplier = -scaleMultiplier;
            }
            transform.localScale = currentScale * Vector3.one;
            
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DanceableEntity : MonoBehaviour
{
    protected bool isDancing = false;

    public static float DANCE_DURATION_SECS = 1.0f;

    float timer = 0.0f;

    Coroutine danceCoroutine;

    abstract protected void DoBeforeDance();
    abstract protected void DoAfterDance();
    abstract protected float GetDanceSpeed();
    abstract protected SpriteRenderer GetSpriteRenderer();

    public enum DanceType { ShortMovement, Rotation, SizeChange}

    void ToggleDancing()
    {
        isDancing = !isDancing;
        if (isDancing)
        {
            timer = DANCE_DURATION_SECS;
        }
        else
        {
            timer = 0.0f;
        }
    }

    protected void Dance(DanceType danceType)
    {
        DoBeforeDance();
        ToggleDancing();
        switch (danceType)
        {
            case DanceType.ShortMovement:
                danceCoroutine = StartCoroutine(DanceShortMovement());
                break;
            case DanceType.Rotation:
                danceCoroutine = StartCoroutine(DanceRotation());
                break;
            case DanceType.SizeChange:
                danceCoroutine = StartCoroutine(DanceSizeChange());
                break;

        }
    }

    protected void StopDance()
    {
        StopCoroutine(danceCoroutine);
        ToggleDancing();
        DoAfterDance();
    }

    IEnumerator DanceShortMovement()
    {
        float speed = GetDanceSpeed();
        SpriteRenderer sr = GetSpriteRenderer();
        while (true)
        {
            if ((timer >= 0.75f * DANCE_DURATION_SECS) || (timer < 0.25f * DANCE_DURATION_SECS))
            {
                transform.position += speed * Time.deltaTime * sr.transform.right;
            }
            else
            {
                transform.position += speed * Time.deltaTime * -sr.transform.right;
            }

            timer -= Time.deltaTime;

            if (timer <= 0.0f)
            {
                StopDance();
            }

            yield return null;
        }
    }

    IEnumerator DanceRotation()
    {
        float speed = GetDanceSpeed();
        while (true)
        {
            float angle = Random.Range(0.0f, 1.0f);
           
            GetSpriteRenderer().transform.Rotate(angle * speed * Vector3.forward);

            timer -= Time.deltaTime;

            if (timer <= 0.0f)
            {
                StopDance();
            }

            yield return null;
        }
    }

    IEnumerator DanceSizeChange()
    {
        float speed = GetDanceSpeed();
        SpriteRenderer sr = GetSpriteRenderer();
        while (true)
        {
            bool block1 = (timer >= 0.75f * DANCE_DURATION_SECS);
            bool block2 = (timer < 0.75f * DANCE_DURATION_SECS) && (timer >= 0.5f * DANCE_DURATION_SECS);
            bool block3 = (timer < 0.5f * DANCE_DURATION_SECS) && (timer >= 0.25f * DANCE_DURATION_SECS);
            bool block4 = (timer < 0.25f * DANCE_DURATION_SECS);
            if (block1 || block3)
            {
                sr.transform.localScale += speed * Time.deltaTime * Vector3.one * 0.2f;
            }
            else
            {
                sr.transform.localScale -= speed * Time.deltaTime * Vector3.one * 0.2f;
            }

            timer -= Time.deltaTime;

            if (timer <= 0.0f)
            {
                StopDance();
                sr.transform.localScale = Vector3.one;
            }

            yield return null;
        }
    }
}

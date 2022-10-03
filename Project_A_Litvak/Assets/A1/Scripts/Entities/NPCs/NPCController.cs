using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : DanceableEntity
{
    SpriteRenderer sr;
    CheatCodeActivator cheatCodeActivator;

    private void Awake() => sr = GetComponent<SpriteRenderer>();

    protected override float GetDanceSpeed()
    {
        return 5f;
    }

    protected override SpriteRenderer GetSpriteRenderer()
    {
        return sr;
    }

    protected override void DoBeforeDance()
    {
       
    }

    void DestroyGameObject()
    {
        Destroy(gameObject);
    }
    protected override void DoAfterDance()
    {

    }

    private void Start()
    {
        float waitTime = Random.Range(0.5f, 4f);
        cheatCodeActivator = GameObject.FindGameObjectWithTag("cheatcode").GetComponent<CheatCodeActivator>();
        Invoke(nameof(StartRoutine), waitTime);
    }

    void StartRoutine()
    {
        StartCoroutine(DanceContinuously());
    }


    IEnumerator DanceContinuously()
    {
        while (true)
        {
            DanceType danceType = (DanceType)Random.Range(0, 3);
            float waitTime = Random.Range(DANCE_DURATION_SECS + 0.5f, DANCE_DURATION_SECS + 4f);
            Dance(danceType);
            yield return new WaitForSeconds(waitTime);
        }
    }

    private void Update()
    {
        var isActive = cheatCodeActivator.isSquidgameActive;
        var isTurnRed =
            cheatCodeActivator.squidgameTurn == CheatCodeActivator.SquidgameTurn.Red;
        if (isDancing && isActive && isTurnRed)
        {
            Invoke(nameof(DestroyGameObject), 1f);
        }
    }

}

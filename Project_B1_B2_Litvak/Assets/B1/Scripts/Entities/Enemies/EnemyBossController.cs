using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossController : EnemyController
{

    [SerializeField] protected List<GameObject> specialLasers;
    [SerializeField] protected AudioClip specialShootSound;

    protected new void Start()
    {
        enemySpeed = 6f;
        healthPoints = 15;
        base.Start();
        StartCoroutine(ShootSpecial());
    }


    protected IEnumerator ShootSpecial()
    {
        yield return new WaitForSeconds(3.0f);
        while (true)
        {
            GameObject laser = specialLasers[Random.Range(0, specialLasers.Count)];
            Vector2 pos = rb.position + new Vector2(sr.bounds.size.x * 0.5f, 0f);
            Instantiate(laser, pos, Quaternion.Euler(0, 0, 90f));
            audioSource.PlayOneShot(specialShootSound, 0.35f);
            yield return new WaitForSeconds(4.0f);
        }

    }
}

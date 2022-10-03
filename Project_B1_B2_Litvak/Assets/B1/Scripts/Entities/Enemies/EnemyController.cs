using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    protected float enemySpeed = 7.5f;
    public float moveDirection = 1.0f;
    protected int healthPoints = 3;
    protected SpriteRenderer sr;
    [SerializeField] protected GameObject destroyParticle;
    [SerializeField] protected GameObject audioHolder;
    [SerializeField] protected AudioClip hitSound;
    [SerializeField] protected AudioClip destroySound;
    [SerializeField] protected AudioClip enemyShootSound;
    [SerializeField] protected GameObject enemyLaser;
    protected AudioSource audioSource;

    protected Rigidbody2D rb;
    protected void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    protected void Start()
    {
        StartCoroutine(ShootLaser());
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            moveDirection = -moveDirection;
        }

    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerProjectile"))
        {
            healthPoints -= 1;
            Destroy(collision.gameObject);
            if (healthPoints <= 0)
            {
                GameObject ah = Instantiate(audioHolder);
                AudioSource audioSourceTemp = ah.GetComponent<AudioSource>();
                audioSourceTemp.PlayOneShot(destroySound, 0.5f);
                Instantiate(destroyParticle, rb.position, Quaternion.identity);
                GameObject gc = GameObject.Find("GameController");
                gc.GetComponent<SpaceGameController>().score += 1;
                Destroy(gameObject);
            }
            else
            {
                audioSource.PlayOneShot(hitSound, 0.5f);
            }
        }
    }


    protected IEnumerator ShootLaser()
    {
        yield return new WaitForSeconds(1.0f);
        while (true)
        {
            Vector2 pos = rb.position + new Vector2(sr.bounds.size.x * 0.5f, 0f);
            Instantiate(enemyLaser, pos, Quaternion.Euler(0, 0, 90f));
            audioSource.PlayOneShot(enemyShootSound, 0.25f);
            yield return new WaitForSeconds(2.5f);
        }
        
    }

    protected void Update()
    {
        Vector2 dir = Vector2.up * moveDirection * Time.deltaTime * enemySpeed;
        rb.MovePosition(rb.position + dir);
    }
}

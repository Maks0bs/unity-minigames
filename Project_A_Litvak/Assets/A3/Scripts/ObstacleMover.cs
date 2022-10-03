using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    protected Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ObstacleBorder"))
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = Vector2.left * 2f;
    }
}

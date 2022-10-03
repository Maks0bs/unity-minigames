using UnityEngine;

public class BasicLaserController : MonoBehaviour
{
    protected float PROJECTILE_SPEED = 7.5f;

    protected Rigidbody2D rb;
    protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(PROJECTILE_SPEED, 0);
    }

    protected void DestroyOutScreenObject(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            Destroy(rb.gameObject);
            return;
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        DestroyOutScreenObject(collision);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpecialLaserController1 : PlayerLaserController
{
    public float sinAngle = 0.0f;
    public float amplitude = 0.35f;
    public float startingY = 0.0f;
    public float speed = 4.0f;

    protected new void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        startingY = rb.position.y;
    }

    private void Update()
    {
        sinAngle += Time.deltaTime * 2f;
        float sinus = Mathf.Sin(sinAngle);
        float y = startingY + amplitude * Mathf.Sin(sinAngle);
        Vector2 pos = new Vector2(rb.position.x - Time.deltaTime * speed, y);
        rb.MovePosition(pos);
        rb.gameObject.transform.localScale = 2f * (2 - sinus) * Vector3.one;
        rb.rotation += Time.deltaTime * 50.0f;
    }

}

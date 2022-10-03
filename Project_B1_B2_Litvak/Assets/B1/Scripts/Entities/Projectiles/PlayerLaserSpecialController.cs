using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaserSpecialController : PlayerLaserController
{
    public float sinAngle = 0.0f;
    public float amplitude = 1.0f;
    public float startingY = 0.0f;
    public float speed = 1.0f;

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
        float y = startingY + amplitude * Mathf.Sin(sinAngle);
        Vector2 pos = new Vector2(rb.position.x + Time.deltaTime * speed, y);
        rb.MovePosition(pos);
    }

}

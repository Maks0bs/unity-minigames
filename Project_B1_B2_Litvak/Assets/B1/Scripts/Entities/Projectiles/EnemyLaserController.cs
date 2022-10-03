using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserController : BasicLaserController
{
    protected new void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-base.PROJECTILE_SPEED, 0);
    }
}

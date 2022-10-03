using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpecialLaserController2 : PlayerLaserController
{

    protected new void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector3 v = rb.transform.localScale;
        rb.transform.localScale = new Vector3(3f, 10f, v.z);
    }

    private void Update()
    {
        Vector2 pos = new Vector2(
            rb.position.x - Time.deltaTime * 7.5f, 
            rb.position.y + Random.Range(-0.1f, 0.1f)
        );
        rb.MovePosition(pos);
    }

}

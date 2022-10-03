using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMoverAdvanced : ObstacleMover
{
    float timer = 0.0f;
    bool canMove = true;

    void FixedUpdate()
    {
        if (canMove)
        {
            rb.velocity = Vector2.left * 2f;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
        
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer < 0.25f)
        {
            canMove = false;
        }
        else
        {
            canMove = true;
        }

        if (timer > 2f)
        {
            timer -= 2f;
        }
    }
}

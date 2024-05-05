using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainPlayer : Player
{
    void Update()
    {
        PlayerInput.x = Input.GetAxisRaw("Horizontal");
        PlayerInput.y = Input.GetAxisRaw("Vertical");

        Movement();
        Flip();
    }

    void Flip()
    {
        PointerPosition = mousePos;
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = (mousePos - (Vector2)transform.position).normalized;
        Vector2 scale = transform.localScale;

        if (direction.x < 0 /*&& pauseMenu.isPaused == false*/)
        {
            spriteRenderer.flipX = true;
        }
        else if (direction.x > 0 /*&& pauseMenu.isPaused == false*/)
        {
            spriteRenderer.flipX = false;
        }
        transform.localScale = scale;
    }

    void Movement()
    {
        Vector2 moveForce = PlayerInput * moveSpeed;
        moveForce += forceToApply;
        forceToApply /= forceDamping;
        if (Mathf.Abs(forceToApply.x) <= 0.01f && Mathf.Abs(forceToApply.y) <= 0.01f)
        {
            forceToApply = Vector2.zero;
        }
        rb.velocity = moveForce;

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;
        //rb.rotation = angle;
    }
}

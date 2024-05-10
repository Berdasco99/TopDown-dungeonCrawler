using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [Header("UI")]
    public PauseMenu pauseMenu;
    public HpBar hp;
    public Camera cam;

    [Header("JUGADOR")]
    public GameObject weaponParent;

    public bool hasRanged = false, hasMelee = false;
    
    [Header("VECTORES")]
    public Vector2 forceToApply;
    public Vector2 playerInput;
    public Vector2 moveForce;
    public Vector2 mousePos;
    public Vector2 PointerPosition { get; set; }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
           float damageTaken = collision.gameObject.GetComponent<Enemy>().damage;

            if (currentHealth > 0)
            {
                currentHealth -= damageTaken;
            }
        }
    }

    public void MeleeTrue()
    {
        hasMelee = true;
        hasRanged = false;
    }

    public void RangedTrue()
    {
        hasRanged = true;
        hasMelee = false;
    }

    protected virtual void PlayerFunctions()
    {
        playerInput.x = Input.GetAxisRaw("Horizontal");
        playerInput.y = Input.GetAxisRaw("Vertical");
        Flip();
        Movement();
        AnimationHandling();
        HealthCap();
    }

    protected virtual void Flip()
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

    protected virtual void Movement()
    {
        playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        moveForce = playerInput * moveSpeed;
        moveForce += forceToApply;
        forceToApply /= forceDamping;
        if (Mathf.Abs(forceToApply.x) <= 0.01f && Mathf.Abs(forceToApply.y) <= 0.01f)
        {
            forceToApply = Vector2.zero;
        }
        rb.velocity = moveForce;

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;
    }

    protected virtual void AnimationHandling()
    {
        if(playerInput.y != 0 || playerInput.x != 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}

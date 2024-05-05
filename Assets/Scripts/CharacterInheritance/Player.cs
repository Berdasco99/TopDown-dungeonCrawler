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
    public Vector2 PlayerInput;
    public Vector2 mousePos;
    public Vector2 PointerPosition { get; set; }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            maxHealth = currentHealth - 1;
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

    public void PlayerFunctions()
    {
        PlayerInput.x = Input.GetAxisRaw("Horizontal");
        PlayerInput.y = Input.GetAxisRaw("Vertical");
        Flip();
        Movement();
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

    //Crear metodo DASH(){};
}

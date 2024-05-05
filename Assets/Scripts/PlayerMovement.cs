using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{

    [Header("Jugador")]
    public SpriteRenderer playerSprite;
    public GameObject weaponParent;
    public Rigidbody2D rb;
    //I recommend 7 for the move speed, and 1.2 for the force damping
    public float moveSpeed;
    public float forceDamping;

    [Header("Canvas")]
    [SerializeField] PauseMenu pauseMenu;

    public Vector2 forceToApply;
    public Vector2 PlayerInput;
    Vector2 mousePos;
    public Camera cam;
    public Animator animator;

    public Vector2 PointerPosition { get; set; }

    void Update()
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
            playerSprite.flipX = true;
        }
        else if (direction.x > 0 /*&& pauseMenu.isPaused == false*/)
        {
            playerSprite.flipX = false;
        }
        transform.localScale = scale;
    }
}

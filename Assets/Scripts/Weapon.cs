using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Herencia")]
    public Sprite sprite;
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer characterRenderer;
    public Transform point;
    public GameObject weapon;
    public int damage;
    public Animator animator;
    public Animator playerAnimator;
    public Camera cam;
    protected Vector2 direction;
    public Vector2 mousePos;
    public Vector2 PointerPosition { get; set; }
    public Player playerScript;
    //[SerializeField] PauseMenu pauseMenu;
    public Transform weaponParent;

    public void AimLogic()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        PointerPosition = mousePos;

        Vector2 direction = (PointerPosition - (Vector2)transform.position).normalized;
        transform.right = direction;
        Vector2 scale = transform.localScale;

        if (direction.x < 0)
        {
            spriteRenderer.flipY = true;
        }
        else if (direction.x > 0)
        {
            spriteRenderer.flipY = false;
        }
        transform.localScale = scale;

        if (transform.eulerAngles.z > 0 && transform.eulerAngles.z < 180)
        {
            spriteRenderer.sortingOrder = characterRenderer.sortingOrder - 1;
        }
        else
        {
            spriteRenderer.sortingOrder = characterRenderer.sortingOrder + 1;
        }
    }
}

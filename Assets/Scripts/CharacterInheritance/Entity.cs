using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [Header("ENTITY CLASS")]
    [Header("ATRIBUTOS")]
    public int maxHealth;
    public int currentHealth;
    public int minHealth = 0;
    public int moveSpeed;
    public float forceDamping;
    [Header("FISICAS")]
    public Rigidbody2D rb;
    public BoxCollider2D boxCollider;
    [Header("VISUAL")]
    public Animator animator;
    public SpriteRenderer spriteRenderer;
}

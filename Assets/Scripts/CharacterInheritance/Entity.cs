using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [Header("ENTITY CLASS")]
    [Header("ATRIBUTOS")]
    public float maxHealth;
    public float currentHealth;
    [HideInInspector] public float minHealth = 0f;
    public int moveSpeed;
    public float forceDamping;
    [Header("FISICAS")]
    public Rigidbody2D rb;
    public BoxCollider2D boxCollider;
    [Header("VISUAL")]
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        currentHealth = maxHealth;
    }
}

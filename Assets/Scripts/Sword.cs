using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    [Header("Propias")]
    public float delay = 0.3f;
    [SerializeField] float attackRange = 0.5f;
    public LayerMask hitLayers;

    private void Start()
    {
        sprite = null;
    }
    void Update()
    {
        if (playerScript.hasMelee)
        {
            AimLogic();
            weapon.SetActive(true);
            
            SwordLogic();
        }
        else if (!playerScript.hasMelee)
        {
            
            weapon.SetActive(false);
        }
    }

    private void SwordLogic()
    {
        if (attackBlock)
        {
            return;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Swing();
        }
    }

    public void Swing()
    {
        if (attackBlock)
        {
            return;
        }
        animator.SetTrigger("SwordAttack");
        attackBlock = true;
        StartCoroutine(DelayAttack());

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(point.position, attackRange, hitLayers);

        foreach (Collider2D entity in hitEnemies)
        {
            entity.GetComponent<Enemy>().damageTaken = damage;
            //Falta hacer que se rompan los jarrones
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (point == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(point.position, attackRange);
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);
        attackBlock = false;
    }
}

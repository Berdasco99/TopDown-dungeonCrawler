using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    public float delay = 0.3f;
    private bool attackBlock;
    [SerializeField] float attackRange = 0.5f;

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
            
            //SwordLogic();
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

        //Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(swordPoint.position, attackRange, hitLayers);

        //foreach (Collider2D entity in hitEnemies)
        //{
        //    entity.GetComponent<ClaseEnemigo>().TakeDamage(25);
        //    //Falta hacer que se rompan los jarrones
        //}
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

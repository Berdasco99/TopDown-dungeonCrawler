using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    protected GameObject player;
    protected float distance;
    public float chaseDistance;
    public float damage;
    public float damageTaken;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            damageTaken = collision.gameObject.GetComponent<Projectile>().damage;

            if (currentHealth > 0)
            {
                //Damage taken animation
                currentHealth -= damageTaken;
            }
            if(currentHealth <= 0)
            {
                //Death animation
                Destroy(gameObject);
            }
        }
    }

    protected void EnemyFunctions()
    {
        MoveTowards();
        HealthCap();
    }

    protected void MoveTowards()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);

        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (distance < chaseDistance)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, moveSpeed * Time.deltaTime);

            if (angle >= 90 && angle <= 180 || angle <= -90 && angle >= -180)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }
    }
}

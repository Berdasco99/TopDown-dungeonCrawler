using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] HpBar hp;
    public bool hasRanged = false, hasMelee = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            hp.health = hp.health - 1;
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
}

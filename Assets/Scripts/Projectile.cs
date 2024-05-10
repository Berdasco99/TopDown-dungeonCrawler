using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter2D(Collider2D collider)
    {

        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public float damage;
    [SerializeField] Weapon weapon;

    private void Start()
    {
        weapon = GameObject.Find("WeaponParent").GetComponent<Weapon>();
    }
    private void Update()
    {
        damage = weapon.damage;
        Debug.Log(damage);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Destroy(gameObject);
    }
}

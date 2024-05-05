using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bow : Weapon
{
    [Header("Propias")]
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] float arrowForce = 20f;
    void Update()
    {
        if (playerScript.hasRanged)
        {
            AimLogic();
            weapon.SetActive(true);
            BowLogic();

        }
        else if (!playerScript.hasRanged)
        {
            weapon.SetActive(false);
        }
    }


    void BowLogic()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject arrow = Instantiate(arrowPrefab, point.position, weaponParent.rotation);
        //arrow.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(aimPos.y, aimPos.x) * Mathf.Deg2Rad);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.AddForce(point.right * arrowForce, ForceMode2D.Impulse);
        Destroy(arrow, 2f);
    }
}
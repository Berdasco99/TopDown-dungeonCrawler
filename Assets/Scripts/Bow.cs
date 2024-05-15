using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class Bow : Weapon
{
    [Header("Propias")]
    public float delay = 0.3f;
    float time;
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] float arrowForce = 20f;

    void Update()
    {
        if (attackBlock)
        {
            reloadBar.fillAmount += Time.deltaTime / delay;
        }

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
        if (attackBlock)
        {
            return;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (attackBlock)
        {
            return;
        }

        attackBlock = true;
        StartCoroutine(DelayAttack());

        GameObject arrow = Instantiate(arrowPrefab, point.position, weaponParent.rotation);
        //arrow.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(aimPos.y, aimPos.x) * Mathf.Deg2Rad);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.AddForce(point.right * arrowForce, ForceMode2D.Impulse);
        Destroy(arrow, 2f);
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);
        attackBlock = false;
        reloadBar.fillAmount = 0f;
    }
}
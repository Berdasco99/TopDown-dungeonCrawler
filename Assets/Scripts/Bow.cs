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
        GameObject arrow = Instantiate(arrowPrefab, point.position, point.rotation);
        //El firepoint.rotation le da el valor del Quaternion 0,0,0 pero cuando apunto a la derecha tiene que ser
        //-90 y a la izquierda 90 en la Z o si no dispara para arriba ni puta idea de porque\

        //SOLUCION
        //El problema era que para girar el arma hacia scale.y = 0.7f; o -0,7f, esto hace que literalmente se gire sobre si misma
        //el arma y se invierta todo, el angulo del firepoint tambien, asi que la solucion es usar Flip que simplemente
        //gira el sprite en vez de girar todo
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.AddForce(point.up * arrowForce, ForceMode2D.Impulse);
    }
}
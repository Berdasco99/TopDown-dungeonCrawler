using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.CompareTag("Enemy"))
        {
            Debug.Log("Ah, que dolor!");
            Destroy(gameObject);
        }
    }
}

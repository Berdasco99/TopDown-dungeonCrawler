using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    Camera cam;
    Player player;
    public Vector2 coordinates;
    public Vector2 playerCoordinates;

    void Start()
    {
        cam = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.CompareTag("Player"))
        {
            MoveCamera();
            player.transform.Translate(playerCoordinates);
        }

    }

    private void MoveCamera()
    {
        cam.transform.Translate(coordinates,0);
    }
}

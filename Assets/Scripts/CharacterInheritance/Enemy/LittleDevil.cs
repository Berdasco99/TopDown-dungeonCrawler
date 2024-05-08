using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleDevil : Enemy
{
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");        
    }

    void Update()
    {
        EnemyFunctions();
    }
}

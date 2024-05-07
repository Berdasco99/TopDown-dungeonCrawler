using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawnPoint : MonoBehaviour
{
    public int openingDirection;
    // 1 --> Bottom door
    // 2 --> Top door
    // 3 --> Left door
    // 4 --> Right door

    private void Update()
    {
        if(openingDirection == 1)
        {
            // Need to spawn a room with a BOTTOM door.
        }
        else if(openingDirection == 2)
        {
            // Need to spawn a room with a TOP door.
        }
        else if (openingDirection == 3)
        {
            // Need to spawn a room with a LEFT door.
        }
        else if (openingDirection == 4)
        {
            // Need to spawn a room with a RIGHT door.
        }
    }
}

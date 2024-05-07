using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] GameObject topDoor;
    [SerializeField] GameObject bottomDoor;
    [SerializeField] GameObject leftDoor;
    [SerializeField] GameObject rightDoor;

    [SerializeField] GameObject topCameraCheck;
    [SerializeField] GameObject bottomCameraCheck;
    [SerializeField] GameObject leftCameraCheck;
    [SerializeField] GameObject rightCameraCheck;

    public Vector2Int RoomIndex {  get; set; }

    public void OpenDoor(Vector2Int direction)
    {
        if(direction == Vector2Int.up)
        {
            topDoor.SetActive(false);
            topCameraCheck.SetActive(true);
        }
        if (direction == Vector2Int.down)
        {
            bottomDoor.SetActive(false);
            bottomCameraCheck.SetActive(true);
        }
        if (direction == Vector2Int.left)
        {
            leftDoor.SetActive(false);
            leftCameraCheck.SetActive(true);
        }
        if (direction == Vector2Int.right)
        {
            rightDoor.SetActive(false);
            rightCameraCheck.SetActive(true);
        }
    }
    
}

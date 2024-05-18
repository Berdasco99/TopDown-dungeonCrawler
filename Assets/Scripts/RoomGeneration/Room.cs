using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Room : MonoBehaviour
{
    [Header("Doors")]
    [SerializeField] GameObject topDoor;
    [SerializeField] GameObject bottomDoor;
    [SerializeField] GameObject leftDoor;
    [SerializeField] GameObject rightDoor;

    [Header("Door Covers")]
    [SerializeField] GameObject topDoorCover;
    [SerializeField] GameObject bottomDoorCover;
    [SerializeField] GameObject leftDoorCover;
    [SerializeField] GameObject rightDoorCover;

    [Header("Enemies")]
    [SerializeField] GameObject parentEnemy;
    public GameObject[] allChildEnemiesArray;
    public List<GameObject> allChildEnemies;

    public Vector2Int RoomIndex {  get; set; }

    private void Start()
    {
        allChildEnemiesArray = new GameObject[parentEnemy.transform.childCount];
        allChildEnemies = new List<GameObject>();

        for (int i = 0; i < allChildEnemiesArray.Length; i++)
        {
            allChildEnemiesArray[i] = parentEnemy.transform.GetChild(i).gameObject;
        }

        allChildEnemies = allChildEnemiesArray.ToList();
    }
    private void Update()
    {
        allChildEnemies.RemoveAll(x => x == null);

        if(allChildEnemies.Count == 0)
        {
            topDoorCover.SetActive(false);
            bottomDoorCover.SetActive(false);
            leftDoorCover.SetActive(false);
            rightDoorCover.SetActive(false);
        }
    }

    public void OpenDoor(Vector2Int direction)
    {
        if(direction == Vector2Int.up)
        {
            topDoor.SetActive(false);
        }
        if (direction == Vector2Int.down)
        {
            bottomDoor.SetActive(false);
        }
        if (direction == Vector2Int.left)
        {
            leftDoor.SetActive(false);
        }
        if (direction == Vector2Int.right)
        {
            rightDoor.SetActive(false);
        }
    }
}

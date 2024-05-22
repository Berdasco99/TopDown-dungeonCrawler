using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] GameObject[] roomPrefab;
    [SerializeField] private int maxRooms = 15;
    [SerializeField] private int minRooms = 10;

    int i;

    int roomWidth = 20;
    int roomHeight = 12;

    int gridSizeX = 10;
    int gridSizeY = 10;


    private List<GameObject> roomObjects = new List<GameObject>();

    private Queue<Vector2Int> roomQueue = new Queue<Vector2Int>();

    private int[,] roomGrid;

    private int roomCount;

    private bool generationComplete = false;

    private bool bossRoomGenerated = false;

    private void Start()
    {
       roomGrid = new int[gridSizeX, gridSizeY];
       roomQueue = new Queue<Vector2Int>();

        Vector2Int initialRoomIndex = new Vector2Int(gridSizeX / 2, gridSizeY / 2);
        StartRoomGenerationFromRoom(initialRoomIndex);
    }

    private void Update()
    {
        if (roomQueue.Count > 0 && roomCount < maxRooms && !generationComplete) 
        {
            Vector2Int roomIndex = roomQueue.Dequeue();
            int gridX = roomIndex.x;
            int gridY = roomIndex.y;

            TryGenerateRoom(new Vector2Int(gridX - 1, gridY));
            TryGenerateRoom(new Vector2Int(gridX + 1, gridY));
            TryGenerateRoom(new Vector2Int(gridX, gridY + 1));
            TryGenerateRoom(new Vector2Int(gridX, gridY - 1));
        }else if (roomCount < minRooms || bossRoomGenerated == false)
        {
            Debug.Log($"Roomcount was less than the minimun amount of rooms or BossRoom wasn't generated. Trying again, {roomCount} was the count");
            RegenerateRooms();
        }
        else if (!generationComplete)
        {
            generationComplete = true;
            Debug.Log($"Generation succesfully completed!, {roomCount} rooms created");
        }

    }

    private bool TryGenerateRoom(Vector2Int roomIndex)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;

        if (x >= gridSizeX || y >= gridSizeY || x < 0 || y < 0)
        {
            return false;
        }
            
        if (roomGrid[x, y] != 0)
        {
            return false;
        }
           
        if (roomCount >= maxRooms)
        {
            return false;
        }
        if(UnityEngine.Random.value < 0.5f && roomIndex != Vector2Int.zero)
        {
            return false;
        }

        if(CountAdjacentRooms(roomIndex) > 1)
        {
            return false;
        }

        roomQueue.Enqueue(roomIndex);
        roomGrid[x, y] = 1;
        roomCount++;

        i = UnityEngine.Random.Range(1, roomPrefab.Length);//Permitimos que la bossroom se genere

        if (bossRoomGenerated)
        {
            i = UnityEngine.Random.Range(2, roomPrefab.Length - 1); //Para que esto funcione hacemos la bossroom sea siempre la ultima sala en la lista de salas, asi el -1 impide que salga elegida a la hora de generarse
        }

        var newRoom = Instantiate(roomPrefab[i], GetPositionFromGridIndex(roomIndex), Quaternion.identity);
        newRoom.GetComponent<Room>().RoomIndex = roomIndex;
        newRoom.name = $"Room-{roomCount}";
        roomObjects.Add(newRoom);
        OpenDoors(newRoom, x, y);

        if (newRoom.tag == "BossRoom" && bossRoomGenerated == false)
        {
            bossRoomGenerated = true;
        }

        return true;
    }

    void OpenDoors(GameObject room, int x, int y)
    {
        Room newRoomScript = room.GetComponent<Room>();

        //Neighbours
        Room leftRoomScript = GetRoomScriptAt(new Vector2(x - 1, y)); //LeftRoom
        Room rigthRoomScript = GetRoomScriptAt(new Vector2(x + 1, y)); //RightRoom
        Room topRoomScript = GetRoomScriptAt(new Vector2(x, y + 1)); //TopRoom
        Room bottomRoomScript = GetRoomScriptAt(new Vector2(x, y - 1)); //BottomRoom

        // Determine which doors to open based on the direction

        if (x > 0 && roomGrid[x - 1, y] != 0)
        {
            //Left neighbour
            newRoomScript.OpenDoor(Vector2Int.left);
            leftRoomScript.OpenDoor(Vector2Int.right);
        }
        if (x < gridSizeX - 1 && roomGrid[x + 1, y] != 0)
        {
            //Right neighbour
            newRoomScript.OpenDoor(Vector2Int.right);
            rigthRoomScript.OpenDoor(Vector2Int.left);
        }
        if (y > 0 && roomGrid[x, y - 1] != 0)
        {
            //Bottom neighbour
            newRoomScript.OpenDoor(Vector2Int.down);
            bottomRoomScript.OpenDoor(Vector2Int.up);
        }
        if (y < gridSizeY - 1 && roomGrid[x, y + 1] != 0)
        {
            //Top neighbour
            newRoomScript.OpenDoor(Vector2Int.up);
            topRoomScript.OpenDoor(Vector2Int.down);
        }
    }

    private Room GetRoomScriptAt(Vector2 index)
    {
        GameObject roomObject = roomObjects.Find(r => r.GetComponent<Room>().RoomIndex == index);

        if(roomObject != null)
        {
            return roomObject.GetComponent<Room>();
        }
        else
        {
            return null;
        }
    }

    //Clear all rooms and try again
    private void RegenerateRooms()
    {
        roomObjects.ForEach(Destroy);
        roomObjects.Clear();
        roomGrid = new int[gridSizeX, gridSizeY];
        roomQueue.Clear();
        roomCount = 0;
        generationComplete = false;
        bossRoomGenerated = false;

        Vector2Int initialRoomIndex = new Vector2Int(gridSizeX / 2, gridSizeY / 2);
        StartRoomGenerationFromRoom(initialRoomIndex);
        
    }

    private int CountAdjacentRooms(Vector2Int roomIndex)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;
        int count = 0;

        if(x > 0 && roomGrid[x - 1, y] != 0)
        {
            count++;//Left neighbour
        }
        if(x < gridSizeX - 1 && roomGrid[x + 1, y] != 0)
        {
            count++;//Right neighbour
        }
        if(y > 0 && roomGrid[x, y - 1] != 0)
        {
            count++;//Bottom neighbour
        }
        if(y < gridSizeY - 1 && roomGrid[x, y + 1] != 0)
        {
            count++;//Top neighbour
        }

        return count;


    }

    private void StartRoomGenerationFromRoom(Vector2Int roomIndex)
    {
        roomQueue.Enqueue(roomIndex);
        int x = roomIndex.x;
        int y = roomIndex.y;
        roomGrid[x, y] = 1;
        roomCount++;
        var initialRoom = Instantiate(roomPrefab[0], GetPositionFromGridIndex(roomIndex),Quaternion.identity);
        initialRoom.name = $"Room-{roomCount}";
        initialRoom.GetComponent<Room>().RoomIndex = roomIndex;
        roomObjects.Add(initialRoom);
    }

    private Vector3 GetPositionFromGridIndex(Vector2Int gridIndex)
    {
        int gridX = gridIndex.x;
        int gridY = gridIndex.y;

        //Obtenemos la posicion de la habitacion en funcion a donde spawnea en el grid
        return new Vector3(roomWidth * (gridX - gridSizeX / 2), roomHeight * (gridY - gridSizeY / 2));
    }

    private void OnDrawGizmos()
    {
        Color gizmoColor = new Color(0, 1, 1, 0.5f);
        Gizmos.color = gizmoColor;

        for(int x = 0; x < gridSizeX; x++)
        {
            for(int y = 0; y < gridSizeY; y++)
            {
                Vector3 position = GetPositionFromGridIndex(new Vector2Int(x, y));
                Gizmos.DrawWireCube(position, new Vector3(roomWidth, roomHeight, 1));
            }
        }
    }
}

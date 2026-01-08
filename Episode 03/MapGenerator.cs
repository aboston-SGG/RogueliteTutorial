using UnityEngine;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour
{
    public int mainPathLength = 8;
    public int extraBranches = 3;
    public int branchLength = 2;

    public int roomHeight;
    public int roomWidth;

    public GameObject roomPrefab;

    Dictionary<Vector2Int, Room> rooms = new();

    private Vector2Int[] dirs =
    {
        new Vector2Int(0, 1), // Up
        new Vector2Int(0, -1), // Down
        new Vector2Int(1, 0), // Right
        new Vector2Int(-1, 0) // Left
    };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        List<Room> map = GenerateMap();

        foreach(Room room in map)
        {
            GameObject newRoom = Instantiate(
                roomPrefab,
                new Vector3(room.gridPos.x * roomWidth, 0, room.gridPos.y * roomHeight),
                Quaternion.identity,
                transform);
        }
    }

    // Update room connections based on their positions
    private void UpdateConnections()
    {
        foreach(Room room in rooms.Values)
        {
            Vector2Int pos = room.gridPos;

            room.up = rooms.ContainsKey(pos + dirs[0]);
            room.down = rooms.ContainsKey(pos + dirs[1]);
            room.right = rooms.ContainsKey(pos + dirs[2]);
            room.left = rooms.ContainsKey(pos + dirs[3]);
        }
    }

    // Get a random free cell adjacent to the given position
    private Vector2Int GetNextFreeCellNear(Vector2Int from)
    {
        List<Vector2Int> possible = new();

        foreach(Vector2Int dir in dirs)
        {
            Vector2Int newPos = from + dir;

            if (!rooms.ContainsKey(newPos))
            {
                possible.Add(newPos);
            }
        }

        if (possible.Count == 0)
        {
            return from + dirs[Random.Range(0, dirs.Length)];
        }

        return possible[Random.Range(0, possible.Count)];
    }

    // Generate the map with rooms and connections
    public List<Room> GenerateMap()
    {
        rooms.Clear();

        Vector2Int currentPos = Vector2Int.zero;
        rooms[currentPos] = new Room(currentPos, RoomType.Start);

        for(int i = 0; i < mainPathLength; i++)
        {
            currentPos = GetNextFreeCellNear(currentPos);
            rooms[currentPos] = new Room(currentPos);

            if(i == mainPathLength - 1)
            {
                rooms[currentPos].type = RoomType.Boss;
            }
        }

        for(int b = 0; b < extraBranches; b++)
        {
            List<Vector2Int> mainPathPositions = new(rooms.Keys);
            Vector2Int branchStart = mainPathPositions[Random.Range(0, mainPathPositions.Count - 1)];

            Vector2Int pos = branchStart;

            for(int i = 0; i < branchLength; i++)
            {
                pos = GetNextFreeCellNear(pos);

                if (!rooms.ContainsKey(pos))
                {
                    rooms[pos] = new Room(pos);
                }
            }
        }

        UpdateConnections();

        return new List<Room>(rooms.Values);
    }
}

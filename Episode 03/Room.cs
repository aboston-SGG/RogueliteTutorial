using UnityEngine;

public enum RoomType
{
    Normal,
    Start,
    Boss,
    Treasure,
    Shop
}

[System.Serializable]
public class Room
{
    public Vector2Int gridPos;
    public RoomType type;
    public bool up, down, left, right;

    // Constructor
    public Room(Vector2Int pos, RoomType type = RoomType.Normal)
    {
        gridPos = pos;
        this.type = type;
    }
}

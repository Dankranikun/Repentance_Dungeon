using UnityEngine;

[System.Serializable]
public class RoomData
{
    public GameObject roomPrefab;
    public RoomType roomType;
}

public enum RoomType
{
    Normal,
    Boss,
    Treasure,
    Shop
}

public enum DoorDirection
{
    North,
    South,
    East,
    West
}
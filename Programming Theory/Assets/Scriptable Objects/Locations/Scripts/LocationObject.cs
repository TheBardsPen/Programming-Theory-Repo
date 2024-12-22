using UnityEngine;
using System.Collections.Generic;

public enum LocationType
{
    Town,
    Dungeon
}

public abstract class LocationObject : ScriptableObject
{
    public GameObject prefabButton;
    public LocationType type;
    [TextArea(15, 20)]
    public string description;
}

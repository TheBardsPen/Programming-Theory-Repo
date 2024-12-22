using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Town Object", menuName = "Locations/Town")]
public class TownObject : LocationObject
{
    public List<string> subLocations;

    private void Awake()
    {
        type = LocationType.Town;
    }
}

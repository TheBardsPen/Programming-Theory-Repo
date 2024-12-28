using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Town Database", menuName = "Locations/Town Database")]
public class TownDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    public TownObject[] Towns;
    public Dictionary<TownObject, int> GetId = new Dictionary<TownObject, int>();
    public Dictionary<int, TownObject> GetTown = new Dictionary<int, TownObject>();

    public void OnAfterDeserialize()
    {
        GetId = new Dictionary<TownObject, int>();
        GetTown = new Dictionary<int, TownObject>();

        for (int i = 0; i < Towns.Length; i++)
        {
            GetId.Add(Towns[i], i);
            GetTown.Add(i, Towns[i]);
        }
    }

    public void OnBeforeSerialize()
    {

    }
}
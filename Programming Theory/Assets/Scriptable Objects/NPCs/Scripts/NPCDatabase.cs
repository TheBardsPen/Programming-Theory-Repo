using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC Database", menuName = "NPC/NPC Database")]
public class NPCDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    public NPCObject[] npcArray;
    public Dictionary<NPCObject, int> getId = new Dictionary<NPCObject, int>();
    public Dictionary<int, NPCObject> getNPC = new Dictionary<int, NPCObject>();

    public void OnAfterDeserialize()
    {
        getId = new Dictionary<NPCObject, int>();
        getNPC = new Dictionary<int, NPCObject>();
        for (int i = 0; i < npcArray.Length; i++)
        {
            getId.Add(npcArray[i], i);
            getNPC.Add(i, npcArray[i]);
        }
    }

    public void OnBeforeSerialize()
    {

    }
}

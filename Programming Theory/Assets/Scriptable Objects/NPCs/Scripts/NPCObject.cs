using UnityEngine;
using Ink.Runtime;
using Ink.UnityIntegration;

[CreateAssetMenu(fileName = "NPC", menuName = "NPC/NPC")]
public class NPCObject : ScriptableObject
{
    public string npcName;
    public TextAsset story;
    [TextArea(15, 20)]
    public string description;
}

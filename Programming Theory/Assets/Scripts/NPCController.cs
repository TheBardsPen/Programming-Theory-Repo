using UnityEngine;
using Ink.Runtime;

public class NPCController : MonoBehaviour
{
    public NPCObject npc;

    private NPCSlot slot;

    public Story story;
    public int relationship;
    public bool isQuestActive;

    private void Start()
    {
        // Get story attatched to NPCObject
        story = new Story(npc.story.text);

        // Add npc if not already in datamanager
        //DataManager.instance.NPCRelations.AddNPC(npc);

        // Get reference slot for saved variables from datamanager
        for (int i = 0; i < DataManager.instance.npcRelations.container.Count; i++)
        {
            if (DataManager.instance.npcRelations.container[i].npc == npc)
            {
                slot = DataManager.instance.npcRelations.container[i];
            }
        }

        // Set local variables from datamanager
        if (slot != null)
        {
            relationship = slot.relationship;
            isQuestActive = slot.isQuestActive;
        }

        
        
    }

    public void RelationUpdate()
    {
        for (int i = 0; i < DataManager.instance.npcRelations.container.Count; i++)
        {
            if (DataManager.instance.npcRelations.container[i].npc == npc)
            {
                DataManager.instance.npcRelations.container[i].relationship = relationship;
                DataManager.instance.npcRelations.container[i].isQuestActive = isQuestActive;
            }
        }
    }

    /*private void Update()
    {
        for (int i = 0; i < DataManager.instance.NPCRelations.container.Count; i++)
        {
            if (npc = DataManager.instance.NPCRelations.container[i].npc)
            {
                if ()
                {
                    DataManager.instance.NPCRelations.container[i].relationship = relationship;
                    DataManager.instance.NPCRelations.container[i].isQuestActive = isQuestActive;
                }
            }
        }
    }*/
}

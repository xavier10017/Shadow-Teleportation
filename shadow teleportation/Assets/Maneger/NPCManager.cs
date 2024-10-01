using UnityEngine;
using System.Collections.Generic;

public class NPCManager : MonoBehaviour
{
    public List<NPC> npcList = new List<NPC>();

    void Update()
    {
        foreach (NPC npc in npcList)
        {
            npc.Move();
        }
    }

    // Método para adicionar NPCs à lista
    public void RegisterNPC(NPC npc)
    {
        npcList.Add(npc);
    }
}

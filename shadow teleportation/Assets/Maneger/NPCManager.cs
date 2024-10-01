using UnityEngine;
using System.Collections.Generic;

public class NPCManager : MonoBehaviour
{
    public List<NPC> npcList = new List<NPC>();
    public List<Transform> patrolPoints = new List<Transform>();

    void Start()
    {
        // Pegar todos os pontos de patrulha no início
        foreach (GameObject point in GameObject.FindGameObjectsWithTag("PatrolPoint"))
        {
            patrolPoints.Add(point.transform);
        }
    }

    void Update()
    {
        foreach (NPC npc in npcList)
        {
            npc.Move();
        }
    }

    public void RegisterNPC(NPC npc)
    {
        npcList.Add(npc);
        npc.AssignPatrolPoints(patrolPoints);
    }
}


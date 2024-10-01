using UnityEngine;
using System.Collections.Generic;

public class NPC : MonoBehaviour
{
    private List<Transform> patrolPoints;
    private int currentPatrolIndex = 0;
    public float moveSpeed = 2.0f;
    public float stoppingDistance = 0.2f; // Dist�ncia m�nima para considerar que chegou ao ponto de patrulha

    void Start()
    {
        NPCManager manager = FindObjectOfType<NPCManager>();
        if (manager != null)
        {
            manager.RegisterNPC(this);
        }
    }

    public void AssignPatrolPoints(List<Transform> points)
    {
        patrolPoints = points;
    }

    public void Move()
    {
        // Verifica se os pontos de patrulha foram definidos
        if (patrolPoints == null || patrolPoints.Count == 0)
        {
            Debug.LogWarning("No patrol points assigned!");
            return;
        }

        // Pega a posi��o do ponto de patrulha atual
        Transform targetPatrolPoint = patrolPoints[currentPatrolIndex];

        // Mant�m a posi��o Y do NPC
        Vector3 targetPosition = new Vector3(targetPatrolPoint.position.x, transform.position.y, targetPatrolPoint.position.z);

        // Move o NPC em dire��o ao ponto de patrulha
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Verifica se o NPC chegou ao ponto de patrulha
        if (Vector3.Distance(transform.position, targetPosition) <= stoppingDistance)
        {
            // Passa para o pr�ximo ponto de patrulha
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;
        }
    }

}


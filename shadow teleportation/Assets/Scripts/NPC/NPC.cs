using UnityEngine;

public class NPC : MonoBehaviour
{
    public Transform[] patrolPoints;  // Pontos de patrulha
    public float speed = 2.0f;        // Velocidade de movimento
    private int currentPointIndex = 0; // Índice do ponto atual de patrulha

    void Start()
    {
        NPCManager manager = FindObjectOfType<NPCManager>();
        if (manager != null)
        {
            manager.RegisterNPC(this);
        }
    }

    public void Move()
    {
        if (patrolPoints.Length == 0) return;

        // Mover NPC para o ponto atual de patrulha
        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, speed * Time.deltaTime);

        // Se o NPC chegou no ponto atual, move para o próximo ponto
        if (Vector3.Distance(transform.position, patrolPoints[currentPointIndex].position) < 0.1f)
        {
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;  // Loop nos pontos
        }
    }
}

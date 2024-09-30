using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowTeleport : MonoBehaviour
{

    public Transform player;
    public LayerMask shadowLayer;
    public float detectionRadius = 5f;

    private bool isInShadow = false;
    private Collider[] shadows;
    private Transform nearestShadow;

    void Update()
    {
        // Se o jogador estiver dentro de uma sombra, ele pode teleportar
        if (isInShadow)
        {
            if (Input.GetKeyDown(KeyCode.T)) // Supondo que "T" seja a tecla para teleportar
            {
                TeleportToNearestShadow();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Shadow"))
        {
            isInShadow = true;
            Debug.Log("Entrou na sombra");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Shadow"))
        {
            isInShadow = false;
            Debug.Log("Saiu da sombra");
        }
    }

    void TeleportToNearestShadow()
    {
        shadows = Physics.OverlapSphere(player.position, detectionRadius, shadowLayer);

        if (shadows.Length > 1) // Ignorar a sombra onde o jogador já está
        {
            float closestDistance = Mathf.Infinity;
            Transform closestShadow = null;

            foreach (Collider shadow in shadows)
            {
                // Evitar a sombra atual
                if (shadow.transform == nearestShadow)
                    continue;

                float distanceToShadow = Vector3.Distance(player.position, shadow.transform.position);
                if (distanceToShadow < closestDistance)
                {
                    closestDistance = distanceToShadow;
                    closestShadow = shadow.transform;
                }
            }

            if (closestShadow != null)
            {
                // Atualiza a sombra mais próxima e faz o teleporte
                nearestShadow = closestShadow;
                Vector3 targetPosition = nearestShadow.position;
                targetPosition.y += 1f; // Ajusta a altura se necessário

                player.position = targetPosition;
                Debug.Log("Teleportado para uma nova sombra");
            }
        }
    }


    private void OnDrawGizmos()
    {
        if (player != null)
        {
            Gizmos.color = Color.red; // Define a cor como vermelho
            Gizmos.DrawWireSphere(player.position, detectionRadius); // Desenha um raio ao redor do player
        }
    }

}

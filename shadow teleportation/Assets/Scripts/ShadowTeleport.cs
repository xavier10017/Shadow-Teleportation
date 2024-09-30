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

    private int selectedShadowIndex = 0; // Index para escolher sombras

    void Update()
    {
        // Se o jogador estiver dentro de uma sombra, ele pode teleportar
        if (isInShadow)
        {
            // Detecta sombras ao redor do player
            shadows = Physics.OverlapSphere(player.position, detectionRadius, shadowLayer);

            if (shadows.Length > 1) // Há mais de uma sombra para escolher
            {
                // Cicla pelas sombras usando teclas de seta (ou poderia usar scroll do mouse)
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    selectedShadowIndex = (selectedShadowIndex - 1 + shadows.Length) % shadows.Length;
                    HighlightSelectedShadow();
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    selectedShadowIndex = (selectedShadowIndex + 1) % shadows.Length;
                    HighlightSelectedShadow();
                }

                // Se o jogador pressionar "T", ele se teleporta para a sombra selecionada
                if (Input.GetKeyDown(KeyCode.T))
                {
                    TeleportToSelectedShadow();
                }
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

    void HighlightSelectedShadow()
    {
        // Aqui você pode modificar a aparência da sombra selecionada
        for (int i = 0; i < shadows.Length; i++)
        {
            Renderer shadowRenderer = shadows[i].GetComponent<Renderer>();

            if (i == selectedShadowIndex)
            {
                shadowRenderer.material.color = Color.green; // Muda a cor da sombra selecionada para verde
            }
            else
            {
                shadowRenderer.material.color = Color.black; // Restaura a cor das outras sombras para preto
            }
        }
    }

    void TeleportToSelectedShadow()
    {
        nearestShadow = shadows[selectedShadowIndex].transform;
        Vector3 targetPosition = nearestShadow.position;
        targetPosition.y += 1f; // Ajusta a altura se necessário

        player.position = targetPosition;
        Debug.Log("Teleportado para a sombra selecionada");
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

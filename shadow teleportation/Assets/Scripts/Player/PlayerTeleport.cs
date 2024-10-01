using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    private TeleportManager teleportManager; // Referência ao TeleportManager
    private Transform playerTransform; // Referência ao transform do jogador
    public float teleportHeightOffset = 1.0f; // Ajuste de altura para evitar entrar no chão
    public LayerMask teleportLayerMask; // Layer dos objetos de teleporte
    public LayerMask obstacleLayerMask; // Layer dos obstáculos que devem ser ignorados pelo Raycast

    void Start()
    {
        playerTransform = transform; // Referência ao transform do jogador
        teleportManager = FindObjectOfType<TeleportManager>(); // Pegar a referência do TeleportManager na cena
    }

    void Update()
    {
        // Detectar clique do botão esquerdo do mouse
        if (Input.GetMouseButtonDown(0))
        {
            DetectTeleportClick(); // Detectar se o jogador clicou em um ponto de teleporte
        }
    }

    void DetectTeleportClick()
    {
        // Raycast da posição da câmera em direção ao ponto clicado
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Desenhar a linha do Raycast na cena
        Debug.DrawLine(ray.origin, ray.direction * 100, Color.red, 2.0f); // Desenha a linha do Raycast em vermelho por 2 segundos

        // Verificar se o Raycast atinge um obstáculo primeiro
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, obstacleLayerMask))
        {
            // O Raycast atingiu um obstáculo, não faça nada ou trate como preferir
            Debug.Log("Raycast atingiu um obstáculo, não pode teleportar.");
            return; // Retorna e não faz nada se houver um obstáculo
        }

        // Agora verificar se o Raycast atinge um ponto de teleporte
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, teleportLayerMask))
        {
            // Se o objeto atingido pelo Raycast for um ponto de teleporte, teleportar o jogador
            Transform clickedObject = hit.transform;

            // Verificar se o objeto clicado está na lista de pontos de teleporte
            TryTeleportToPoint(clickedObject);
        }
        else
        {
            Debug.LogWarning("O objeto clicado não é um ponto de teleporte válido!");
        }
    }

    void TryTeleportToPoint(Transform teleportPoint)
    {
        if (teleportManager.GetAllTeleportPoints().Contains(teleportPoint))
        {
            // Teleportar o jogador para a posição do ponto de teleporte com ajuste de altura
            Vector3 targetPosition = teleportPoint.position;
            targetPosition.y += teleportHeightOffset;
            playerTransform.position = targetPosition;
        }
        else
        {
            Debug.LogWarning("O objeto clicado não é um ponto de teleporte válido!");
        }
    }
}





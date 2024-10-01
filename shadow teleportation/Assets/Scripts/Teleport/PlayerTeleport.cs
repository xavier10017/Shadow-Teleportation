using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    private TeleportManager teleportManager; // Refer�ncia ao TeleportManager
    private Transform playerTransform; // Refer�ncia ao transform do jogador
    public float teleportHeightOffset = 1.0f; // Ajuste de altura para evitar entrar no ch�o
    public LayerMask teleportLayerMask; // Layer dos objetos de teleporte
    public LayerMask obstacleLayerMask; // Layer dos obst�culos que devem ser ignorados pelo Raycast
    public Material highlightMaterial; // Material para destacar o ponto de teleporte
    public Material defaultMaterial; // Material padr�o do ponto de teleporte

    private Transform lastHighlightedObject; // �ltimo ponto de teleporte destacado

    void Start()
    {
        playerTransform = transform; // Refer�ncia ao transform do jogador
        teleportManager = FindObjectOfType<TeleportManager>(); // Pegar a refer�ncia do TeleportManager na cena
    }

    void Update()
    {
        // Detectar clique do bot�o esquerdo do mouse
        if (Input.GetMouseButtonDown(0))
        {
            DetectTeleportClick(); // Detectar se o jogador clicou em um ponto de teleporte
        }

        // Checar se o jogador est� com o mouse sobre um ponto de teleporte
        CheckForTeleportHighlight();
    }

    void DetectTeleportClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Desenhar a linha do Raycast na cena
        Debug.DrawLine(ray.origin, ray.direction * 100, Color.red, 2.0f); // Desenha a linha do Raycast em vermelho por 2 segundos

        // Verificar se o Raycast atinge um obst�culo primeiro
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, obstacleLayerMask))
        {
            Debug.Log("Raycast atingiu um obst�culo, n�o pode teleportar.");
            return; // Retorna e n�o faz nada se houver um obst�culo
        }

        // Verificar se o Raycast atinge um ponto de teleporte
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, teleportLayerMask))
        {
            Transform clickedObject = hit.transform;
            TryTeleportToPoint(clickedObject);
        }
        else
        {
            Debug.LogWarning("O objeto clicado n�o � um ponto de teleporte v�lido!");
        }
    }

    void TryTeleportToPoint(Transform teleportPoint)
    {
        if (teleportManager.GetAllTeleportPoints().Contains(teleportPoint))
        {
            Vector3 targetPosition = teleportPoint.position;
            targetPosition.y += teleportHeightOffset;
            playerTransform.position = targetPosition;
        }
        else
        {
            Debug.LogWarning("O objeto clicado n�o � um ponto de teleporte v�lido!");
        }
    }

    void CheckForTeleportHighlight()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Verificar se o Raycast atinge um ponto de teleporte
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, teleportLayerMask))
        {
            Transform hitObject = hit.transform;

            // Se um ponto de teleporte for detectado, trocar o material
            if (lastHighlightedObject != hitObject)
            {
                // Restaurar o material do �ltimo objeto destacado
                if (lastHighlightedObject != null)
                {
                    SetObjectMaterial(lastHighlightedObject, defaultMaterial);
                }

                // Trocar o material do novo objeto destacado
                SetObjectMaterial(hitObject, highlightMaterial);
                lastHighlightedObject = hitObject;
            }
        }
        else
        {
            // Se n�o houver um ponto de teleporte sob o mouse, restaurar o �ltimo material destacado
            if (lastHighlightedObject != null)
            {
                SetObjectMaterial(lastHighlightedObject, defaultMaterial);
                lastHighlightedObject = null;
            }
        }
    }

    void SetObjectMaterial(Transform obj, Material material)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = material;
        }
    }
}

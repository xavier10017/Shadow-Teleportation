using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    private TeleportManager teleportManager; // Referência ao TeleportManager
    private CharacterController characterController; // Referência ao Character Controller do jogador
    public float teleportHeightOffset = 1.0f; // Ajuste de altura para evitar entrar no chão
    public LayerMask teleportLayerMask; // Layer dos objetos de teleporte
    public LayerMask obstacleLayerMask; // Layer dos obstáculos que devem ser ignorados pelo Raycast
    public Material highlightMaterial; // Material para destacar o ponto de teleporte
    public Material defaultMaterial; // Material padrão do ponto de teleporte

    private Transform lastHighlightedObject; // Último ponto de teleporte destacado

    void Start()
    {
        characterController = GetComponent<CharacterController>(); // Referência ao Character Controller do jogador
        teleportManager = FindObjectOfType<TeleportManager>(); // Pegar a referência do TeleportManager na cena
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DetectTeleportClick(); // Detectar se o jogador clicou em um ponto de teleporte
        }

        // Checar se o jogador está com o mouse sobre um ponto de teleporte
        CheckForTeleportHighlight();
    }

    void DetectTeleportClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Debug.DrawLine(ray.origin, ray.direction * 100, Color.red, 2.0f);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, obstacleLayerMask))
        {
            Debug.Log("Raycast atingiu um obstáculo, não pode teleportar.");
            return;
        }

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, teleportLayerMask))
        {
            Transform clickedObject = hit.transform;
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
            Vector3 targetPosition = teleportPoint.position;
            targetPosition.y += teleportHeightOffset;

            // Usar o método Move do CharacterController para o teleporte
            Vector3 teleportVector = targetPosition - characterController.transform.position;
            characterController.Move(teleportVector);
        }
        else
        {
            Debug.LogWarning("O objeto clicado não é um ponto de teleporte válido!");
        }
    }

    void CheckForTeleportHighlight()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, teleportLayerMask))
        {
            Transform hitObject = hit.transform;

            if (lastHighlightedObject != hitObject)
            {
                if (lastHighlightedObject != null)
                {
                    SetObjectMaterial(lastHighlightedObject, defaultMaterial);
                }

                SetObjectMaterial(hitObject, highlightMaterial);
                lastHighlightedObject = hitObject;
            }
        }
        else
        {
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


using UnityEngine;

public class SixDirectionMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidade de movimento
    public float gravity = 9.8f; // Gravidade simulada
    private CharacterController controller;
    private Vector3 moveDirection;

    void Start()
    {
        // Obtém o componente CharacterController
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Obtém as entradas de movimento no plano horizontal (X e Z)
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Para cima e para baixo (usando as teclas Q e E)
        float moveUpDown = 0f;
        if (Input.GetKey(KeyCode.Q))
        {
            moveUpDown = 1f; // Movimenta para cima
        }
        else if (Input.GetKey(KeyCode.E))
        {
            moveUpDown = -1f; // Movimenta para baixo
        }

        // Se o personagem está no chão, define a direção de movimento
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(moveHorizontal, moveUpDown, moveVertical);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= moveSpeed;
        }

        // Aplica gravidade constante
        moveDirection.y -= gravity * Time.deltaTime;

        // Move o CharacterController
        controller.Move(moveDirection * Time.deltaTime);
    }
}


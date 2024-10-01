using UnityEngine;

public class SixDirectionMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidade do movimento
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Obtém as entradas no eixo horizontal e vertical
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Para cima e para baixo (usando as teclas Q e E como padrão)
        float moveUpDown = 0f;
        if (Input.GetKey(KeyCode.Q))
        {
            moveUpDown = 1f; // Movimenta para cima
        }
        else if (Input.GetKey(KeyCode.E))
        {
            moveUpDown = -1f; // Movimenta para baixo
        }

        // Vetor de movimento
        Vector3 movement = new Vector3(moveHorizontal, moveUpDown, moveVertical);

        // Aplica a movimentação ao Rigidbody
        rb.MovePosition(transform.position + movement * moveSpeed * Time.deltaTime);
    }
}

using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // O jogador ou objeto a ser seguido
    public float distance = 5.0f; // Distância da câmera para o jogador
    public float height = 2.0f; // Altura da câmera em relação ao jogador
    public float rotationSpeed = 5.0f; // Velocidade da rotação da câmera

    private float currentX = 0.0f; // Armazena a rotação atual no eixo X
    private float currentY = 0.0f; // Armazena a rotação atual no eixo Y

    void Update()
    {
        // Atualiza a rotação com base na entrada do mouse
        currentX += Input.GetAxis("Mouse X") * rotationSpeed;
        currentY -= Input.GetAxis("Mouse Y") * rotationSpeed;

        // Limita a rotação vertical da câmera
        currentY = Mathf.Clamp(currentY, -20, 60);
    }

    void LateUpdate()
    {
        // Calcula a rotação da câmera
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);

        // Calcula a posição desejada da câmera
        Vector3 position = target.position - (rotation * Vector3.forward * distance) + (Vector3.up * height);

        // Define a posição e a rotação da câmera
        transform.position = position;
        transform.LookAt(target);
    }
}

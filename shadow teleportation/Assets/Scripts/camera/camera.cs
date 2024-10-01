using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    // Referência para o Transform do jogador (ou objeto que a câmera vai seguir)
    public Transform _transform;

    // Referência para o Transform da própria câmera (ou seja, a posição e rotação da câmera)
    public Transform CameraTransform;

    // Variável que armazena a rotação do mouse no eixo X (horizontal) e Y (vertical)
    Vector2 rotacaoMouse;

    // Sensibilidade do controle do mouse (quanto a rotação será afetada pelo movimento do mouse)
    public int sensibilidade;

    // Limite inferior para o movimento vertical da câmera (quando o jogador olha para baixo)
    public float offSetCameraFloor = -80f;

    // Limite superior para o movimento vertical da câmera (quando o jogador olha para cima)
    public float offSetCameraRoof = 80f;

    // Método chamado no início do jogo
    private void Start()
    {
        // Trava o cursor no centro da tela e esconde o cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; // O cursor do mouse não será visível enquanto o jogo estiver rodando
    }

    // Método chamado a cada frame do jogo
    private void Update()
    {
        // Coleta a movimentação do mouse nos eixos X (horizontal) e Y (vertical)
        Vector2 controleMouse = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        // Atualiza a rotação do mouse baseada na sensibilidade e no deltaTime (para suavizar o movimento)
        rotacaoMouse = new Vector2(rotacaoMouse.x + controleMouse.x * sensibilidade * Time.deltaTime,
                                   rotacaoMouse.y + controleMouse.y * sensibilidade * Time.deltaTime);

        // Rotaciona o transform do jogador no eixo Y (horizontal), mantendo a rotação atual nos outros eixos
        _transform.eulerAngles = new Vector3(_transform.eulerAngles.x, rotacaoMouse.x, _transform.eulerAngles.z);

        // Limita a rotação vertical (eixo Y) da câmera dentro dos valores definidos por offSetCameraFloor e offSetCameraroof
        rotacaoMouse.y = Mathf.Clamp(rotacaoMouse.y, offSetCameraFloor, offSetCameraRoof);

        // Atualiza a rotação local da câmera no eixo X (vertical), permitindo que ela olhe para cima e para baixo
        CameraTransform.localEulerAngles = new Vector3(-rotacaoMouse.y, CameraTransform.localEulerAngles.y, CameraTransform.localEulerAngles.z);
    }
}


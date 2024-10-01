using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    // Refer�ncia para o Transform do jogador (ou objeto que a c�mera vai seguir)
    public Transform _transform;

    // Refer�ncia para o Transform da pr�pria c�mera (ou seja, a posi��o e rota��o da c�mera)
    public Transform CameraTransform;

    // Vari�vel que armazena a rota��o do mouse no eixo X (horizontal) e Y (vertical)
    Vector2 rotacaoMouse;

    // Sensibilidade do controle do mouse (quanto a rota��o ser� afetada pelo movimento do mouse)
    public int sensibilidade;

    // Limite inferior para o movimento vertical da c�mera (quando o jogador olha para baixo)
    public float offSetCameraFloor = -80f;

    // Limite superior para o movimento vertical da c�mera (quando o jogador olha para cima)
    public float offSetCameraRoof = 80f;

    // M�todo chamado no in�cio do jogo
    private void Start()
    {
        // Trava o cursor no centro da tela e esconde o cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; // O cursor do mouse n�o ser� vis�vel enquanto o jogo estiver rodando
    }

    // M�todo chamado a cada frame do jogo
    private void Update()
    {
        // Coleta a movimenta��o do mouse nos eixos X (horizontal) e Y (vertical)
        Vector2 controleMouse = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        // Atualiza a rota��o do mouse baseada na sensibilidade e no deltaTime (para suavizar o movimento)
        rotacaoMouse = new Vector2(rotacaoMouse.x + controleMouse.x * sensibilidade * Time.deltaTime,
                                   rotacaoMouse.y + controleMouse.y * sensibilidade * Time.deltaTime);

        // Rotaciona o transform do jogador no eixo Y (horizontal), mantendo a rota��o atual nos outros eixos
        _transform.eulerAngles = new Vector3(_transform.eulerAngles.x, rotacaoMouse.x, _transform.eulerAngles.z);

        // Limita a rota��o vertical (eixo Y) da c�mera dentro dos valores definidos por offSetCameraFloor e offSetCameraroof
        rotacaoMouse.y = Mathf.Clamp(rotacaoMouse.y, offSetCameraFloor, offSetCameraRoof);

        // Atualiza a rota��o local da c�mera no eixo X (vertical), permitindo que ela olhe para cima e para baixo
        CameraTransform.localEulerAngles = new Vector3(-rotacaoMouse.y, CameraTransform.localEulerAngles.y, CameraTransform.localEulerAngles.z);
    }
}


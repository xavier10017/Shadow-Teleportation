using UnityEngine;
using UnityEngine.UI;

public class ChangeUIColor : MonoBehaviour
{
    public Image centroBar;    // Referência à barra do centro

    public Color newColor = Color.red; // Nova cor que você quer aplicar

    void Start()
    {
        // Muda a cor de todas as barras
        if (centroBar != null)
        {
            centroBar.color = newColor;
        }
    }
}

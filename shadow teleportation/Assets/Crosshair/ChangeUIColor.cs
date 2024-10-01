using UnityEngine;
using UnityEngine.UI;

public class ChangeUIColor : MonoBehaviour
{
    public Image centroBar;    // Refer�ncia � barra do centro

    public Color newColor = Color.red; // Nova cor que voc� quer aplicar

    void Start()
    {
        // Muda a cor de todas as barras
        if (centroBar != null)
        {
            centroBar.color = newColor;
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    public List<Transform> teleportPoints = new List<Transform>(); // Lista dos pontos de teleporte

    void Start()
    {
        // Preencher a lista de teleportPoints com objetos vazios (Empty Game Objects)
        GameObject[] points = GameObject.FindGameObjectsWithTag("TeleportPoint");

        // Adicionar cada ponto de teleporte � lista de Transforms
        foreach (GameObject point in points)
        {
            teleportPoints.Add(point.transform);
        }
    }

    // Fun��o para obter um ponto de teleporte espec�fico pelo �ndice
    public Transform GetTeleportPoint(int index)
    {
        if (index >= 0 && index < teleportPoints.Count)
        {
            return teleportPoints[index]; // Retorna o ponto de teleporte se o �ndice for v�lido
        }
        return null; // Retorna null se o �ndice for inv�lido
    }

    // Fun��o para obter a lista de todos os pontos de teleporte
    public List<Transform> GetAllTeleportPoints()
    {
        return teleportPoints;
    }
}

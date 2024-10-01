using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    public List<Transform> teleportPoints = new List<Transform>(); // Lista dos pontos de teleporte

    void Start()
    {
        // Preencher a lista de teleportPoints com objetos vazios (Empty Game Objects)
        GameObject[] points = GameObject.FindGameObjectsWithTag("TeleportPoint");

        // Adicionar cada ponto de teleporte à lista de Transforms
        foreach (GameObject point in points)
        {
            teleportPoints.Add(point.transform);
        }
    }

    // Função para obter um ponto de teleporte específico pelo índice
    public Transform GetTeleportPoint(int index)
    {
        if (index >= 0 && index < teleportPoints.Count)
        {
            return teleportPoints[index]; // Retorna o ponto de teleporte se o índice for válido
        }
        return null; // Retorna null se o índice for inválido
    }

    // Função para obter a lista de todos os pontos de teleporte
    public List<Transform> GetAllTeleportPoints()
    {
        return teleportPoints;
    }
}

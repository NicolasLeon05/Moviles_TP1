using UnityEngine;
using System.Collections.Generic;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> obstaculos; // arrastrás todos los obstáculos aquí en el editor

    public void AplicarDificultad(GameSession.Difficulty dificultad)
    {
        // Desactivamos todos primero
        foreach (var obs in obstaculos)
            obs.SetActive(false);

        // Definimos porcentaje según dificultad
        float porcentaje = dificultad switch
        {
            GameSession.Difficulty.Easy => 0.4f,
            GameSession.Difficulty.Normal => 0.7f,
            GameSession.Difficulty.Hard => 1f,
            _ => 1f
        };

        int cantidad = Mathf.RoundToInt(obstaculos.Count * porcentaje);

        // Elegimos aleatoriamente los que se activan
        List<GameObject> copia = new List<GameObject>(obstaculos);

        for (int i = 0; i < cantidad; i++)
        {
            int index = Random.Range(0, copia.Count);
            copia[index].SetActive(true);
            copia.RemoveAt(index);
        }
    }
}

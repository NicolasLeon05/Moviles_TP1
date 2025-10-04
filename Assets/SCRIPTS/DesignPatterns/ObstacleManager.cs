using UnityEngine;
using System.Collections.Generic;

public class ObstacleManager : MonoBehaviour
{
    [Header("Obstáculos")]
    [SerializeField] private List<GameObject> obstaculos;

    [Header("Bolsas")]
    [SerializeField] private List<GameObject> bolsas;

    [Header("Estaciones")]
    [SerializeField] private List<GameObject> estaciones;


    public void AplicarDificultad(GameSession.Difficulty dificultad)
    {
        SetAllInactive(obstaculos);
        SetAllInactive(bolsas);
        SetAllInactive(estaciones);

        float porcentajeObs = dificultad switch
        {
            GameSession.Difficulty.Easy => 0.4f,
            GameSession.Difficulty.Normal => 0.7f,
            GameSession.Difficulty.Hard => 1f,
            _ => 1f
        };

        float porcentajeBolsas = dificultad switch
        {
            GameSession.Difficulty.Easy => 1f,
            GameSession.Difficulty.Normal => 0.7f,
            GameSession.Difficulty.Hard => 0.4f,
            _ => 1f
        };

        float porcentajeEstaciones = dificultad switch
        {
            GameSession.Difficulty.Easy => 1f,
            GameSession.Difficulty.Normal => 0.7f,
            GameSession.Difficulty.Hard => 0.5f,
            _ => 1f
        };

        ActivarAleatorios(obstaculos, porcentajeObs);
        ActivarAleatorios(bolsas, porcentajeBolsas);
        ActivarAleatorios(estaciones, porcentajeEstaciones);
    }

    private void SetAllInactive(List<GameObject> lista)
    {
        foreach (var go in lista)
            if (go != null) go.SetActive(false);
    }

    private void ActivarAleatorios(List<GameObject> lista, float porcentaje)
    {
        int cantidad = Mathf.RoundToInt(lista.Count * porcentaje);
        List<GameObject> copia = new List<GameObject>(lista);

        for (int i = 0; i < cantidad && copia.Count > 0; i++)
        {
            int index = Random.Range(0, copia.Count);
            copia[index].SetActive(true);
            copia.RemoveAt(index);
        }
    }
}

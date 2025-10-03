using UnityEngine;

public class CarreraState : IGameState
{
    private GameManager gm;
    private float tiempo = 60f;

    public CarreraState(GameManager gm) { this.gm = gm; }

    public void Enter()
    {
        Debug.Log("Estado: Carrera");
        gm.obstacleManager.AplicarDificultad(gm.ActualSession.difficulty);
    }

    public void Update()
    {
        tiempo -= Time.deltaTime;
        if (tiempo <= 0)
        {
            gm.FinalizarCarrera();
        }
    }

    public void Exit() { }
}

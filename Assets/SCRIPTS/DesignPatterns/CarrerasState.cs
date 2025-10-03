using UnityEngine;

public class CarreraState : IGameState
{
    private GameManager gm;
    private float tiempo = 60f;

    public CarreraState(GameManager gm) { this.gm = gm; }

    public void Enter()
    {
        Debug.Log("Estado: Carrera");

        if (gm.Player1 != null)
            gm.Player1.CambiarAConduccion();
        if (gm.Player2 != null)
            gm.Player2.CambiarAConduccion();
    }

    public void Update()
    {
        tiempo -= Time.deltaTime;
        if (tiempo <= 0)
        {
            gm.FinalizarCarrera();
        }
    }

    public void Exit()
    {
        Debug.Log("Saliendo de CarreraState");
    }
}

using UnityEngine;

public class CarreraState : IGameState
{
    private GameManager gm;
    private float tiempo = 5;

    public CarreraState(GameManager gm) { this.gm = gm; }

    public void Enter()
    {
        Debug.Log("Estado: Carrera");

        // Posicionar jugadores según su lado
        // (asumiendo que guardás estas posiciones en el GameManager como antes)
        if (gm.Player1 != null && gm.Player2 != null)
        {
            gm.Player1.transform.forward = Vector3.forward;
            gm.Player2.transform.forward = Vector3.forward;
        }

        if (gm.Player1 != null)
        {
            gm.Player1.CambiarAConduccion();
            gm.Player1.GetComponent<Frenado>().Frenar();
            gm.Player1.GetComponent<Frenado>().RestaurarVel();
            gm.Player1.GetComponent<ControlDireccion>().habilitado = true;
        }

        if (gm.ActualSession.mode == GameSession.GameMode.MultiPlayer && gm.Player2 != null)
        {
            gm.Player2.CambiarAConduccion();
            gm.Player2.GetComponent<Frenado>().Frenar();
            gm.Player2.GetComponent<Frenado>().RestaurarVel();
            gm.Player2.GetComponent<ControlDireccion>().habilitado = true;
        }
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

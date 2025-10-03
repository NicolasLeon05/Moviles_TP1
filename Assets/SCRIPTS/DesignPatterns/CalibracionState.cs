using UnityEngine;

public class CalibracionState : IGameState
{
    private GameManager gm;

    public CalibracionState(GameManager gm) { this.gm = gm; }

    public void Enter()
    {
        Debug.Log("Estado: Calibración");

        // Arrancar a los jugadores en calibración
        gm.Player1.CambiarACalibracion();
        if (gm.ActualSession.mode == GameSession.GameMode.MultiPlayer)
            gm.Player2.CambiarACalibracion();
    }

    public void Update()
    {
        if (gm.ActualSession.mode == GameSession.GameMode.SinglePlayer)
        {
            if (gm.Player1.EstAct != Player.Estados.EnCalibracion)
            {
                gm.CambiarEstado(new CarreraState(gm));
            }
        }
        else // MultiPlayer
        {
            if (gm.Player1.EstAct != Player.Estados.EnCalibracion &&
                gm.Player2.EstAct != Player.Estados.EnCalibracion)
            {
                gm.CambiarEstado(new CarreraState(gm));
            }
        }
    }

    public void Exit()
    {
        Debug.Log("Saliendo de Calibración");
    }
}

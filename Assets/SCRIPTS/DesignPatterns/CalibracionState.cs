using UnityEngine;

public class CalibracionState : IGameState
{
    private GameManager gm;

    public CalibracionState(GameManager gm) { this.gm = gm; }

    public void Enter()
    {
        Debug.Log("Estado: Calibraci�n");

        // Arrancar a los jugadores en calibraci�n
        gm.Player1.CambiarACalibracion();
        if (gm.ActualSession.mode == GameSession.GameMode.MultiPlayer)
            gm.Player2.CambiarACalibracion();

        // Pod�s inicializar ac� los objetos de calibraci�n tambi�n
        // Ejemplo:
        // foreach (var obj in gm.ObjetosCalibracion)
        //     obj.SetActive(true);
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
        Debug.Log("Saliendo de Calibraci�n");

        // Ac� desactiv�s objetos de calibraci�n y activ�s los de carrera
        // foreach (var obj in gm.ObjetosCalibracion)
        //     obj.SetActive(false);

        gm.Player1.CambiarAConduccion();
        if (gm.ActualSession.mode == GameSession.GameMode.MultiPlayer)
            gm.Player2.CambiarAConduccion();
    }
}

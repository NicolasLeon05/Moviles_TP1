using UnityEngine;

public class FinalState : IGameState
{
    private GameManager gm;
    private float timer = 10f;

    public FinalState(GameManager gm) { this.gm = gm; }

    public void Enter()
    {
        Debug.Log("Estado: Final");

        if (gm.ActualSession.mode == GameSession.GameMode.SinglePlayer)
            Debug.Log($"Puntaje Jugador 1: {gm.ActualSession.PtsJugador1}");
        else
            Debug.Log($"Ganador: Jugador {gm.ActualSession.WinnerId + 1}");
    }


    public void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            GameEvents.TriggerShowCredits();
        }
    }

    public void Exit() { }
}

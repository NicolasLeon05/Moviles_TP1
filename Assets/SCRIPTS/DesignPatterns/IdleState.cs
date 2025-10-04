using UnityEngine;

public class IdleState : IGameState
{
    private GameManager gm;

    public IdleState(GameManager gm)
    {
        this.gm = gm;
    }

    public void Enter()
    {
        Debug.Log("Estado: Idle (en men�)");
        // Pod�s resetear variables si quer�s:
        // gm.playersReady = 0;
    }

    public void Update()
    {
 
    }

    public void Exit()
    {
        Debug.Log("Saliendo de Idle");
    }
}

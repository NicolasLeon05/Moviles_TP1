using UnityEngine;

public class FinalState : IGameState
{
    private GameManager gm;
    private float timer = 10f;

    public FinalState(GameManager gm) { this.gm = gm; }

    public void Enter()
    {
        Debug.Log("Estado: Final");
        SceneController.Instance.LoadLevel(SceneController.Instance.levels[2]);
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

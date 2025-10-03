using UnityEngine;

public class GameSettingButton : MonoBehaviour
{
    public void SetEasy()
    {
        GameManager.Instance.ActualSession.difficulty = GameSession.Difficulty.Easy;
    }

    public void SetNormal()
    {
        GameManager.Instance.ActualSession.difficulty = GameSession.Difficulty.Normal;
    }

    public void SetHard()
    {
        GameManager.Instance.ActualSession.difficulty = GameSession.Difficulty.Hard;
    }

    public void SetSingleplayer()
    {
        // Creamos una GameSession provisoria con modo elegido y dificultad default
        GameManager.Instance.ActualSession = new GameSession(
            GameSession.Difficulty.Normal,
            GameSession.GameMode.SinglePlayer
        );
    }

    public void SetMultiplayer()
    {
        GameManager.Instance.ActualSession = new GameSession(
            GameSession.Difficulty.Normal,
            GameSession.GameMode.MultiPlayer
        );
    }
}

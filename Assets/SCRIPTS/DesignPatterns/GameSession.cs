public class GameSession
{
    public enum Difficulty { Easy, Normal, Hard }
    public enum GameMode { SinglePlayer, MultiPlayer }

    public Difficulty difficulty;
    public GameMode mode;

    public int PtsJugador1;
    public int PtsJugador2;

    public int WinnerId;

    public GameSession(Difficulty difficulty, GameMode modo)
    {
        this.difficulty = difficulty;
        this.mode = modo;
    }
}

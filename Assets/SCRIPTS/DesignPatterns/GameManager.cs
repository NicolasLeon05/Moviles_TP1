using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Referencias en runtime")]
    public Player Player1;
    public Player Player2;
    public ObstacleManager obstacleManager;
    public GameSession ActualSession;

    public InputSystem_Actions actions;

    private IGameState estadoActual;
    private int playersReady = 0; // control calibración

    //--------------------------------------------------------//

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            actions = new InputSystem_Actions();
            actions.Enable();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (ActualSession == null)
            ActualSession = new GameSession(GameSession.Difficulty.Normal, GameSession.GameMode.MultiPlayer);
    }

    private void Update()
    {
        if (estadoActual != null)
            estadoActual.Update();
    }

    //--------------------------------------------------------//

    public void CambiarEstado(IGameState nuevoEstado)
    {
        if (estadoActual != null)
            estadoActual.Exit();

        estadoActual = nuevoEstado;
        estadoActual.Enter();
    }

    //--------------------------------------------------------//

    public void FinalizarCarrera()
    {
        ActualSession.PtsJugador1 = Player1.Dinero;

        if (ActualSession.mode == GameSession.GameMode.MultiPlayer && Player2 != null && Player2.gameObject.activeSelf)
        {
            ActualSession.PtsJugador2 = Player2.Dinero;
            ActualSession.WinnerId = (Player1.Dinero > Player2.Dinero) ? 0 : 1;
        }
        else
        {
            ActualSession.PtsJugador2 = 0;
            ActualSession.WinnerId = 0;
        }

        CambiarEstado(new FinalState(this));
    }

    //--------------------------------------------------------//

    // Lo llama ContrCalibracion.FinTutorial()
    public void FinCalibracion(int playerID)
    {
        playersReady++;

        if (ActualSession.mode == GameSession.GameMode.SinglePlayer && playersReady == 1)
        {
            CambiarEstado(new CarreraState(this));
        }
        else if (ActualSession.mode == GameSession.GameMode.MultiPlayer && playersReady == 2)
        {
            CambiarEstado(new CarreraState(this));
        }
    }
}

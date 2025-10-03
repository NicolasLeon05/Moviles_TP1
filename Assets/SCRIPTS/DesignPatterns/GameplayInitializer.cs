using UnityEngine;

public class GameplayInitializer : MonoBehaviour
{
    [Header("Configuración Jugadores")]
    public Transform[] spawnPositions;   // 0 = P1, 1 = P2
    public Player playerPrefab1;
    public Player playerPrefab2;

    [Header("Cámaras Calibracion")]
    public Camera camCalibracionP1;
    public Camera camCalibracionP2;

    [Header("Cámaras Conducción")]
    public Camera camConduccionP1;
    public Camera camConduccionP2;

    [Header("Cámaras Descarga")]
    public Camera camDescargaP1;
    public Camera camDescargaP2;

    [Header("Otros sistemas")]
    public ObstacleManager obstacleManager;

    void Start()
    {
        var gm = GameManager.Instance;

        // Instanciar Player1 siempre
        gm.Player1 = Instantiate(playerPrefab1, spawnPositions[0].position, spawnPositions[0].rotation);
        gm.Player1.IdPlayer = 0;

        if (gm.ActualSession.mode == GameSession.GameMode.MultiPlayer)
        {
            gm.Player2 = Instantiate(playerPrefab2, spawnPositions[1].position, spawnPositions[1].rotation);
            gm.Player2.IdPlayer = 1;
        }

        // Configuración de cámaras
        if (gm.ActualSession.mode == GameSession.GameMode.MultiPlayer)
        {
            // Conducción
            camConduccionP1.rect = new Rect(0f, 0f, 0.5f, 1f);
            camConduccionP2.rect = new Rect(0.5f, 0f, 0.5f, 1f);
            camConduccionP1.gameObject.SetActive(true);
            camConduccionP2.gameObject.SetActive(true);

            // Descarga
            camDescargaP1.rect = new Rect(0f, 0f, 0.5f, 1f);
            camDescargaP2.rect = new Rect(0.5f, 0f, 0.5f, 1f);
            camDescargaP1.gameObject.SetActive(true);
            camDescargaP2.gameObject.SetActive(true);

            // Calibracion
            camCalibracionP1.rect = new Rect(0f, 0f, 0.5f, 1f);
            camCalibracionP2.rect = new Rect(0.5f, 0f, 0.5f, 1f);
            camCalibracionP1.gameObject.SetActive(true);
            camCalibracionP2.gameObject.SetActive(true);
        }
        else
        {
            // SinglePlayer -> sólo P1 en fullscreen
            camConduccionP1.rect = new Rect(0f, 0f, 1f, 1f);
            camConduccionP1.gameObject.SetActive(true);
            camConduccionP2.gameObject.SetActive(false);

            camDescargaP1.rect = new Rect(0f, 0f, 1f, 1f);
            camDescargaP1.gameObject.SetActive(true);
            camDescargaP2.gameObject.SetActive(false);

            camCalibracionP1.rect = new Rect(0f, 0f, 1f, 1f);
            camCalibracionP1.gameObject.SetActive(true);
            camCalibracionP2.gameObject.SetActive(false);
        }

        // Asignar obstacle manager de esta escena
        gm.obstacleManager = obstacleManager;

        // Aplicar dificultad antes de arrancar
        gm.obstacleManager.AplicarDificultad(gm.ActualSession.difficulty);

        // Si es single y accidentalmente se creó P2, lo apagamos
        if (gm.ActualSession.mode == GameSession.GameMode.SinglePlayer && gm.Player2 != null)
        {
            gm.Player2.gameObject.SetActive(false);
            gm.Player2 = null;
        }

        // Iniciar calibración
        gm.CambiarEstado(new CalibracionState(gm));
    }
}

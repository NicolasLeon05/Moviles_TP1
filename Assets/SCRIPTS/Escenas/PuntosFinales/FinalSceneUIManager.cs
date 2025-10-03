using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class FinalSceneUIManager : MonoBehaviour
{
    [Header("UI References")]
    public Image winnerBanner;
    public TextMeshProUGUI scoreLeft;
    public TextMeshProUGUI scoreRight;
    public GameObject panelFinal;
    public Sprite winnerLeft;
    public Sprite winnerRight;

    [Header("Settings")]
    public float blinkInterval = 0.7f;
    public float restartDelay = 10f;

    private float blinkTimer;
    private bool blinkOn = true;
    private bool showUI = false;

    void Start()
    {
        SetupWinner();
        Invoke(nameof(EnableUI), 2.5f);
    }

    void Update()
    {
        if (restartDelay <= 0)
        {
            GameEvents.TriggerShowCredits();
            OldGameManager.Instancia.ResetGame();
            //Debug.Log("Restart entered");
            //SceneController.Instance.LoadLevel(SceneController.Instance.levels[0]);
        }


        // Blink ganador
        if (showUI)
        {
            blinkTimer += Time.deltaTime;
            if (blinkTimer >= blinkInterval)
            {
                blinkTimer = 0;
                blinkOn = !blinkOn;
                UpdateScores();
            }
        }
    }

    void EnableUI()
    {
        showUI = true;
        panelFinal.SetActive(true);
        UpdateScores();
    }

    public void DisableUI()
    {
        if (panelFinal != null)
            panelFinal.SetActive(false);
    }

    void SetupWinner()
    {
        if (DatosPartida.LadoGanadaor == DatosPartida.Lados.Izq)
        {
            // winnerText.text = "PLAYER 1 IS THE WINNER";
            winnerBanner.sprite = winnerLeft;
        }
        else
        {
            // winnerText.text = "PLAYER 2 IS THE WINNER";
            winnerBanner.sprite = winnerRight;
        }

        var aux = winnerBanner.color;
        aux.a = 1.0f;
        winnerBanner.color = aux;
    }

    void UpdateScores()
    {
        if (DatosPartida.LadoGanadaor == DatosPartida.Lados.Izq)
        {
            scoreLeft.text = blinkOn ? "$" + DatosPartida.PtsGanador : "";
            scoreRight.text = "$" + DatosPartida.PtsPerdedor;
        }
        else
        {
            scoreLeft.text = "$" + DatosPartida.PtsPerdedor;
            scoreRight.text = blinkOn ? "$" + DatosPartida.PtsGanador : "";
        }
    }
}

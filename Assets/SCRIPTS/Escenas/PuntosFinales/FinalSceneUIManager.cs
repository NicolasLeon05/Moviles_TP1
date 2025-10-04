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
        if (GameManager.Instance.ActualSession.WinnerId == 0)
            winnerBanner.sprite = winnerLeft;
        else
            winnerBanner.sprite = winnerRight;
    }

    void UpdateScores()
    {
        if (GameManager.Instance.ActualSession.WinnerId == 0)
        {
            scoreLeft.text = blinkOn ? "$" + GameManager.Instance.ActualSession.PtsJugador1 : "";
            scoreRight.text = "$" + GameManager.Instance.ActualSession.PtsJugador2;
        }
        else
        {
            scoreLeft.text = "$" + GameManager.Instance.ActualSession.PtsJugador1;
            scoreRight.text = blinkOn ? "$" + GameManager.Instance.ActualSession.PtsJugador2 : "";
        }
    }

}

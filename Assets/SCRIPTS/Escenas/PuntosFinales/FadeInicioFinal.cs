using UnityEngine;
using UnityEngine.UI;

public class FadeInicioFinal : MonoBehaviour
{
    [Header("Fade Settings")]
    public float DuracionFade = 2f;
    public Image fadeImage;

    public FinalSceneUIManager mng;
    private float tiempInicial;
    private bool mngAvisado = false;
    private Color aux;

    void Start()
    {
        tiempInicial = mng.restartDelay;

        aux = fadeImage.color;
        aux.a = 1;
        fadeImage.color = aux;
    }

    void Update()
    {
        if (mng.restartDelay > tiempInicial - DuracionFade) // Fading In (aparece desde negro)
        {
            aux = fadeImage.color;
            aux.a -= Time.deltaTime / DuracionFade;
            fadeImage.color = aux;
        }
        else if (mng.restartDelay < DuracionFade) // Fading Out (desaparece a negro)
        {
            aux = fadeImage.color;
            aux.a += Time.deltaTime / DuracionFade;
            fadeImage.color = aux;

            if (!mngAvisado)
            {
                mngAvisado = true;
                mng.DisableUI();
            }
        }
    }
}

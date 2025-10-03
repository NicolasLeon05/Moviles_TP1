using UnityEngine;
using UnityEngine.UI;

public class FadeInicioFinal : MonoBehaviour
{
    [Header("Fade Settings")]
    public float Duracion = 2f;  // duración del fade in/out
    public Image fadeImage;      // referencia al Image full-screen del Canvas

    public FinalSceneUIManager mng;
    private float tiempInicial;
    private bool mngAvisado = false;
    private Color aux;

    void Start()
    {
        // Buscamos el manager de la UI final
        //mng = FindAnyObjectByType<FinalUIManager>();
        tiempInicial = mng.restartDelay;

        // Configuramos el color inicial del fade
        aux = fadeImage.color;
        aux.a = 1;
        fadeImage.color = aux;
    }

    void Update()
    {
        if (mng.restartDelay > tiempInicial - Duracion) // Fading In (aparece desde negro)
        {
            aux = fadeImage.color;
            aux.a -= Time.deltaTime / Duracion;
            fadeImage.color = aux;
        }
        else if (mng.restartDelay < Duracion) // Fading Out (desaparece a negro)
        {
            aux = fadeImage.color;
            aux.a += Time.deltaTime / Duracion;
            fadeImage.color = aux;

            if (!mngAvisado)
            {
                mngAvisado = true;
                mng.DisableUI(); // oculta el panelFinal
            }
        }
    }
}

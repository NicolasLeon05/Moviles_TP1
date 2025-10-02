using UnityEngine;
using UnityEngine.UI;

public class LoopTexturaUI : MonoBehaviour
{
    public float Intervalo = 1f;
    private float tempo = 0f;

    public Sprite[] Imagenes;
    private int contador = 0;

    private Image uiImage;

    void Start()
    {
        uiImage = GetComponent<Image>();

        if (Imagenes.Length > 0)
            uiImage.sprite = Imagenes[0];
    }

    void Update()
    {
        tempo += Time.deltaTime;

        if (tempo >= Intervalo && Imagenes.Length > 0)
        {
            tempo = 0;
            contador = (contador + 1) % Imagenes.Length;
            uiImage.sprite = Imagenes[contador];
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class PantallaCalibTutoUI : MonoBehaviour
{
    public Sprite[] ImagenesDelTuto;
    public float Intervalo = 1.2f;

    private float tempoIntTuto = 0;
    private int enCursoTuto = 0;

    public Sprite ImaReady;
    public ContrCalibracion ContrCalib;

    private Image uiImage;

    void Start()
    {
        uiImage = GetComponent<Image>();
    }

    void Update()
    {
        switch (ContrCalib.EstAct)
        {
            case ContrCalibracion.Estados.Tutorial:
                tempoIntTuto += Time.deltaTime;
                if (tempoIntTuto >= Intervalo)
                {
                    tempoIntTuto = 0;
                    enCursoTuto = (enCursoTuto + 1) % ImagenesDelTuto.Length;
                }
                uiImage.sprite = ImagenesDelTuto[enCursoTuto];
                break;

            case ContrCalibracion.Estados.Finalizado:
                uiImage.sprite = ImaReady;
                break;
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VisualizacionCanvas : MonoBehaviour
{
    public enum Lado { Izq, Der }
    public Lado LadoAct;

    public GameObject panelConduccion;

    [Header("Referencias")]
    public Player Pj;
    public ControlDireccion Direccion;

    [Header("Dinero")]
    public TMP_Text dineroText;

    [Header("Volante")]
    public Image volanteImage;
    public float maxAnguloVolante = 100f;

    [Header("Inventario")]
    public Image inventarioImage;
    public Sprite[] spritesInvIzq;
    public Sprite[] spritesInvDer;
    public float tiempoParpadeo = 0.8f;

    [Header("Bonus (solo Descarga)")]
    public GameObject panelDescarga;
    public Image bonusBarra;
    public TMP_Text bonusText;
    public Image bonusIcono;

    private float tempParp = 0;
    private bool primIma = true;

    private void Awake()
    {
        panelDescarga.SetActive(false);
    }

    void Update()
    {
        if (Pj == null || Direccion == null) return;

        switch (Pj.EstAct)
        {
            case Player.Estados.EnConduccion:
                panelConduccion.SetActive(true);
                volanteImage.gameObject.SetActive(true);
                panelDescarga.SetActive(false);

                SetDinero();
                SetVolante();
                SetInventario();
                break;

            case Player.Estados.EnDescarga:
                volanteImage.gameObject.SetActive(false);
                panelDescarga.SetActive(true);

                SetDinero();
                SetInventario();
                SetBonus();
                break;

            default:
                panelConduccion.SetActive(false);
                panelDescarga.SetActive(false);
                break;
        }
    }


    // ---------------- DINERO ----------------
    void SetDinero()
    {
        if (dineroText == null) return;

        if (Pj.Dinero < 1)
        {
            dineroText.text = "";
        }
        else
        {
            dineroText.text = "$" + PrepararNumeros(Pj.Dinero);
        }
    }

    string PrepararNumeros(int dinero)
    {
        string strDinero = dinero.ToString();
        string res = "";

        if (dinero < 1)
        {
            res = "";
        }
        else if (strDinero.Length == 6)
        {
            for (int i = 0; i < strDinero.Length; i++)
            {
                res += strDinero[i];
                if (i == 2) res += ".";
            }
        }
        else if (strDinero.Length == 7)
        {
            for (int i = 0; i < strDinero.Length; i++)
            {
                res += strDinero[i];
                if (i == 0 || i == 3) res += ".";
            }
        }
        else
        {
            res = strDinero;
        }

        return res;
    }

    // ---------------- VOLANTE ----------------
    void SetVolante()
    {
        if (volanteImage == null)
            return;

        float giro = Direccion.GetGiro(); // -1 a 1
        float angulo = giro * maxAnguloVolante;
        volanteImage.rectTransform.rotation = Quaternion.Euler(0, 0, -angulo);
    }

    // ---------------- INVENTARIO ----------------
    void SetInventario()
    {
        if (inventarioImage == null)
            return;

        int contador = 0;
        for (int i = 0; i < 3; i++)
        {
            if (Pj.Bolasas[i] != null)
                contador++;
        }

        Sprite[] arr = (LadoAct == Lado.Izq) ? spritesInvIzq : spritesInvDer;

        if (contador < 3)
        {
            inventarioImage.sprite = arr[contador];
        }
        else
        {
            tempParp += Time.deltaTime;
            if (tempParp >= tiempoParpadeo)
            {
                tempParp = 0;
                primIma = !primIma;
            }
            inventarioImage.sprite = primIma ? arr[3] : arr[4];
        }
    }

    // ---------------- BONUS ----------------
    void SetBonus()
    {
        if (panelDescarga == null || bonusText == null || bonusBarra == null)
            return;

        panelDescarga.SetActive(true);

        int bonus = (int)(Pj.ContrDesc != null ? Pj.ContrDesc.Bonus : 0);
        bonusText.text = "$" + PrepararNumeros(bonus);

        float progreso = 0f;
        if (Pj.ContrDesc != null && Pj.ContrDesc.BonusMax > 0)
        {
            progreso = Mathf.Clamp01((float)bonus / Pj.ContrDesc.BonusMax);
        }
        bonusBarra.fillAmount = progreso;
    }

}
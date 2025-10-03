using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public int Dinero = 0;
    public int IdPlayer = 0;

    public Bolsa[] Bolasas;
    int CantBolsAct = 0;
    public string TagBolsas = "";

    public enum Estados { EnDescarga, EnConduccion, EnCalibracion/*, EnTutorial*/}
    public Estados EstAct = Estados.EnConduccion;

    public bool EnConduccion = true;
    public bool EnDescarga = false;

    public ControladorDeDescarga ContrDesc;
    public ContrCalibracion ContrCalib;
    //public ContrTutorial ContrTuto;

    //Visualizacion MiVisualizacion;

    //------------------------------------------------------------------//

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < Bolasas.Length; i++)
            Bolasas[i] = null;

        //MiVisualizacion = GetComponent<Visualizacion>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //------------------------------------------------------------------//

    public bool AgregarBolsa(Bolsa b)
    {
        if (CantBolsAct + 1 <= Bolasas.Length)
        {
            Bolasas[CantBolsAct] = b;
            CantBolsAct++;
            Dinero += (int)b.Monto;
            b.Desaparecer();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void VaciarInv()
    {
        for (int i = 0; i < Bolasas.Length; i++)
            Bolasas[i] = null;

        CantBolsAct = 0;
    }

    public bool ConBolasas()
    {
        for (int i = 0; i < Bolasas.Length; i++)
        {
            if (Bolasas[i] != null)
            {
                return true;
            }
        }
        return false;
    }

    public void SetContrDesc(ControladorDeDescarga contr)
    {
        ContrDesc = contr;
    }

    public ControladorDeDescarga GetContr()
    {
        return ContrDesc;
    }

    public void CambiarACalibracion()
    {
        EstAct = Estados.EnCalibracion;

        // deshabilitar controles de conducción
        var dir = GetComponent<ControlDireccion>();
        if (dir != null)
            dir.habilitado = false;

        var acel = GetComponent<Aceleracion>();
        if (acel != null)
            acel.enabled = false;

        if (ContrCalib != null)
            ContrCalib.IniciarTesteo();
    }


    public void CambiarAConduccion()
    {
        EstAct = Estados.EnConduccion;

        // habilitar controles de manejo
        var dir = GetComponent<ControlDireccion>();
        if (dir != null)
            dir.habilitado = true;

        var acel = GetComponent<Aceleracion>();
        if (acel != null)
            acel.enabled = true;
    }

    public void CambiarADescarga()
    {
        EstAct = Estados.EnDescarga;

        var dir = GetComponent<ControlDireccion>();
        if (dir != null)
            dir.habilitado = false;

        var acel = GetComponent<Aceleracion>();
        if (acel != null)
            acel.enabled = false;

        if (ContrDesc != null)
            ContrDesc.enabled = true;
    }


    public void SacarBolasa()
    {
        for (int i = 0; i < Bolasas.Length; i++)
        {
            if (Bolasas[i] != null)
            {
                Bolasas[i] = null;
                return;
            }
        }
    }
}

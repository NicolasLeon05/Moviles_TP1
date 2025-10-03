using UnityEngine;

public class ContrCalibracion : MonoBehaviour
{
    public Player Pj;
    public float TiempEspCalib = 3;
    private float tempo = 0;

    public enum Estados { Tutorial, Finalizado }
    public Estados EstAct = Estados.Tutorial;

    public ManejoPallets Partida;
    public ManejoPallets Llegada;
    public Pallet P;
    public ManejoPallets palletsMover;

    //----------------------------------------------------//

    void Start()
    {
        palletsMover.enabled = false;
        Pj.ContrCalib = this;

        // Preparar pallet inicial
        P.CintaReceptora = Llegada.gameObject;
        Partida.Recibir(P);

        SetActivComp(false);
    }

    void Update()
    {
        if (EstAct == Estados.Tutorial)
        {
            if (tempo < TiempEspCalib)
            {
                tempo += Time.deltaTime;
                if (tempo > TiempEspCalib)
                {
                    SetActivComp(true);
                }
            }
        }
    }

    public void IniciarTesteo()
    {
        EstAct = Estados.Tutorial;
        palletsMover.enabled = true;
    }

    public void FinTutorial()
    {
        EstAct = Estados.Finalizado;
        palletsMover.enabled = false;

        GameManager.Instance.FinCalibracion(Pj.IdPlayer);
    }

    void SetActivComp(bool estado)
    {
        if (Partida.GetComponent<Renderer>() != null)
            Partida.GetComponent<Renderer>().enabled = estado;
        Partida.GetComponent<Collider>().enabled = estado;

        if (Llegada.GetComponent<Renderer>() != null)
            Llegada.GetComponent<Renderer>().enabled = estado;
        Llegada.GetComponent<Collider>().enabled = estado;

        P.GetComponent<Renderer>().enabled = estado;
    }
}

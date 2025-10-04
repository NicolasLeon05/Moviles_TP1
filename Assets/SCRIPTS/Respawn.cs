using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour
{
    CheakPoint CPAct;
    CheakPoint CPAnt;

    public float AngMax = 90;//angulo maximo antes del cual se reinicia el camion
    int VerifPorCuadro = 20;
    int Contador = 0;

    public float RangMinDer = 0;
    public float RangMaxDer = 0;

    bool IgnorandoColision = false;
    public float TiempDeNoColision = 2;
    float Tempo = 0;

    //--------------------------------------------------------//

    // Use this for initialization
    void Start()
    {
        Physics.IgnoreLayerCollision(8, 9, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (CPAct != null)
        {
            Contador++;
            if (Contador == VerifPorCuadro)
            {
                Contador = 0;
                if (AngMax < Quaternion.Angle(transform.rotation, CPAct.transform.rotation))
                {
                    Respawnear();
                }
            }
        }

        if (IgnorandoColision)
        {
            Tempo += T.GetDT();
            if (Tempo > TiempDeNoColision)
            {
                IgnorarColision(false);
            }
        }

    }

    //--------------------------------------------------------//

    public void Respawnear()
    {
        var rb = GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        gameObject.GetComponent<CarController>().SetGiro(0f);

        if (CPAct != null && CPAct.Habilitado())
        {
            SetPosSegunJugador(CPAct);
        }
        else if (CPAnt != null)
        {
            SetPosSegunJugador(CPAnt);
        }

        IgnorarColision(true);
    }

    public void Respawnear(Vector3 pos)
    {
        GetComponent<Rigidbody>().linearVelocity = Vector3.zero;

        gameObject.GetComponent<CarController>().SetGiro(0f);

        transform.position = pos;

        IgnorarColision(true);
    }

    private void SetPosSegunJugador(CheakPoint cp)
    {
        var player = GetComponent<Player>();
        Vector3 pos;

        if (player != null && player.IdPlayer == 1) // derecha
            pos = cp.transform.position + cp.transform.right * Random.Range(RangMinDer, RangMaxDer);
        else // izquierda
            pos = cp.transform.position + cp.transform.right * Random.Range(-RangMaxDer, -RangMinDer);

        transform.position = pos;
        transform.forward = cp.transform.forward;
    }
    public void Respawnear(Vector3 pos, Vector3 dir)
    {
        GetComponent<Rigidbody>().linearVelocity = Vector3.zero;

        gameObject.GetComponent<CarController>().SetGiro(0f);

        transform.position = pos;
        transform.forward = dir;

        IgnorarColision(true);
    }

    public void AgregarCP(CheakPoint cp)
    {
        if (cp != CPAct)
        {
            CPAnt = CPAct;
            CPAct = cp;
        }
    }

    void IgnorarColision(bool b)
    {
        //no contempla si los dos camiones respawnean relativamente cerca en el espacio 
        //temporal y uno de ellos va contra el otro, 
        //justo el segundo cancela las colisiones e inmediatamente el 1º las reactiva, 
        //entonces colisionan, pero es dificil que suceda. 

        Physics.IgnoreLayerCollision(8, 9, b);
        IgnorandoColision = b;
        Tempo = 0;
    }




}

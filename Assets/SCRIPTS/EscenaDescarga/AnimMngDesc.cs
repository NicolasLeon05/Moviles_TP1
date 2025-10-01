using UnityEngine;
using System.Collections;

public class AnimMngDesc : MonoBehaviour
{
    public string AnimEntrada = "Entrada";
    public string AnimSalida = "Salida";
    public ControladorDeDescarga ContrDesc;

    enum AnimEnCurso { Salida, Entrada, Nada }
    AnimEnCurso AnimAct = AnimEnCurso.Nada;

    public GameObject PuertaAnimada;

    // Update is called once per frame
    void Update()
    {

        switch (AnimAct)
        {
            case AnimEnCurso.Entrada:

                if (!GetComponent<Animation>().IsPlaying(AnimEntrada))
                {
                    AnimAct = AnimEnCurso.Nada;
                    ContrDesc.FinAnimEntrada();
                    print("fin Anim Entrada");
                }

                break;

            case AnimEnCurso.Salida:

                if (!GetComponent<Animation>().IsPlaying(AnimSalida))
                {
                    AnimAct = AnimEnCurso.Nada;
                    ContrDesc.FinAnimSalida();
                    print("fin Anim Salida");
                }

                break;

            case AnimEnCurso.Nada:
                break;
        }
    }

    public void Entrar()
    {
        AnimAct = AnimEnCurso.Entrada;
        GetComponent<Animation>().Play(AnimEntrada);

        if (PuertaAnimada != null)
        {
            PuertaAnimada.GetComponent<Animation>()["AnimPuerta"].time = 0;
            PuertaAnimada.GetComponent<Animation>()["AnimPuerta"].speed = 1;
            PuertaAnimada.GetComponent<Animation>().Play("AnimPuerta");
        }
    }

    public void Salir()
    {
        AnimAct = AnimEnCurso.Salida;
        GetComponent<Animation>().Play(AnimSalida);

        if (PuertaAnimada != null)
        {
            PuertaAnimada.GetComponent<Animation>()["AnimPuerta"].time = PuertaAnimada.GetComponent<Animation>()["AnimPuerta"].length;
            PuertaAnimada.GetComponent<Animation>()["AnimPuerta"].speed = -1;
            PuertaAnimada.GetComponent<Animation>().Play("AnimPuerta");
        }
    }
}

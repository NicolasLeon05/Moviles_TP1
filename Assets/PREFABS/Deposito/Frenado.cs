using UnityEngine;
using System.Collections;

public class Frenado : MonoBehaviour 
{
	public float VelEntrada = 0;
	public string TagDeposito = "Deposito";
	
	int Contador = 0;
	int CantMensajes = 10;
	float TiempFrenado = 0.5f;
	float Tempo = 0f;
	
	Vector3 Destino;
	
	public bool Frenando = false;
	
	//-----------------------------------------------------//
	
	// Use this for initialization
	void Start () 
	{
		Frenar();
	}
	
	void FixedUpdate ()
	{
		if(Frenando)
		{
			Tempo += T.GetFDT();
			if(Tempo >= (TiempFrenado / CantMensajes) * Contador)
			{
				Contador++;
			}
			if(Tempo >= TiempFrenado)
			{
				//termino de frenar, que haga lo que quiera
			}
		}
	}
	
	void OnTriggerEnter(Collider other) 
	{
		if(other.tag == TagDeposito)
		{
			Deposito2 dep = other.GetComponent<Deposito2>();
			if(dep.Vacio)
			{	
				if(this.GetComponent<Player>().ConBolasas())
				{
					dep.Entrar(this.GetComponent<Player>());
					Destino = other.transform.position;
					transform.forward = Destino - transform.position;
					Frenar();
				}				
			}
		}
	}
	
	//-----------------------------------------------------------//
	
	public void Frenar()
	{
		GetComponent<ControlDireccion>().enabled = false;
		gameObject.GetComponent<CarController>().SetAcel(0);

        GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
		
		Frenando = true;
		
		Tempo = 0;
		Contador = 0;
	}
	
	public void RestaurarVel()
	{
		GetComponent<ControlDireccion>().enabled = true;
        gameObject.GetComponent<CarController>().SetAcel(1);
        Frenando = false;
		Tempo = 0;
		Contador = 0;
	}
}

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VidIntrMgr : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//PARA JUGAR
		if(Input.GetKeyDown(KeyCode.KeypadEnter) || 
		   Input.GetKeyDown(KeyCode.Return) ||
		   Input.GetKeyDown(KeyCode.Mouse0))
		{
            SceneController.Instance.LoadLevel(SceneController.Instance.levels[0]); ;//el juego
		}
		
		
		//CIERRA LA APLICACION
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}
}

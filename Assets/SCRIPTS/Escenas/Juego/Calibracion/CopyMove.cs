using UnityEngine;
using System.Collections;

public class CopyMove : MonoBehaviour
{
    public Transform Target;
    //public float Diferencia = 1;


    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Target.position;// + Target.transform.right * Diferencia;
                                             //transform.localRotation = Target.localRotation;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaContainer : MonoBehaviour
{
    private Animator anim;

    [SerializeField] private SistemaInteraccion sistemaInteraccion;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void AbrirPuerta()
    {

        anim.SetTrigger("Puerta1");
        anim.ResetTrigger("Puerta2");
        sistemaInteraccion.puertaAbierta = true;

    }
    public void CerrarPuerta()
    {

        anim.SetTrigger("Puerta2");
        anim.ResetTrigger("Puerta1");
        sistemaInteraccion.puertaAbierta = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rango : MonoBehaviour
{

    private bool estaEnRango = false;

    public bool EstaEnRango { get => estaEnRango; set => estaEnRango = value; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EstaEnRango = true;


        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EstaEnRango = false;


        }
    }

    
}

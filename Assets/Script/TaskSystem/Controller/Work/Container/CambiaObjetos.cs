using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiaObjetos : MonoBehaviour
{
    [SerializeField] private GameObject radio;
    [SerializeField] private Material radioOff;
    

    private int indiceActual;

    private bool rActive = true;

    private void Start()
    {
        radioOff.DisableKeyword("_EMISSION");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && rActive)
        {
            radio.SetActive(true);
            rActive = false;
            
            



        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) && !rActive)
        {

            radio.SetActive(false);
            rActive = true;
            radioOff.DisableKeyword("_EMISSION");
        }
    }

    //private void CambiaOb(int indiceNuevoObjeto)
    //{
    //    objeto[indiceActual].SetActive(false);

    //    objeto[indiceNuevoObjeto].SetActive(true);

    //    indiceActual = indiceNuevoObjeto;
    //}
}

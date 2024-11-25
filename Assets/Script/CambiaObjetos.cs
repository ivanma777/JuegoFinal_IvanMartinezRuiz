using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiaObjetos : MonoBehaviour
{
    [SerializeField] private GameObject radio;

    private int indiceActual;

    private bool rActive = true;
    

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
        }
    }

    //private void CambiaOb(int indiceNuevoObjeto)
    //{
    //    objeto[indiceActual].SetActive(false);

    //    objeto[indiceNuevoObjeto].SetActive(true);

    //    indiceActual = indiceNuevoObjeto;
    //}
}

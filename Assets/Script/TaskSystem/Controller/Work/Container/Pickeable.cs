using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickeable : MonoBehaviour, IInteractuable
{
    Transform objetoPadre;
    public void Interact()
    {
        Recoger(objetoPadre);
    }

   public void Recoger(Transform parent)
    {

        transform.SetParent(parent);
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



}

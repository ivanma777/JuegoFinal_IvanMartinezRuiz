using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.HighDefinition;
using UnityEngine;

public class Radio : MonoBehaviour
{
    [SerializeField] private Material radioOff;
    private bool estaApagado = true;
    
    void Start()
    {
       
        //radioOff = GetComponent<Material>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && estaApagado)
        {
            radioOff.DisableKeyword("_EMISSION");
            estaApagado = false;


        }
        else if(Input.GetKeyDown(KeyCode.E) && !estaApagado)
        {
            radioOff.EnableKeyword("_EMISSION");
            estaApagado = true;

        }
    }
}

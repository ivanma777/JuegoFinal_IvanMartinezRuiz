using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.HighDefinition;
using UnityEngine;

public class Radio : MonoBehaviour
{
    [SerializeField] private Material radioOff;
    private bool estaApagado = false;

    [SerializeField] private DialogueManager dialogueManager;

    [SerializeField] private GameObject radioCanvas;

    void Start()
    {
        radioOff.DisableKeyword("_EMISSION");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && estaApagado)
        {
            radioOff.DisableKeyword("_EMISSION");
            estaApagado = false;
            radioCanvas.SetActive(false);


        }
        else if(Input.GetKeyDown(KeyCode.E) && !estaApagado)
        {
            radioOff.EnableKeyword("_EMISSION");
            estaApagado = true;
            radioCanvas.SetActive(true);
            dialogueManager.StartDialogue();


        }

        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.HighDefinition;

using UnityEngine;
using TMPro;

public class Radio : MonoBehaviour
{
    [SerializeField] private RadioEvent radioEvent;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private GameObject radioCanvas;
    [SerializeField] private Material radioMaterial;

    private string[] lineas;
    private int index;
    private float textSpeed = 0.05f;
    private bool estaActiva;

    private void OnEnable()
    {
        radioEvent.Register(OnRadioMensajeRecibido);
    }

    private void OnDisable()
    {
        radioEvent.UnRegister(OnRadioMensajeRecibido);
    }

    private void OnRadioMensajeRecibido(RadioSO so)
    {
        RadioResponseSO respuesta = so.ObtenerRespuestaPorConfianza(0); // se ignora porque ya se eligió en el manager

        if (respuesta != null)
        {
            lineas = new string[] { respuesta.mensaje }; // puedes dividir en varias si deseas
            index = 0;
            textComponent.text = "";
            audioSource.PlayOneShot(respuesta.audioClip);
            StartCoroutine(TypeLine());

            radioCanvas.SetActive(true);
            radioMaterial.EnableKeyword("_EMISSION");
            estaActiva = true;
        }
    }

    private void Update()
    {
        if (!estaActiva) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            FindObjectOfType<RadioManager>().CogerAlarma();
            radioCanvas.SetActive(false);
            estaActiva = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (textComponent.text == lineas[index])
            {
                SiguienteLinea();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lineas[index];
            }
        }
    }

    private IEnumerator TypeLine()
    {
        textComponent.text = "";
        foreach (char c in lineas[index])
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    private void SiguienteLinea()
    {
        if (index < lineas.Length - 1)
        {
            index++;
            StartCoroutine(TypeLine());
        }
        else
        {
            textComponent.text = "";
        }
    }
}


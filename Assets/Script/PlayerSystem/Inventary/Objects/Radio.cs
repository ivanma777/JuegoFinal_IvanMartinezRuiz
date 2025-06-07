using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.HighDefinition;

using UnityEngine;
using TMPro;

public class Radio : MonoBehaviour
{
    [SerializeField] private RadioEvent radioEvent;
    [SerializeField] private VoidEvent radioAnsweredEvent;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private GameObject radioCanvas;
    [SerializeField] private Material radioMaterial;
    [SerializeField] private GameObject radioObject;

    private string[] lineas;
    private int index;
    private float textSpeed = 0.05f;
    private bool estaActiva;
    private bool haRespondido = false;
    private bool radioSacada = false;

    private AudioClip currentAudioClip;

    private void Awake()
    {
           
    }


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
        Debug.Log("Llego alarma");
        RadioResponseSO respuesta = so.ObtenerRespuestaPorConfianza(0); // se ignora porque ya se eligió en el manager

        if (respuesta != null)
        {
            lineas = new string[] { respuesta.mensaje }; // puedes dividir en varias si deseas
            index = 0;
            textComponent.text = "";

            currentAudioClip = respuesta.audioClip;

            //StartCoroutine(TypeLine());

            //radioCanvas.SetActive(true);
            //radioMaterial.EnableKeyword("_EMISSION");
            estaActiva = true;
            haRespondido = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            radioSacada = !radioSacada; // Alterna entre sacarla y guardarla

            radioObject.SetActive(radioSacada);
            radioCanvas.SetActive(radioSacada); // Muestra o esconde el canvas
            radioMaterial.SetColor("_EmissionColor", radioSacada ? Color.white : Color.black); // Efecto visual opcional
        }

        if (radioSacada && estaActiva && Input.GetKeyDown(KeyCode.R))
        {
            if (!haRespondido)
            {
                StartCoroutine(TypeLine());
                audioSource.PlayOneShot(currentAudioClip);

                radioCanvas.SetActive(true);
                radioMaterial.EnableKeyword("_EMISSION");
                haRespondido = true;
                radioAnsweredEvent.Raise(new Void());
            }
            else
            {
                // Apagar radio
                audioSource.Stop();
                radioCanvas.SetActive(false);
                radioMaterial.DisableKeyword("_EMISSION");
                estaActiva = false;
                textComponent.text = "";
            }
        }

        if (radioSacada && estaActiva && Input.GetKeyDown(KeyCode.Space))
        {
            if (lineas == null || lineas.Length == 0) return;

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

    private IEnumerator CerrarRadioConDelay()
    {
        yield return new WaitForSeconds(1f);
        textComponent.text = "";
        radioCanvas.SetActive(false);
        radioMaterial.DisableKeyword("_EMISSION");
        estaActiva = false;
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
            StartCoroutine(CerrarRadioConDelay());
        }
    }
}

